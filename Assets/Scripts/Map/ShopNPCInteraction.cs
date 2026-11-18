using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ShopNPCPanel;
    public GameObject NPCinteractionkey;
    private bool isPlayerNearby;
    public GameObject ShopItemPanel;
    public ItemInformation itemInformation;
    public GameObject ShopCheckPanel;
    public Inventory inventory;
    
    public void ClosePanel()
    {
        ShopNPCPanel.SetActive(false);
        NPCinteractionkey.SetActive(true);
    }
    void Start()
    {
        ShopNPCPanel.SetActive(false);
        NPCinteractionkey.SetActive(false);
        ShopItemPanel.SetActive(false);
        ShopCheckPanel.SetActive(false);
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
            ShopNPCPanel.SetActive(false);
            NPCinteractionkey.SetActive(false);
            CloseShop();
        }
    }
    public void CloseShop()
    {
        ShopNPCPanel.SetActive(false);
        ShopItemPanel.SetActive(false);
        if(itemInformation.gameObject.activeSelf)
        {
            itemInformation.CloseItemInformation();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShopNPCPanel.SetActive(true);
            NPCinteractionkey.SetActive(false);
        }
    }
    public void OpenShop()
    {
        ShopItemPanel.SetActive(true);
        ShopNPCPanel.SetActive(false);
    }
    public void OpenBuyCheck() //구매버튼 활성화
    {
        ShopCheckPanel.SetActive(true);
    }
    public void CloseBuyCheck() //취소버튼 활성화
    {
        ShopCheckPanel.SetActive(false);
    }
    public void ConfirmBuy()
    {
        Debug.Log("구매 선택 아이템 : " + itemInformation.selectedItem);
        inventory.AddItem(itemInformation.selectedItem);
        ShopCheckPanel.SetActive(false);
    }
}
