using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 탭 버튼의 종류를 파악. ==> 열거형으로 who, When, Where, How, What, EvilInform 정의
public enum TabType
{
    Who, When, Where, How, What, EvilInform, None
}

public class TabButtonController : MonoBehaviour
{
    public GameObject AnswerOffCollect;
    public GameObject[] tabUIObjects;

    // UI Text 배열을 연결해야 합니다. Answer1부터 Answer7까지 표시할 UI Text 배열
    public Text[] AnswerTexts; 

    public TabType CurrentTab { get; private set; }  // 현재 탭 정보
    public List<TabType> ConfirmedTabs = new List<TabType>(5); // 확정된 탭 목록

    public TextTapDataManager tabDataManager; // TextTapDataManager 스크립트에 대한 참조

    // 다른 스크립트에 변경사항을 알릴 이벤트 정의
    public delegate void AnswerUpdated(string[] answers);
    public static event AnswerUpdated OnAnswerUpdated;

  
    private void Start()
    {
        // 초기 탭을 설정하거나 필요에 따라 원하는 탭을 선택합니다.
        CurrentTab = TabType.Who;
    }


    public void ConfirmTab()
    {
        // 현재 탭을 확정된 탭 목록에 추가
        ConfirmedTabs.Add(CurrentTab);


        // 확정된 탭 버튼 색상 변경
        float normalized_R = 63f / 255f;
        float normalized_G = 63f / 255f;
        float normalized_B = 63f / 255f;

        Color customGrayColor = new Color(normalized_R, normalized_G, normalized_B);
        ConfirmManager.Instance.confirmTabButtons[(int)CurrentTab].image.color = customGrayColor;
    }

    // OnClick에 응답할 메서드
    public void OnTabButtonClick(int tabIdx)
    {

        TabType clickedTab = (TabType)tabIdx;

        if (ConfirmedTabs.Contains(clickedTab))
        {
            // 이미 확정된 탭인 경우 아무것도 하지 않음
            return;
        }

        // 클릭한 탭 버튼이 확정된 탭과 다를 때만 동작
        // 클릭한 탭 버튼에 따라 현재 탭을 변경하고 해당 탭의 텍스트를 표시합니다.
        CurrentTab = clickedTab;
        ShowTextForCurrentTab();
        // Debug.Log("탭 클릭 됨.");

    }


    //현재 클릭한 탭의 종류를 파악하여 해당하는 Question(질문지)을 받아온다.
    void ShowTextForCurrentTab()
    {
        foreach (GameObject obj in tabUIObjects)
        {
            obj.SetActive(false);

            if (CurrentTab == TabType.EvilInform)
            {
                AnswerOffCollect.SetActive(false);
            }
            else AnswerOffCollect.SetActive(true);
        }

     
        tabUIObjects[(int)CurrentTab].SetActive(true);
        SetTabButtonData();
    }


    /// <summary>
    /// 현재 클릭한 탭에 대한 선택지 데이터를 가져와 갱신을 알린다.
    /// </summary>
    void SetTabButtonData()
    {
        string[] answers = tabDataManager.GetAnswersForTab(CurrentTab);

        for (int i = 0; i < answers.Length; i++)
        {
            AnswerTexts[i].text = answers[i];
        }

        // 탭 버튼으로 이동 될 때마다 
        // 이벤트를 호출하여 데이터 갱신을 알립니다.
        if (OnAnswerUpdated != null)
        {
           // Debug.Log("탭 데이터 갱신 됨.");
            OnAnswerUpdated(answers);
        }
        
    }
}
