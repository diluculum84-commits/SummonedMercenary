using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryInformation : MonoBehaviour
{
    public Text mercenaryNameText;
    public Text JOB;
    public Text Hp;
    public Text Attack;
    public Text Defense;
    public Text mercenaryPriceText;
    public Mercenary selectedMercenary;
    private MercenaryData selectedOwnedMercenary;
    // Start is called before the first frame update
    void Start()
    {
       gameObject.SetActive(false);
    }
    public void CloseMercenaryInformation()
    {
        gameObject.SetActive(false);
        selectedMercenary = null;
    }
    public void ShowMercenaryInformation(Mercenary mercenary)//상점에서 용병 정보 확인
    {
        if (selectedMercenary == mercenary && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            selectedMercenary = null;
            return;
        }
        selectedMercenary = mercenary;
        gameObject.SetActive(true);
        gameObject.transform.position = mercenary.transform.position;
        mercenaryNameText.text = "이름 : " + mercenary.mercenaryName;
        JOB.text = "직업 : " + mercenary.job;
        mercenaryPriceText.text = "가격 : " + mercenary.price;
        Hp.text = "HP : " + mercenary.maxHP;
        Attack.text = "공격력 : " + mercenary.attack;
        Defense.text = "방어력 : " + mercenary.defense;
    }
    public void ShowOwnedMercenaryInformation(MercenaryData data, RectTransform slot)//인벤에서 용병 정보 확인
    {
        if(selectedOwnedMercenary == data && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            selectedOwnedMercenary = null;
            return;
        }
        selectedOwnedMercenary = data;

        gameObject.SetActive(true);
        RectTransform informationRect = GetComponent<RectTransform>();
        informationRect.position = slot.position + Vector3.right * (slot.rect.width + informationRect.rect.width / 2f);
        mercenaryNameText.text = "이름 : " + data.mercenaryName;
        JOB.text = "직업 : " + data.job;
        Hp.text = "HP : " + data.currentHP + " / " + data.maxHP;
        Attack.text = "공격력 : " + data.attack;
        Defense.text = "방어력 : " + data.defense;

        mercenaryPriceText.gameObject.SetActive(false);
    }
    public void BuyMercenary()
    {
        if (selectedMercenary == null)
        {
            Debug.Log("선택된 용병이 없습니다.");
            return;
        }

        Debug.Log("구매할 용병 : " + selectedMercenary.mercenaryName);
    }

}
