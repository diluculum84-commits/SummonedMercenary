using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGridManager : MonoBehaviour
{
    public Transform allyArea;
    public Transform enemyArea;
  
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < allyArea.childCount; i++)
        {
            Debug.Log(allyArea.GetChild(i).name);
            BattleSlot slot = allyArea.GetChild(i).GetComponent<BattleSlot>();
            if (slot == null)
            {
                slot = allyArea.GetChild(i).gameObject.AddComponent<BattleSlot>();
            }
            slot.x = i % 6;
            slot.y = i / 6;
            slot.slotTeam = BattleSlot.SlotTeam.Ally;
        }
        for (int i = 0; i < enemyArea.childCount; i++)
        {
            Debug.Log(enemyArea.GetChild(i).name);
            BattleSlot slot = enemyArea.GetChild(i).GetComponent<BattleSlot>();
            if (slot == null)
            {
                slot = enemyArea.GetChild(i).gameObject.AddComponent<BattleSlot>();
            }
            slot.x = i % 6;
            slot.y = i / 6;
            slot.slotTeam = BattleSlot.SlotTeam.Enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
