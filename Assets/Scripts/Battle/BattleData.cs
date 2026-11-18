using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoBehaviour
{
    public string monsterName;
    public int monsterCount;
    public int difficulty;
    public static BattleData instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public enum Difficulty
    {
        Easy,Normal,Hard
    }
}
