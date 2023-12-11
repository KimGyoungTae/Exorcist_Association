using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmManager : MonoBehaviour
{

    private static ConfirmManager instance;
    public static ConfirmManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ConfirmManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ConfirmManager");
                    instance = obj.AddComponent<ConfirmManager>();
                }
            }
            return instance;
        }
    }

    public TabType GetConfirmedTab
    {
        get { return tabBtnManager.CurrentTab; }
    }

    public int GetCorrectCount
    {
        get { return correctCount; }
    }

    private Selects selectManage;
    private TabButtonController tabBtnManager;
    
    public Button[] confirmTabButtons;
    public Text[] confirmAnswerTexts;

    private int correctCount = 0; // 정답 개수를 추적하는 변수
    [HideInInspector] public bool ActiveFinishButton = false;

    private void Start()
    {
        selectManage = FindObjectOfType<Selects>();
        tabBtnManager = FindObjectOfType<TabButtonController>();
    }


    public void ConfirmAnswer()
    {
        tabBtnManager.ConfirmTab();

        // 탭 마다 정답을 판별할 때 사용할 예정
        int getSelectNumber = selectManage.currentSlotNumber;

        //TabType currentConfirmTab = tabBtnManager.CurrentTab;
        TabType currentConfirmTab = GetConfirmedTab;

        CheckAnswer(currentConfirmTab, getSelectNumber);
       // Debug.Log("Correct Answer! Count: " + correctCount);

        string currentTabAnswer = selectManage.currentAnswerData[getSelectNumber];
       // Debug.Log(selectManage.currentAnswerData[getSelectNumber]);

        ReflectAnswer(currentConfirmTab, currentTabAnswer);

        if (tabBtnManager.ConfirmedTabs.Count == tabBtnManager.ConfirmedTabs.Capacity) 
        {
            // 확정한 탭의 개수가 모두 충족 시 "제출하기" 버튼 활성화
            ActiveFinishButton = true;
        }

        // 현재 확정 탭 확인하기.
       // Debug.Log("Confirmed Tab: " + currentConfirmTab);
    }



  
    /// <summary>
    /// 정답을 체크한다. 정답일 시 correctCount 증가
    /// </summary>
    /// <param name="currentTab">현재 정답을 체크할 탭 정보</param>
    /// <param name="selectedOptionIndex">어떤 정보를 선택했는 지에 대한 인덱스</param>
    private void CheckAnswer(TabType currentTab, int selectedOptionIndex)
    {
        // 할머니
        if (currentTab == TabType.Who && selectedOptionIndex == 5)
        {
            correctCount++;
        }
        // 6개월 전
        else if (currentTab == TabType.When && selectedOptionIndex == 2)
        {
            correctCount++;
        }
        // 주택가
        else if (currentTab == TabType.Where && selectedOptionIndex == 3)
        {
            correctCount++;
        }
        // 심장마비
        else if (currentTab == TabType.How && selectedOptionIndex == 3)
        {
            correctCount++;
        }
        // 재개발
        else if (currentTab == TabType.What && selectedOptionIndex == 0)
        {
            correctCount++;
        }
      
        else Debug.Log("Incorrect Answer");
    }



    /// <summary>
    /// 각 탭에서 확정한 데이터 답안지 탭에 기록됨.
    /// </summary>
    /// <param name="currentTab">정답을 기록한 탭 정보</param>
    /// <param name="TabAnswer">답안지에 기록될 정보 </param>
    private void ReflectAnswer(TabType currentTab, string TabAnswer)
    {
        switch(currentTab)
        {
            case TabType.Who:
                confirmAnswerTexts[(int)currentTab].text = TabAnswer;
                break;
            case TabType.When:
                confirmAnswerTexts[(int)currentTab].text = TabAnswer;
                break;
            case TabType.Where:
                confirmAnswerTexts[(int)currentTab].text = TabAnswer;
                break;
            case TabType.How:
                confirmAnswerTexts[(int)currentTab].text = TabAnswer;
                break;
            case TabType.What:
                confirmAnswerTexts[(int)currentTab].text = TabAnswer;
                break;
        }
    }
}
