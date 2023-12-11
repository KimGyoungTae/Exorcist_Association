using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �� ��ư�� ������ �ľ�. ==> ���������� who, When, Where, How, What, EvilInform ����
public enum TabType
{
    Who, When, Where, How, What, EvilInform, None
}

public class TabButtonController : MonoBehaviour
{
    public GameObject AnswerOffCollect;
    public GameObject[] tabUIObjects;

    // UI Text �迭�� �����ؾ� �մϴ�. Answer1���� Answer7���� ǥ���� UI Text �迭
    public Text[] AnswerTexts; 

    public TabType CurrentTab { get; private set; }  // ���� �� ����
    public List<TabType> ConfirmedTabs = new List<TabType>(5); // Ȯ���� �� ���

    public TextTapDataManager tabDataManager; // TextTapDataManager ��ũ��Ʈ�� ���� ����

    // �ٸ� ��ũ��Ʈ�� ��������� �˸� �̺�Ʈ ����
    public delegate void AnswerUpdated(string[] answers);
    public static event AnswerUpdated OnAnswerUpdated;

  
    private void Start()
    {
        // �ʱ� ���� �����ϰų� �ʿ信 ���� ���ϴ� ���� �����մϴ�.
        CurrentTab = TabType.Who;
    }


    public void ConfirmTab()
    {
        // ���� ���� Ȯ���� �� ��Ͽ� �߰�
        ConfirmedTabs.Add(CurrentTab);


        // Ȯ���� �� ��ư ���� ����
        float normalized_R = 63f / 255f;
        float normalized_G = 63f / 255f;
        float normalized_B = 63f / 255f;

        Color customGrayColor = new Color(normalized_R, normalized_G, normalized_B);
        ConfirmManager.Instance.confirmTabButtons[(int)CurrentTab].image.color = customGrayColor;
    }

    // OnClick�� ������ �޼���
    public void OnTabButtonClick(int tabIdx)
    {

        TabType clickedTab = (TabType)tabIdx;

        if (ConfirmedTabs.Contains(clickedTab))
        {
            // �̹� Ȯ���� ���� ��� �ƹ��͵� ���� ����
            return;
        }

        // Ŭ���� �� ��ư�� Ȯ���� �ǰ� �ٸ� ���� ����
        // Ŭ���� �� ��ư�� ���� ���� ���� �����ϰ� �ش� ���� �ؽ�Ʈ�� ǥ���մϴ�.
        CurrentTab = clickedTab;
        ShowTextForCurrentTab();
        // Debug.Log("�� Ŭ�� ��.");

    }


    //���� Ŭ���� ���� ������ �ľ��Ͽ� �ش��ϴ� Question(������)�� �޾ƿ´�.
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
    /// ���� Ŭ���� �ǿ� ���� ������ �����͸� ������ ������ �˸���.
    /// </summary>
    void SetTabButtonData()
    {
        string[] answers = tabDataManager.GetAnswersForTab(CurrentTab);

        for (int i = 0; i < answers.Length; i++)
        {
            AnswerTexts[i].text = answers[i];
        }

        // �� ��ư���� �̵� �� ������ 
        // �̺�Ʈ�� ȣ���Ͽ� ������ ������ �˸��ϴ�.
        if (OnAnswerUpdated != null)
        {
           // Debug.Log("�� ������ ���� ��.");
            OnAnswerUpdated(answers);
        }
        
    }
}
