
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryManager : MonoBehaviour
{
    [Header("보유 용병")]
    public List<Mercenary> ownedMercenaries = new List<Mercenary>();

    [Header("용병 목록")]
    public GameObject mercenaryButtonPrefab;
    public Transform mercenaryList;

    [Header("보유 용병 제한")]
    public int maxMercenaryCount = 20;

    public void AddMercenary(Mercenary mercenary)
    {
        ownedMercenaries.Add(mercenary);
        CreateMercenaryButton(mercenary);
    }

    private void CreateMercenaryButton(Mercenary mercenary)
    {
        GameObject newButton = Instantiate(
            mercenaryButtonPrefab,
            mercenaryList
        );

        Text buttonText = newButton.GetComponentInChildren<Text>();

        buttonText.text = mercenary.mercenaryName +" / " + mercenary.job;
    }
}
