using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory;
    public Transform bag;
    public GameObject itemInformation;
    public GameObject mercenaryInventory;
    public GameObject inventoryPanel;
    public List<Item> items = new List<Item>();
    public GameObject tabButtons;
    public GameObject mercenaryInformation;

    public void AddItem(Item item)//인벤토리에 물건을 연동
    {
        items.Add(item);
        for(int i = 0; i < bag.transform.childCount; i++)
        {
            Transform slot = bag.GetChild(i);
            if(slot.childCount == 0)
            {
                GameObject newItem = Instantiate(item.gameObject);
                newItem.transform.SetParent(slot, false);
                //newItem.transform.position = slot.position;
                RectTransform newItemRect = newItem.GetComponent<RectTransform>();
                newItemRect.anchorMin = new Vector2(0.5f, 0.5f);
                newItemRect.anchorMax = new Vector2(0.5f, 0.5f);
                newItemRect.pivot = new Vector2(0.5f, 0.5f);
                newItemRect.anchoredPosition = new Vector2(0, 0);
                break;
            }
        }
    }
    public void OpenItemInventory()
    {
        inventory.SetActive(true);
        mercenaryInventory.SetActive(false);
    }
    public void OpenMercenaryInventory()
    {
        inventory.SetActive(false);
        mercenaryInventory.SetActive(true);
    }
    void Start()
    {
        inventoryPanel.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && Time.timeScale != 0f)
        {
            if(!inventoryPanel.activeSelf) // inventory 활성화 되어 있지 않다면?이라는 뜻
            {
                inventoryPanel.SetActive(true);
                tabButtons.SetActive(true);
                OpenItemInventory();
            }
            else
            {
                inventoryPanel.SetActive(false);
                itemInformation.SetActive (false);
                mercenaryInformation.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryPanel.SetActive(false);
            itemInformation.SetActive(false);
            mercenaryInformation.SetActive(false);
        }
    }
}
