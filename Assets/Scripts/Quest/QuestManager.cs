using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<QuestData> questList = new List<QuestData>(); //전체 의뢰목록
    public List<QuestData> currentQuests = new List<QuestData>();//표시되는 9개 의뢰 목록
    public List<Text> questName = new List<Text>();
    public GameObject noticePanel;
    public GameObject questBoardPanel;
    public GameObject questListPanel;
    public GameObject difficultyPanel;
    public QuestData selectedQuest;//의뢰 선택
    public int selectedDifficulty; //선택 난이도
    public int selectedMonsterCount; //선택 의뢰의 몬스터 수
    public CanvasGroup questListCanvasGroup;
    public void GenerateRandomQuests()//의뢰 목록 랜덤 생성
    {
        currentQuests.Clear();//현재 의뢰 초기화
        //선택 의뢰를 현재 의뢰목록에 추가
        int targetCount = Mathf.Min(9, questList.Count);
        while (currentQuests.Count < targetCount)
        {
            int randomIndex = Random.Range(0, questList.Count);
            if (!currentQuests.Contains(questList[randomIndex]))
            {
                currentQuests.Add(questList[randomIndex]);
            }
        }
        Debug.Log("생성된 의뢰 수 : " + currentQuests.Count);
    }
    public void OpenQuestBoard()
    {
        noticePanel.SetActive(false);
        questBoardPanel.SetActive(true);
        questListPanel.SetActive(true);
    }
    public void CloseQuestBoard()
    {
        noticePanel.SetActive(false);
        questBoardPanel.SetActive(false);
        questListPanel.SetActive(false);
    }
    public void OpenDifficultyPanel()
    {
        difficultyPanel.SetActive(true);
    }
    public void CloseDifficultyPanel()
    {
        difficultyPanel.SetActive(false);
        questListCanvasGroup.interactable = true;
    }
    public void SelectQuest(int index)
    {
        selectedQuest = currentQuests[index];
        Debug.Log("선택한 의뢰 : " + selectedQuest.questName);
        questListCanvasGroup.interactable = false;
        difficultyPanel.SetActive(true);
    }
    public void SelectDifficulty(int difficylty)
    {
        selectedDifficulty = difficylty;
        if (selectedDifficulty == 1)
        {
            selectedMonsterCount = 2;
        }
        else if (selectedDifficulty == 2)
        {
            selectedMonsterCount = 3;
        }
        else if (selectedDifficulty == 3)
        {
            selectedMonsterCount = 5;
        }
        BattleData.instance.monsterName = selectedQuest.monsterName;
        BattleData.instance.monsterCount = selectedMonsterCount;
        BattleData.instance.difficulty = selectedDifficulty;
        EnterBattleScene();
        Debug.Log("선택한 몬스터 수 : " + selectedMonsterCount);
    }
    void Start()
    {
        questListPanel.SetActive(false);
        difficultyPanel.SetActive(false);
        questBoardPanel.SetActive(false);
        GenerateRandomQuests();
        for (int i = 0; i < Mathf.Min(questName.Count, currentQuests.Count); i++)
        {
            questName[i].text = currentQuests[i].questName;
        }
    }
    public void EnterBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
