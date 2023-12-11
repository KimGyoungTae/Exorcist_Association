using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Selects : MonoBehaviour
{
    // ���� ����Ʈ ������ �׸��� ���� �� �� �ִ�.
    public GameObject[] slots;
    public Sprite[] slotsSprites;
    public string[] currentAnswerData;
    
    [Header("���� ������ �� ��ȣ")]
    public int currentSlotNumber;

    [Header("������ ���� ������ ���� ������.")]
    public Text choiceText;

    [Header("���Ͽ�Ģ ��ư ����")]
    public FWOHButtonManager fWOHBtn;

    private bool isTabMove = false;

    private Color originalColor;
    private Color originalTextColor;


    private void Awake()
    {
        // slots �迭�� ���̸� ������� currentAnswerData �迭�� �ʱ�ȭ
        // slots �迭�� ũ�Ⱑ ����Ǹ� currentAnswerData �迭�� �ش�ũ��� ���������� �ʱ�ȭ
        currentAnswerData = new string[slots.Length];
    }

    private void Start()
    {
        originalColor = GetSlotColor(0); // �ʱ� ��ư ������ ù ��° ��ư�� �������� ����
        originalTextColor = GetSlotTextColor(0);

        // �� �ڵ�� ù ��° ��ư�� ������ originalColor�� �����մϴ�.
        ChangeSlotColor(0, originalColor, 1);
        ChangeSlotTextColor(0, originalTextColor);
    }



    /// <summary>
    /// ����(�߸� ������)�� ���� ������ �������� �Լ�
    /// </summary>
    /// <param name="slotIndex">���� ������ ������ ���� �ε���</param>
    /// <returns></returns>
    public Color GetSlotColor(int slotIndex)
    {
        // ����(������)�� Image ������Ʈ�� ������ �� ������ ��ȯ
        Image slotImage = slots[slotIndex].GetComponent<Image>();
        return slotImage.color;
    }


    
    /// <summary>
    /// ����(�߸� ������) ����Ʈ �� �׸��� �����Ѵ�.
    /// ���õ� ������ ���� ���� �޾ƿ� �����Ѵ�.
    /// </summary>
    /// <param name="slotIndex">���� Ŭ���� ���� �ε���</param>
    /// <param name="newColor">���� Ŭ���� ���Կ��� �ο��� ����</param>
    /// <param name="slotSpriteIndex">���� Ŭ���� ������ Sprite �ε���</param>
    public void ChangeSlotColor(int slotIndex, Color newColor, int slotSpriteIndex)
    {
        GameObject slot = slots[slotIndex];
        Image slotImage = slot.GetComponent<Image>();
        slotImage.color = newColor;
        slotImage.sprite = slotsSprites[slotSpriteIndex];

        currentSlotNumber = slotIndex;
        ChangeChoiceText();
    }


    /// <summary>
    /// ����(�߸� ������)�� ���� �ؽ�Ʈ ������ �������� �Լ�
    /// </summary>
    /// <param name="slotIndex">���� �ؽ�Ʈ ������ ������ ���� �ε���</param>
    /// <returns></returns>
    public Color GetSlotTextColor(int slotIndex)
    {
        Text buttonText = slots[slotIndex].GetComponentInChildren<Text>();
        return buttonText.color;
    }



    /// <summary>
    /// ���õ� ������ �ؽ�Ʈ ���� ���� �޾ƿ� �����Ѵ�.
    /// </summary>
    /// <param name="slotIndex">���� Ŭ���� ���� �ε���</param>
    /// <param name="newColor">���� Ŭ���� ������ �ؽ�Ʈ�� �ο��� ����</param>
    public void ChangeSlotTextColor(int slotIndex, Color newColor)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            // Ŭ���� ��ư���� Text ������Ʈ ã��
            Text buttonText = slots[slotIndex].GetComponentInChildren<Text>();

            if (buttonText != null)
            {
                buttonText.color = newColor;
            }
            else
            {
                Debug.LogError("Ŭ���� ��ư �ڽ� ������Ʈ�� Text ������Ʈ�� �����ϴ�.");
            }
        }

        else
        {
            Debug.LogError("��ȿ���� ���� ��ư �ε����Դϴ�.");
        }
    }



    /// <summary>
    /// �� �̵����� ������ �����Ͱ� ���ŵȴ�.
    /// �� �����͸� ������� ���� ���� �� ������ �����Ͱ� �ؽ�Ʈ�� �������� �ȴ�.
    /// </summary>
    public void ChangeChoiceText()
    {
        
        for (int i = 0; i < currentAnswerData.Length; i++)
        {
            if (currentSlotNumber == i)
            {
                choiceText.text = currentAnswerData[i];
            }
        }
    }

    private void OnEnable()
    {
        TabButtonController.OnAnswerUpdated += UpdateChoiceText;
      //  Debug.Log("������ ����");
    }

    private void OnDisable()
    {
        TabButtonController.OnAnswerUpdated -= UpdateChoiceText;
      //  Debug.Log("�ݳ�? ");
    }



    /// <summary>
    /// �� �̵����� ������ �����Ͱ� ���ŵȴ�.
    /// �����Ͱ� ���ŵ� �� ���� �ǿ��� ������ ������ ����(�ؽ�Ʈ, ���� ��)�� �ʱ�ȭ �Ѵ�.
    /// </summary>
    /// <param name="answersText">���� Ŭ���� �ǿ� ���� ������ ������ ������</param>
    void UpdateChoiceText(string[] answersText) 
    {
        isTabMove = true;
      // Debug.Log(isTabMove);  ==> True

        if(isTabMove == true)
        {
            // ���⿡�� ���� Ŭ���� ��ư ���� �ʱ�ȭ
            for (int i = 0; i < slots.Length; i++)
            {
                ChangeSlotColor(i, originalColor, 1); // originalColor�� ��ư ���� �ʱ�ȭ
                ChangeSlotTextColor(i, originalTextColor);
            }

            // ���⿡�� Text �ʱ�ȭ
            choiceText.text = "";
        }

       for(int i = 0; i < answersText.Length; i++)
        {
            if (i < answersText.Length)
            {
                currentAnswerData[i] = answersText[i];
            }
            else
            {
                // �� ���� ������ ������ : answerText
                // �ǿ� ������ �ִ� ������ ����(������ ���̿� ���� �޶���) : cuurrentAnswerData
                // answersText currentAnswerData �迭�� �� ū ���,
                // ��, �ǿ� ������ ������ ������ ��� ��쿡�� �� ���ڿ� �Ǵ� �ٸ� ������ �ʱ�ȭ �Ѵ�.
                currentAnswerData[i] = ""; // �Ǵ� �ٸ� �⺻ ������ �ʱ�ȭ
            }
        }

       isTabMove = false;
      // Debug.Log(isTabMove);    ==> false
    }
}
