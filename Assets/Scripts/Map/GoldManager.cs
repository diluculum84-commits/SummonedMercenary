using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public int currentGold = 10000;
    public static GoldManager instance;
    public Text goldText;
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
    public bool SpendGold(int amount)//골드 차감
    {
        if(currentGold < amount)//현재 골드보다 작을시
        {
            Debug.Log("골드가 부족합니다");
            return false;
        }
        currentGold -= amount;//골드 차감
        UpdateGoldUI();
        Debug.Log("차감된 골드 : " + amount);
        Debug.Log("남은 골드 : " + currentGold);
        return true;
    }
    public void UpdateGoldUI()
    {
        if(goldText != null)
        {
            goldText.text = "골드 : " + currentGold + " G";
        }
    }
    public void ConnectGoldUI(Text newGoldText)
    {
        goldText = newGoldText;
        UpdateGoldUI();
    }
    public void AddGold(int amount)//전투 종료후 보상
    {
        if(amount <=0)
        {
            return;
        }
        currentGold += amount;
        UpdateGoldUI();
        Debug.Log("골드 획득 : " + amount);
        Debug.Log("현재 골드 : " + currentGold);
    }
    public void RepayDebt(int amount)//돈 갚기
    {
        currentGold -= amount;
        UpdateGoldUI();
    }
}