using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPanelManager : MonoBehaviour
{
    public GameObject boxPanel;
    public BoxInteraction currentBox;
    public void CloseBoxPanel()
    {
        boxPanel.SetActive(false);
    }
    public void ReceiveReward()
    {
        if(currentBox == null || currentBox.isRewardClaimed)
        {
            return;
        }

        GoldManager.instance.AddGold(300);
        currentBox.ClaimReward();
        CloseBoxPanel();
        Debug.Log("상자 보상 300G 획득 완료");
    }
}
