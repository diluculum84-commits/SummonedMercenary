using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySlotClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        BattleManager battleManager = FindObjectOfType<BattleManager>();
        if(battleManager != null && battleManager.isBattleStarted)
        {
            Debug.Log("전투 진행중 용병 배치는 할 수 없습니다.");
            return;
        }

        Debug.Log("아군 슬롯 클릭 : " + gameObject.name);
        BattleMercenaryUI battleUI = FindObjectOfType<BattleMercenaryUI>();

        if (battleUI == null)
        {
            Debug.LogWarning("BattleMercenaryUI를 찾을 수 없습니다.");
            return;
        }

        int selectedIndex = battleUI.GetSelectedMercenaryIndex();

        Debug.Log("현재 선택된 용병 번호 : " + selectedIndex);
        BattleSlot battleSlot = GetComponent<BattleSlot>();
        if (battleSlot.isOccupied)
        {
            int removedIndex = battleSlot.mercenaryIndex;
            battleSlot.ClearSlot(); //배치 취소
            battleUI.EnableMercenarySlot(removedIndex);
            Debug.Log("배치를 취소한 용병 번호 : " + removedIndex);
            return;
        }

        if (selectedIndex == -1)
        {
            Debug.Log("용병을 먼저 선택하세요. ");
            return;
        }
        Debug.Log("비어 있는 아군 슬롯 : " + battleSlot.gameObject.name);
        MercenaryData selectedMercenary = MercenaryDataManager.instance.ownedMercenaries[selectedIndex];

        GameObject battlePrefab = selectedMercenary.mercenaryPrefab;

        if (battlePrefab == null)
        {
            Debug.LogWarning("전투 Prefab이 연결되지 않았습니다.");
            return;
        }

        Debug.Log("배치할 용병 : " + selectedMercenary.mercenaryName);
        Debug.Log("소환할 Prefab : " + battlePrefab.name);
        GameObject spawnedMercenary = Instantiate(battlePrefab,transform.position,Quaternion.identity);
        //Instantiate : 선택한 용병 prefab을 생성
        Mercenary mercenary = spawnedMercenary.GetComponent<Mercenary>();

        mercenary.isBattleMercenary = true;
        mercenary.originalData = selectedMercenary;//남은 체력 저장
        mercenary.mercenaryName = selectedMercenary.mercenaryName;
        mercenary.attack = selectedMercenary.attack;
        mercenary.defense = selectedMercenary.defense;
        mercenary.maxHP = selectedMercenary.maxHP;
        mercenary.currentHP = selectedMercenary.currentHP;

        if (battleSlot.PlaceMercenary(spawnedMercenary, selectedIndex))
        {
            battleUI.DisableMercenarySlot(selectedIndex);
            battleUI.ClearSelection();
        }
        else
        {
            Destroy(spawnedMercenary);
            Debug.Log("용병 배치에 실패했습니다.");
        }
        battleUI.DisableMercenarySlot(selectedIndex);//배치된 용병슬롯 비활성화
        battleUI.ClearSelection();//배치 완료 후 선택 초기화
    }
}
