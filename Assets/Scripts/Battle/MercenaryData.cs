using System;
using UnityEngine;

[Serializable]
public class MercenaryData
{
    public string mercenaryName;
    public string job;
    public int maxHP;
    public int currentHP;
    public int attack;
    public int defense;
    public Sprite mercenarySprite;
    public GameObject mercenaryPrefab;
}