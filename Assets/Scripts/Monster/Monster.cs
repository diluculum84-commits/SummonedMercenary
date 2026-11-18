using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("몬스터 기본 정보")]
    public string monsterName;
    [Header("체력")]
    public int maxHP;//체력
    public int CurrentHP;//현재 체력
    [Header("공격력")]
    public int attack; //공격력
    [Header("방어력")]
    public int defense;//방어
    [Header("마나")]
    public int Mana;//마나
    void Awake()
    {
        CurrentHP = maxHP;
    }
    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        Debug.Log("monsterName" + "남은 체력" + CurrentHP);
        if(CurrentHP <= 0)
        {
            Debug.Log(monsterName + "죽음");
        }
    }
}