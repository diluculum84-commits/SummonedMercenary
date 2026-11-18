using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMercenaryUI : MonoBehaviour
{
    public Transform mercenarySlotContent;
    private int selectedMercenaryIndex = -1;//용병을 아예 배치안한상태
    void Start()
    {
        if (MercenaryDataManager.instance == null) //playscene에 생성한 데이터 관리자에 접근
        {
            Debug.LogWarning("MercenaryDataManager가 없습니다.");
            return;
        }

        int count = MercenaryDataManager.instance.ownedMercenaries.Count; //구매한 용병 수 확인

        Debug.Log("전투 화면에서 확인한 보유 용병 수 : " + count);
        if (count == 0)
        {
            return;
        }
        int displayCount = Mathf.Min(count, 12);

        for (int i = 0; i < displayCount; i++)
        {
            MercenaryData mercenary = MercenaryDataManager.instance.ownedMercenaries[i];
            Transform slot = mercenarySlotContent.GetChild(i);
            Image mercenaryIcon = slot.GetChild(0).GetComponent<Image>();

            mercenaryIcon.sprite = mercenary.mercenarySprite;
            mercenaryIcon.preserveAspect = true;

            Button slotButton = slot.GetComponent<Button>();

            if (slotButton != null)
            {
                int index = i;
                slotButton.onClick.AddListener(() => SelectMercenary(index));
            }
        }
    }
    public void SelectMercenary(int index)
    {
        selectedMercenaryIndex = index;

        Debug.Log("선택한 용병 번호 : " + selectedMercenaryIndex);
        if(IsMercenaryPlace(index))
        {
            Debug.Log("이미 배치된 용병입니다.");
            return;
        }
    }
    public int GetSelectedMercenaryIndex()
    {
        return selectedMercenaryIndex;//용병을 선택했다면0, 아니면 -1 반환
    }
    public void ClearSelection()//용병을 배치한 후에 초기화
    {
        selectedMercenaryIndex = -1; //용병배치를 선택하기 전 상태로 되돌림
    }
    public bool IsMercenaryPlace(int index)
    {
        BattleSlot[] slots = FindObjectsOfType<BattleSlot>();

        foreach (BattleSlot slot in slots)
        {
            if(slot.slotTeam == BattleSlot.SlotTeam.Ally && slot.isOccupied && slot.mercenaryIndex == index)
            {
                return true;
            }
        }
        return false;
    }
    public void DisableMercenarySlot(int index)
    {
        Transform slot = mercenarySlotContent.GetChild(index);
        Button slotButton = slot.GetComponent<Button>();
        slotButton.interactable = false; //슬롯 비활성화 
    }
    public void EnableMercenarySlot(int index)
    {
        Transform slot = mercenarySlotContent.GetChild(index);
        Button slotButton = slot.GetComponent<Button>();
        slotButton.interactable = true;//슬롯 다시 활성화
    }
}