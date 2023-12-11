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

    private int correctCount = 0; // ���� ������ �����ϴ� ����
    [HideInInspector] public bool ActiveFinishButton = false;

    private void Start()
    {
        selectManage = FindObjectOfType<Selects>();
        tabBtnManager = FindObjectOfType<TabButtonController>();
    }


    public void ConfirmAnswer()
    {
        tabBtnManager.ConfirmTab();

        // �� ���� ������ �Ǻ��� �� ����� ����
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
            // Ȯ���� ���� ������ ��� ���� �� "�����ϱ�" ��ư Ȱ��ȭ
            ActiveFinishButton = true;
        }

        // ���� Ȯ�� �� Ȯ���ϱ�.
       // Debug.Log("Confirmed Tab: " + currentConfirmTab);
    }



  
    /// <summary>
    /// ������ üũ�Ѵ�. ������ �� correctCount ����
    /// </summary>
    /// <param name="currentTab">���� ������ üũ�� �� ����</param>
    /// <param name="selectedOptionIndex">� ������ �����ߴ� ���� ���� �ε���</param>
    private void CheckAnswer(TabType currentTab, int selectedOptionIndex)
    {
        // �ҸӴ�
        if (currentTab == TabType.Who && selectedOptionIndex == 5)
        {
            correctCount++;
        }
        // 6���� ��
        else if (currentTab == TabType.When && selectedOptionIndex == 2)
        {
            correctCount++;
        }
        // ���ð�
        else if (currentTab == TabType.Where && selectedOptionIndex == 3)
        {
            correctCount++;
        }
        // ���帶��
        else if (currentTab == TabType.How && selectedOptionIndex == 3)
        {
            correctCount++;
        }
        // �簳��
        else if (currentTab == TabType.What && selectedOptionIndex == 0)
        {
            correctCount++;
        }
      
        else Debug.Log("Incorrect Answer");
    }



    /// <summary>
    /// �� �ǿ��� Ȯ���� ������ ����� �ǿ� ��ϵ�.
    /// </summary>
    /// <param name="currentTab">������ ����� �� ����</param>
    /// <param name="TabAnswer">������� ��ϵ� ���� </param>
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
