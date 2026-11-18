using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeBoardInteraction : MonoBehaviour
{
    public GameObject NoticePanel;
    public GameObject NPCinteractionkey;

    private bool isPlayerNearby;

    public void ClosePanel() // 의뢰게시판 닫기
    {
        NoticePanel.SetActive(false);
        NPCinteractionkey.SetActive(true);
    }
    void Start()
    {
        NoticePanel.SetActive(false);
        NPCinteractionkey.SetActive(false);
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

            if (NoticePanel != null)
            {
                NoticePanel.SetActive(false);
            }

            if (NPCinteractionkey != null)
            {
                NPCinteractionkey.SetActive(false);
            }
        }
    }
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            NoticePanel.SetActive(true);
            NPCinteractionkey.SetActive(false);
        }
    }
}