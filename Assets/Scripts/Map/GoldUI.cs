using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public Text GoldText;
    // Start is called before the first frame update
    void Start()
    {
        if(GoldManager.instance != null)
        {
            GoldManager.instance.ConnectGoldUI(GoldText);
        }
    }
}
