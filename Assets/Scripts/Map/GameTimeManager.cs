using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
    [Header("게임 날짜")]
    public int currentDay = 1;
    [Header("게임 시간")]
    public int currentHour = 9;
    public int currentMinute = 0;
    public Text timeText;
    public static GameTimeManager instance;
    private void Awake()
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
    public void AdvanceTime(int hours)
    {
        currentHour += hours;
        while (currentHour >= 24)
        {
            currentHour -= 24;
            currentDay++;
        }
        Debug.Log("현재 게임 시간 : " + currentDay + "일차 " + currentHour + "시");
    }
    public void UpdateTimeUI()
    {
        timeText.text = currentDay + "일차 " + currentHour.ToString("00") +
            ":" + currentMinute.ToString("00");
    }
}
