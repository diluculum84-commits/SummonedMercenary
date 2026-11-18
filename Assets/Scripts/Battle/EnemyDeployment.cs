using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeployment : MonoBehaviour
{
    public List<BattleSlot> enemySlots = new List<BattleSlot>();
    public GameObject BlooMonsterPrefab;
    public GameObject DemonPrefab;
    public GameObject EvilWizardPrefab;
    public GameObject EvilFireWizardPrefab;
    public GameObject NecromancerPrefab;
    public GameObject OrcPrefab;
    void Start()
    {
        Debug.Log("적 슬롯 : " + enemySlots.Count);
        int monsterCount = BattleData.instance.monsterCount;
        string monsterName = BattleData.instance.monsterName;
        Debug.Log("의뢰 몬스터 : " +  monsterName);
        GameObject selectedMonsterPrefab = null;

        if(monsterName == "BloodMonster")
        {
            selectedMonsterPrefab = BlooMonsterPrefab;
        }
        else if(monsterName == "Demon")
        {
            selectedMonsterPrefab = DemonPrefab;
        }
        else if (monsterName == "EvilWizard")
        {
            selectedMonsterPrefab = EvilWizardPrefab;
        }
        else if (monsterName == "EvilFireWizard")
        {
            selectedMonsterPrefab = EvilFireWizardPrefab;
        }
        else if (monsterName == "Necromancer")
        {
            selectedMonsterPrefab = NecromancerPrefab;
        }
        else if (monsterName == "Orc")
        {
            selectedMonsterPrefab = OrcPrefab;
        }
        Debug.Log("의뢰 몬스터 : " + monsterName);
        List<BattleSlot> availableSlots = new List<BattleSlot>(enemySlots);

        for (int i = 0; i < monsterCount && availableSlots.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableSlots.Count);

            BattleSlot randomSlot = availableSlots[randomIndex];

            Debug.Log("선택된 적 슬롯 : " + randomSlot.gameObject.name);

            if(selectedMonsterPrefab != null)
            {
                GameObject monster = Instantiate(selectedMonsterPrefab,
                randomSlot.transform.position,Quaternion.identity);
                randomSlot.occupant = monster;
                randomSlot.isOccupied = true;
            }
            availableSlots.RemoveAt(randomIndex);
        }
    }
}
