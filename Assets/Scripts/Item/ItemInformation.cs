using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformation : MonoBehaviour
{
    // Start is called before the first frame update
    public Text itemNameText;
    public Text itemEffectText;
    public Text itemPriceText;
    public ItemInformation itemInformation;
    public Item selectedItem; //현재 선택된 아이템

    void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowItemInformation(Item item)
    {
        if (selectedItem == item && gameObject.activeSelf) //클릭한 아이템 = 지금 보고 있는 아이템, 정보창이 열려 있는가?
        {
            gameObject.SetActive(false);
            selectedItem = null;
            return;
        }
        selectedItem = item;
        gameObject.SetActive(true);
        gameObject.transform.position = item.transform.position;

        itemNameText.text = "이름 : " + item.itemName;
        itemEffectText.text = "효과 : " + item.effectName + " +" + item.effectValue;
        itemPriceText.text = "가격 : " + item.price;
    }
    public void CloseItemInformation()
    {
        gameObject.SetActive(false);
        selectedItem = null;
    }

    public void BuyItem()
    {
        Item selectedItem = itemInformation.selectedItem;

        if (selectedItem == null)
        {
            Debug.Log("선택된 아이템이 없습니다.");
            return;
        }

        Debug.Log("구매할 아이템 : " + selectedItem.itemName);
    }
}
