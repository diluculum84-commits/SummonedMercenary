using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BankManager : MonoBehaviour
{
    [Header("대출 정보")]
    public int loanAmount = 0; //대출 원금
    public int repaymentDay = 0; //대출 상환 날짜
    public int repaymentHour = 0; //대출 상환 시간
    public bool hasLoan = false; //대출 여부
    [Header("대출 버튼")]
    public Button loan100Button;
    public Button loan200Button;
    public Button loan300Button;
    public static BankManager instance;
    public void TakeLoan(int amount)
    {
        if (hasLoan)
        {
            Debug.Log("기존 대출금을 상환해야 합니다.");
            return;
        }
        loanAmount = amount;
        hasLoan = true;
        GoldManager.instance.AddGold(amount);//대출 금액 지급
        int loanDays = 1;

        repaymentDay = GameTimeManager.instance.currentDay + loanDays;//현재 날짜+대출기간
        repaymentHour = GameTimeManager.instance.currentHour;//대출 받은 시간
        UpdateLoanButtons();
        Debug.Log("대출 금액 : " + loanAmount);
        Debug.Log("상환 예정 : " + repaymentDay + "일차 " + repaymentHour + "시");
        Debug.Log("현재 날짜 : " + GameTimeManager.instance.currentDay);
        Debug.Log("현재 시간 : " + GameTimeManager.instance.currentHour);
        Debug.Log("상환 날짜 : " + repaymentDay);
        Debug.Log("상환 시간 : " + repaymentHour);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateLoanButtons()
    {
        loan100Button.interactable = !hasLoan;
        loan200Button.interactable = !hasLoan;
        loan300Button.interactable = !hasLoan;
    }
    public bool IsRepaymentDue()
    {
        if(!hasLoan)
        {
            return false;
        }

        int currentTotalHours = GameTimeManager.instance.currentDay * 24
            + GameTimeManager.instance.currentHour;
        int repaymentTotalHours = repaymentDay * 24 + repaymentHour;

        return currentTotalHours >= repaymentTotalHours;
    }
    public void RepayLoan()
    {
        if (!hasLoan)
        {
            return;
        }
        GoldManager.instance.RepayDebt(loanAmount);
        hasLoan = false;
        loanAmount = 0;
        repaymentDay = 0;
        repaymentHour = 0;
        UpdateLoanButtons();
    }
}