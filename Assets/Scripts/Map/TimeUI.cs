using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public Text timeText;

    void Start()
    {
        if(GameTimeManager.instance != null)
        {
            GameTimeManager.instance.timeText = timeText;
            GameTimeManager.instance.UpdateTimeUI();
        }
    }
}