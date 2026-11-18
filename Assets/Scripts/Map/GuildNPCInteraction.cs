using UnityEngine;

public class GuildNPCInteraction : MonoBehaviour
{
    public GameObject GuildNPCPanel;
    public GameObject NPCinteractionkey;
    public GameObject GuildMercenaryPanel; //용병 구매 패널
    public MercenaryInformation mercenaryInformation; //용병 정보 패널
    public GameObject GuildCheckPanel; //용병 구매 패널
    public MercenaryInventory Mercenaryinventory; //용병 확인 인벤토리

    private bool isPlayerNearby;

    void Start()
    {
        GuildNPCPanel.SetActive(false);
        NPCinteractionkey.SetActive(false);
        GuildMercenaryPanel.SetActive(false);
        GuildCheckPanel.SetActive(false);
    }
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            GuildNPCPanel.SetActive(true);
            NPCinteractionkey.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            NPCinteractionkey.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            GuildNPCPanel.SetActive(false);
            NPCinteractionkey.SetActive(false);
            CloseGuild();
        }
    }
    public void OpenGuild()
    {
        GuildMercenaryPanel.SetActive(true);
        GuildNPCPanel.SetActive(false);
    }
    public void OpenBuyCheck() //구매버튼 활성화
    {
        GuildCheckPanel.SetActive(true);
    }
    public void CloseBuyCheck() //취소버튼 활성화
    {
        GuildCheckPanel.SetActive(false);
    }
    public void CloseGuild()
    {
        GuildNPCPanel.SetActive(false);
        GuildMercenaryPanel.SetActive(false);
        if (mercenaryInformation.gameObject.activeSelf)
        {
            mercenaryInformation.CloseMercenaryInformation();
        }
    }
    public void ClosePanel()
    {
        GuildNPCPanel.SetActive(false);

        if (isPlayerNearby)
        {
            NPCinteractionkey.SetActive(true);
        }
    }
    public void MercenaryConfirmBuy()//용병 구매 확인 패널
    {
        Mercenary mercenary = mercenaryInformation.selectedMercenary;//선택한 용병 가져오기

        if(mercenary == null)
        {
            Debug.LogWarning("선택된 용병이 없습니다.");
            return;
        }
        if(!GoldManager.instance.SpendGold(mercenary.price))//골드차감
        {
            return;
        }

        Mercenaryinventory.AddMercenary(mercenary);
        GuildCheckPanel.SetActive(false);
    }
}