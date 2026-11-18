using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryInventory : MonoBehaviour
{
    public List<Mercenary> mercenaries = new List<Mercenary>();
    public Transform Mercenarybag;
    public GameObject mercenaryInformation; 
    public GameObject mercenaryInventory;
    public void AddMercenary(Mercenary mercenary)//인벤토리에 물건을 연동
    {
        Debug.Log("AddMercenary 실행 : " + mercenary.mercenaryName);
        /////
        mercenaries.Add(mercenary); //기존 용병을 인벤토리에 추가

        MercenaryData data = new MercenaryData();// 용병 정보를 저장할 새로운 객체를 생성
        //밑에는 용병 정보
        data.mercenaryName = mercenary.mercenaryName;
        data.job = mercenary.job;
        data.mercenaryPrefab = mercenary.battlePrefab;
        Debug.Log("전투 Prefab : " + (data.mercenaryPrefab != null ? data.mercenaryPrefab.name : "None"));

        data.maxHP = mercenary.maxHP;
        data.currentHP = mercenary.currentHP;

        data.attack = mercenary.attack;
        data.defense = mercenary.defense;

        /////
        Image mercenaryImage = mercenary.GetComponent<Image>();
        data.mercenarySprite = mercenaryImage.sprite;
        MercenaryDataManager.instance.ownedMercenaries.Add(data);
        for (int i = 0; i < 12; i++)
        {
            Transform slot = Mercenarybag.GetChild(i);
            Image slotImage = slot.GetChild(0).GetComponent<Image>();
            
            if (slotImage.sprite == null)
            {
                slotImage.sprite = mercenaryImage.sprite;
                slotImage.preserveAspect = true;

                Button slotButton = slot.GetComponent<Button>();

                if (slotButton != null)
                {
                    MercenaryData selectedData = data;
                    RectTransform selectedSlot = slot.GetComponent<RectTransform>();

                    slotButton.onClick.AddListener(() =>
                    {
                        mercenaryInformation
                            .GetComponent<MercenaryInformation>()
                            .ShowOwnedMercenaryInformation(selectedData, selectedSlot);
                    });
                }
                break;
            }
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        mercenaryInventory.SetActive(false);
        Debug.Log("보유 용병 수 : " + MercenaryDataManager.instance.ownedMercenaries.Count);
        RestoreMercenarySlots();
    }
    private void RestoreMercenarySlots()//전투 후 인벤에 용병 복귀
    {
        List<MercenaryData> ownedMercenaries = MercenaryDataManager.instance.ownedMercenaries;

        int count = Mathf.Min(ownedMercenaries.Count, 12);

        for(int i = 0; i < count; i++)
        {
            MercenaryData data = ownedMercenaries[i];

            Transform slot = Mercenarybag.GetChild(i);
            Image slotImage = slot.GetChild(0).GetComponent<Image>();

            slotImage.sprite = data.mercenarySprite;
            slotImage.preserveAspect = true;

            Button slotButton = slot.GetComponent<Button>();

            if(slotButton != null)
            {
                MercenaryData selectedData = data;
                RectTransform selectedSlot = slot.GetComponent<RectTransform>();

                slotButton.onClick.RemoveAllListeners();

                slotButton.onClick.AddListener(() =>
                {
                    mercenaryInformation.GetComponent<MercenaryInformation>()
                    .ShowOwnedMercenaryInformation(selectedData, selectedSlot);
                });
            }
        }
    }
}
