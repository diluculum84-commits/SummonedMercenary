using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum MercenaryState
{
    Waiting,
    OnQuest
}

public class Mercenary : MonoBehaviour
{
    [Header("용병 기본 정보")]
    public string mercenaryID; // 각 용병을 구분하는 고유 번호

    public MercenaryState state = MercenaryState.Waiting;

    public string[] mercenaryNames =
    {
        "Arthur", "Leon", "Rian", "Kai", "Theo",
        "Liam", "Evan", "Noah", "Luke", "Ian"
    };

    public string mercenaryName;
    public string job;

    [Header("용병 종류")]
    public string mercenaryType;
    [Header("체력")]
    public int maxHP;
    public int currentHP;
    [Header("공격력")]
    public int attack;
    [Header("방어력")]
    public int defense;
    [Header("마나")]
    public int maxMana;
    [Header("스킬 시스템")]
    public int attackCount = 0;
    public int currentMana;
    [Header("가격")]
    public int price;
    public MercenaryInformation mercenaryInformation;
    [Header("전투 캐릭터")]
    public GameObject battlePrefab;
    public bool isBattleMercenary = false;
    public MercenaryData originalData;//전투 후 남은 체력 저장
    void Start()
    {
        if (!isBattleMercenary)
        {
           InitializeMercenary();
        }
    }
   
    public void InitializeMercenary()
    {
        mercenaryID = System.Guid.NewGuid().ToString();
        state = MercenaryState.Waiting;

        int randomName = Random.Range(0, mercenaryNames.Length);
        mercenaryName = mercenaryNames[randomName];

        currentHP = maxHP;

        currentMana = 0;
    }
    public void ClickMercenary() //용병 클릭시 용병 정보 활성화
    {
        Debug.Log("클릭한 용병 : " + mercenaryName);
        mercenaryInformation.ShowMercenaryInformation(this);
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        if(originalData != null)
        {
            originalData.currentHP = currentHP;
        }
        Debug.Log(mercenaryName + "남은 체력" + currentHP);
        if(currentHP <= 0)
        {
            Debug.Log(mercenaryName + "죽음");
        }
    }
}