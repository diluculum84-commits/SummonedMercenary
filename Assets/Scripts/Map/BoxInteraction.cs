using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public GameObject boxPanel;
    public GameObject interactionKey;
    private bool isPlayerNearby = false;
    public bool isRewardClaimed = false;
    public BoxPanelManager boxPanelManager;
    void Start()
    {
        interactionKey.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)//들어올때
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionKey.SetActive(true);
            Debug.Log("상자의 감지 영역에 들어왔습니다.");
        }      
    }
    private void OnTriggerExit2D(Collider2D other)//벗어날때
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionKey.SetActive(false);
            Debug.Log("상자의 감지 영역에서 벗어났습니다.");
        }
    }
    void Update()
    {
        if(isPlayerNearby && !isRewardClaimed && Input.GetKeyDown(KeyCode.E))
        {
            boxPanelManager.currentBox = this;

            Debug.Log("상자 E키 입력 성공");

            boxPanel.SetActive(true);
        }
    }
    public void ClaimReward()
    {
        isRewardClaimed = true;
        isPlayerNearby = false;
        interactionKey.SetActive(false);
    }
}