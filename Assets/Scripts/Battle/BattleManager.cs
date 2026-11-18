using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public bool isBattleStarted = false;
    public float attackDistance = 0.8f;
    public float moveSpeed = 3f;
    private List<MercenaryData> battleParticipants = new List<MercenaryData>();
    // 전투에 참가한 용병들 기록
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public Text victoryGoldText;
    public Text defeatGoldText;
    public int victoryGold = 500;
    public int defeatGold = 200;
    public void Awake()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }
    public void GoToTown()//마을로 이동 버튼
    {
        if(isBattleStarted)
        {
            Debug.Log("전투 중에는 마을로 이동할 수 없습니다.");
            return;
        }
        SceneManager.LoadScene("PlayScene");
    }
    public void VictoryPanel()
    {
        victoryPanel.SetActive(false);
    }
    public void DefeatPanel()
    {
        defeatPanel.SetActive(false);
    }
    public void QuitButton()//전투 종료 후 나가기 버튼
    {
        GameTimeManager.instance.AdvanceTime(12);
        SceneManager.LoadScene("PlayScene");
    }

    public void StartBattle()
    {
        if(isBattleStarted)
        {
            return;
        }
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();
        int allyCount = 0;
        foreach(BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Ally && slot.isOccupied)
            {
                allyCount++;
            }
        }
        if(allyCount == 0)
        {
            Debug.Log("아군 용병을 최소 1명은 배치해야 합니다.");
            return;
        }

        isBattleStarted = true;
        Debug.Log("전투가 시작됩니다!");

        List<BattleSlot> sortedAllies = GetSortedAllies();
        battleParticipants.Clear();
        foreach(BattleSlot slot in sortedAllies)
        {
            if (slot.occupant != null)
            {
                Mercenary mercenary = slot.occupant.GetComponent<Mercenary>();

                if (mercenary != null && mercenary.originalData != null)
                {
                    if (!battleParticipants.Contains(mercenary.originalData))
                    {
                        battleParticipants.Add(mercenary.originalData);
                    }
                }
            }
        }

        Debug.Log("전투 참가 용병 수 : " + battleParticipants.Count);

        foreach(BattleSlot slot in sortedAllies)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Ally && slot.isOccupied)
            {
                int targetRow = FindTargetRow(slot.y);
                if(targetRow == -1)
                {
                    Debug.Log("공격할 대상이 없습니다.");
                    return;
                }
                BattleSlot target = FindFrontEnemy(targetRow);
                if(target != null)
                {
                    Debug.Log("아군 공격 순서 : " + slot.name + " (X:" + slot.x + ", Y:" + slot.y + ")" +
                              " 공격 대상 : " + target.name);
                }
            }
        }
        StartCoroutine(BattleLoop());//턴 반복
    }
    private IEnumerator BattleLoop()//턴 반복 수행
    {
        while(HasLivingUnits(BattleSlot.SlotTeam.Ally) && HasLivingUnits(BattleSlot.SlotTeam.Enemy))
        {
            yield return StartCoroutine(AllyAttackTurn());//아군 턴 시작

            if(!HasLivingUnits(BattleSlot.SlotTeam.Enemy))
            {
                break;
            }
            yield return StartCoroutine(EnemyAttackTurn());//몬스터 턴 시작
        }
        if(HasLivingUnits(BattleSlot.SlotTeam.Ally))
        {
            Debug.Log("전투 승리!");
            GoldManager.instance.AddGold(victoryGold);
            victoryGoldText.text = "획득 골드 : +" + victoryGold + " G";
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.Log("전투 패배!");
            GoldManager.instance.AddGold(defeatGold);
            defeatGoldText.text = "획득 골드 : +" + defeatGold + " G";
            defeatPanel.SetActive(true);
        }
        RemoveBattleParticipants();//승패 상관없이 용병 삭제
    }
    private bool HasLivingUnits(BattleSlot.SlotTeam team)//아군,몬스터가 있는지 확인
    {
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();

        foreach(BattleSlot slot in slots)
        {
            if(slot.slotTeam == team &&
                slot.isOccupied &&
                slot.occupant != null)
            {
                if(team == BattleSlot.SlotTeam.Ally)//아군이 살아있는가
                {
                    Mercenary mercenary = slot.occupant.GetComponent<Mercenary>();

                    if (mercenary != null && mercenary.currentHP > 0)
                        return true;
                }
                else//몬스터가 살아있는가
                {
                    Monster monster = slot.occupant.GetComponent<Monster>();

                    if (monster != null && monster.CurrentHP > 0)
                        return true;
                }
            }
        }
        return false;
    }
    private int GetTargetRow(int attackerRow, int order)//attacK : 공격자,order : 순서
    {
        return (attackerRow + order) % 3;
    }
    private bool HasEnemyInRow(int targetRow)//아군 기준
    {
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();
        foreach(BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Enemy && slot.y == targetRow && slot.isOccupied)
            {
                return true;
            }
        }
        return false;
    }
    private bool HasAllyInRow(int targetRow)//몬스터 기준
    {
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();

        foreach(BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Ally &&
                slot.y == targetRow &&
                slot.isOccupied)
            {
                return true;
            }
        }

        return false;
    }
    private int FindTargetRow(int attackerRow) //공격행 : 1행 : 123/2행 : 231/3행 : 312
    {
        for(int i = 0; i<3; i++)
        {
            int targetRow = GetTargetRow(attackerRow, i);
            if (HasEnemyInRow(targetRow))
            {
                return targetRow;
            }
        }
        return -1;
    }
    private int FindAllyTargetRow(int attackerRow)//몬스터 기준 아군 공격과 패턴 동일
    {
        for(int i = 0; i < 3; i++)
        {
            int targetRow = GetTargetRow(attackerRow, i);

            if(HasAllyInRow(targetRow))
            {
                return targetRow;
            }
        }
        return -1;
    }
    private BattleSlot FindFrontEnemy(int targetRow) //아군 : 그줄 가장 앞 적 찾기
    {
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();
        BattleSlot frontEnemy = null;

        foreach(BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Enemy &&
                slot.y == targetRow &&
                slot.isOccupied)
            {
                if(frontEnemy == null || slot.x < frontEnemy.x)
                {
                    frontEnemy = slot;
                }
            }
        }
        return frontEnemy;
    }

    private BattleSlot FindFrontAlly(int targetRow)//몬스터 : 그줄 가장 앞 적 찾기
    {
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();
        BattleSlot frontAlly = null;

        foreach(BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Ally &&
                slot.y == targetRow &&
                slot.isOccupied)
            {
                if(frontAlly == null || slot.x > frontAlly.x)
                {
                    frontAlly = slot;
                }
            }
        }
        return frontAlly;
    }

    private List<BattleSlot> GetSortedAllies()
    {
        List<BattleSlot> allies = new List<BattleSlot>();

        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();

        foreach(BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Ally && slot.isOccupied)
            {
                allies.Add(slot);
            }
        }
        allies.Sort((a, b) =>
        {
            if(a.y != b.y)
            {
                return a.y.CompareTo(b.y);
            }
            return b.x.CompareTo(a.x);
        });
        return allies;
    }
    private List<BattleSlot> GetSortedEnemies()
    {
        List<BattleSlot> enemies = new List<BattleSlot>();
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();

        foreach (BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Enemy && slot.isOccupied)
            {
                enemies.Add(slot);
            }
        }
        enemies.Sort((a, b) =>
        {
            if(a.y != b.y)
            {
                return a.y.CompareTo(b.y);
            }
            return a.x.CompareTo(b.x);
        });
        return enemies;
    }
    private IEnumerator AllyAttackTurn()//아군턴
    {
        List<BattleSlot> allies = GetSortedAllies();

        foreach(BattleSlot ally in allies)
        {
            Debug.Log("공격 시작 : " + ally.name);

            int targetRow = FindTargetRow(ally.y);

            if(targetRow == -1)//앞에 적이 없다면
            {
                Debug.Log("공격할 적이 없습니다.");
                yield break;
            }
            //다른행으로 넘어가기
            BattleSlot target = FindFrontEnemy(targetRow);

            if(target != null && ally.occupant != null && target.occupant != null)
            {
                Vector3 originalPosition = ally.occupant.transform.position;//원래 위치로 다시 이동
                Vector3 attackPosition = GetAttackPosition(target);
                yield return StartCoroutine(MoveToEnemy(ally.occupant, attackPosition));

                Debug.Log("용병 이동 : " + ally.name);
                //애니메이션
                Animator animator = ally.occupant.GetComponent<Animator>();

                if(animator != null)
                {
                    animator.SetTrigger("Attack");

                    Debug.Log("공격 실행 : " + ally.name);
                }
                yield return new WaitForSeconds(1f);
                //끝
                Mercenary mercenary = ally.occupant.GetComponent<Mercenary>();
                Monster monster = target.occupant.GetComponent<Monster>();
                if(mercenary != null && monster != null)
                {
                    mercenary.attackCount++;
                    Debug.Log(mercenary.mercenaryName + "공격 횟수 : " + mercenary.attackCount);

                    bool isSkillAttack = mercenary.attackCount >= 3;
                    
                    if (isSkillAttack)
                    {
                        Debug.Log(mercenary.mercenaryName + " 스킬 발동!");
                        mercenary.attackCount = 0;
                    }
                    else
                    {
                        Debug.Log(mercenary.mercenaryName + " 일반 공격!");
                    }
                    int attackDamage = mercenary.attack;
                    if (isSkillAttack)
                    {
                        attackDamage *= 2;
                    }
                    int damage = Mathf.Max(1, attackDamage - monster.defense);
                    Debug.Log("용병 공격력 : " + mercenary.attack+ " | 몬스터 방어력 : " + monster.defense
                                + " | 최종 데미지 : " + damage);
                    monster.TakeDamage(damage);
                    if(monster.CurrentHP <= 0)
                    {
                        target.ClearSlot();
                    }
                }
                yield return StartCoroutine(MoveToEnemy(ally.occupant, originalPosition));

                Debug.Log("용병 복귀 : " + ally.name);
            }
        }
        Debug.Log("아군 공격 종료");
    }

    private IEnumerator EnemyAttackTurn()//몬스터 턴
    {
        Debug.Log("몬스터 턴 시작");
        List<BattleSlot> enemies = GetSortedEnemies();

        foreach(BattleSlot enemy in enemies)
        {
            Debug.Log("몬스터 공격 순서 : " + enemy.gameObject.name);

            int targetRow = FindAllyTargetRow(enemy.y);

            if(targetRow == -1)//공격 가능한 아군이 없으면 턴 종료
            {
                Debug.Log("공격할 아군이 없습니다.");
                yield break;
            }

            BattleSlot target = FindFrontAlly(targetRow);//해당 줄에서 가장 앞 아군 선택
            if(target != null)
            {
                Debug.Log("공격 몬스터 : " + enemy.name + " + 공격 대상 아군 : " + target.name);
            }
            if(target != null && enemy.occupant != null && target.occupant != null)
            {
                Vector3 originalPosition = enemy.occupant.transform.position;
                Vector3 attackPosition = GetEnemyAttackPosition(target);

                yield return StartCoroutine(MoveToEnemy(enemy.occupant, attackPosition));

                Debug.Log("몬스터 이동 : " + enemy.name);
                //몬스터 애니메이션
                Animator animator = enemy.occupant.GetComponent<Animator>();

                if(animator != null)
                {
                    animator.SetTrigger("Attack");

                    Debug.Log("몬스터 공격 모션 실행 : " + enemy.name);
                }
                yield return new WaitForSeconds(1f);
                //끝
                Monster monster = enemy.occupant.GetComponent<Monster>();
                Mercenary mercenary = target.occupant.GetComponent<Mercenary>();

                if(monster != null && mercenary != null)
                {
                    int damage = Mathf.Max(0, monster.attack - mercenary.defense);

                    Debug.Log("몬스터 공격력 : " + monster.attack+ " | 아군 방어력 : " 
                                + mercenary.defense+ " | 최종 데미지 : " + damage);
                    mercenary.TakeDamage(damage);
                    if(mercenary.currentHP <= 0)
                    {
                        Debug.Log(mercenary.mercenaryName + " 사망");
                        target.ClearSlot();
                    }
                }
                yield return StartCoroutine(MoveToEnemy(enemy.occupant, originalPosition));

                Debug.Log("몬스터 복귀 : " + enemy.name);
            }

            yield return new WaitForSeconds(1f);
        }
        Debug.Log ("몬스터 공격 종료");
    }

    private Vector3 GetAttackPosition(BattleSlot target)
    {
        Vector3 targetPosition = target.occupant.transform.position;//공격대상 위치
        return targetPosition + Vector3.left * attackDistance;
    }
    private Vector3 GetEnemyAttackPosition(BattleSlot target)
    {
        Vector3 targetPosition = target.occupant.transform.position;

        return targetPosition + Vector3.right * attackDistance;
    }
    private IEnumerator MoveToEnemy(GameObject mercenary, Vector3 targetPosition) //용병 공격이동
    {
        while(Vector3.Distance(mercenary.transform.position, targetPosition) > 0.01f)
        {
            mercenary.transform.position = Vector3.MoveTowards(mercenary.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        mercenary.transform.position = targetPosition;
    }
    private void RemoveBattleParticipants()//전투 종료 후 용병 삭제
    {
        foreach(MercenaryData mercenary in battleParticipants)
        {
            MercenaryDataManager.instance.ownedMercenaries.Remove(mercenary);
        }

        Debug.Log("전투 참가 용병 삭제 완료 : " + battleParticipants.Count + "명");

        battleParticipants.Clear();
    }
}