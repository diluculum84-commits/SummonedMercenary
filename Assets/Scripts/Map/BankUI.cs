using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class BankUI : MonoBehaviour
{
    public Button loan100Button;
    public Button loan200Button;
    public Button loan300Button;
    [Header("대출 갚기")]
    public GameObject bankLoanPanel;
    public Text loanAmountText;
    void Start()
    { 
        if (BankManager.instance != null)
        {
            BankManager.instance.loan100Button = loan100Button;
            BankManager.instance.loan200Button = loan200Button;
            BankManager.instance.loan300Button = loan300Button;

            BankManager.instance.UpdateLoanButtons();
        }
        bankLoanPanel.SetActive(false);
        if (BankManager.instance != null && BankManager.instance.IsRepaymentDue())
        {
            loanAmountText.text = "빌린돈 : " + BankManager.instance.loanAmount + "G";
            bankLoanPanel.SetActive(true);
        }
    }
    public void RepayLoanButton()
    {
        if(BankManager.instance == null)
        {
            return;
        }
        if(!BankManager.instance.IsRepaymentDue())
        {
            return;
        }
        BankManager.instance.RepayLoan();
        bankLoanPanel.SetActive(false);
    }
}