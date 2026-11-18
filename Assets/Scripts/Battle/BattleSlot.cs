using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot : MonoBehaviour
{
    public int x;
    public int y;
    public SlotTeam slotTeam;
    public bool isOccupied;//슬롯이 사용중인지 확인
    public GameObject occupant;//슬롯에 생성된 용병을 gameobject에 저장
    public int mercenaryIndex = -1;//현재 슬롯에 배치덴 용병의 값, 빈슬롯 : -1, 01슬롯 : 0
    public enum SlotTeam
    {
        Ally,Enemy
    }
    public bool PlaceMercenary(GameObject mercenary, int index) // 슬롯에 용병을 배치할 수 있는지 확인, 배치 가능하면 배치
    {
        if(isOccupied || mercenary == null)
        {
            return false;
        }

        occupant = mercenary;
        mercenaryIndex = index;
        isOccupied = true;

        mercenary.transform.position = transform.position;

        return true;
    }
    public void ClearSlot()
    {
        if (occupant != null)//배치된 용병이 있는지 확인
        {
            Destroy(occupant);// 있으면 제거
        }
        occupant = null;/*용병 상태 초기화*/
        isOccupied=false;
        mercenaryIndex=-1;
    }
}
