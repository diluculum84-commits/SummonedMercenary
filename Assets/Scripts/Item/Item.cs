using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("아이템 기본 정보")]
    public string itemName;
    public int price;

    [Header("아이템 효과")]
    public string effectName;
    public int effectValue;

    public ItemInformation itemInformation;
    
    public void ShowInformation()
    {
        itemInformation.ShowItemInformation(this);
    }
}
