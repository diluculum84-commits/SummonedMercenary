using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankNPCInteraction : MonoBehaviour
{
    public GameObject BankNPCPanel;
    public GameObject NPCinteractionkey;
    private bool isPlayerNearby;
    public GameObject BankPanel; //은행 돈 빌리는 패널
    public void ClosePanel()
    {
        BankNPCPanel.SetActive(false);
        NPCinteractionkey.SetActive(true);
        BankPanel.SetActive(false);
    }
    void Start()
    {
        BankNPCPanel.SetActive(false);
        NPCinteractionkey.SetActive(false);
        BankPanel.SetActive(false);
}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            NPCinteractionkey.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            BankNPCPanel.SetActive(false);
            NPCinteractionkey.SetActive(false);
            CloseBank();
        }
    }
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            BankNPCPanel.SetActive(true);
            NPCinteractionkey.SetActive(false);
        }
    }
    public void CloseBank()
    {
        BankNPCPanel.SetActive(false);
        BankPanel.SetActive(false);
    }
    public void OpenBankPanel() //은행 확인 패널
    {
        BankNPCPanel.SetActive(false);
        BankPanel.SetActive(true);
    }

    public void CloseBankPanel() //은행 확인 패널
    {
        BankPanel.SetActive(false);
    }
}