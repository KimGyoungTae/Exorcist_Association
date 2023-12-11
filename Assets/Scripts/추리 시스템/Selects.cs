using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Selects : MonoBehaviour
{
    // 증거 리스트 내에서 항목을 선택 할 수 있다.
    public GameObject[] slots;
    public Sprite[] slotsSprites;
    public string[] currentAnswerData;
    
    [Header("현재 선택한 답 번호")]
    public int currentSlotNumber;

    [Header("질문에 대한 선택한 답을 보여줌.")]
    public Text choiceText;

    [Header("육하원칙 버튼 참조")]
    public FWOHButtonManager fWOHBtn;

    private bool isTabMove = false;

    private Color originalColor;
    private Color originalTextColor;


    private void Awake()
    {
        // slots 배열의 길이를 기반으로 currentAnswerData 배열을 초기화
        // slots 배열의 크기가 변경되면 currentAnswerData 배열도 해당크기로 유동적으로 초기화
        currentAnswerData = new string[slots.Length];
    }

    private void Start()
    {
        originalColor = GetSlotColor(0); // 초기 버튼 색상을 첫 번째 버튼의 색상으로 설정
        originalTextColor = GetSlotTextColor(0);

        // 이 코드는 첫 번째 버튼의 색상을 originalColor로 설정합니다.
        ChangeSlotColor(0, originalColor, 1);
        ChangeSlotTextColor(0, originalTextColor);
    }



    /// <summary>
    /// 슬롯(추리 선택지)의 현재 색상을 가져오는 함수
    /// </summary>
    /// <param name="slotIndex">현재 색상을 저장할 슬롯 인덱스</param>
    /// <returns></returns>
    public Color GetSlotColor(int slotIndex)
    {
        // 슬롯(아이콘)의 Image 컴포넌트를 가져온 후 색상을 반환
        Image slotImage = slots[slotIndex].GetComponent<Image>();
        return slotImage.color;
    }


    
    /// <summary>
    /// 슬롯(추리 선택지) 리스트 내 항목을 선택한다.
    /// 선택된 슬롯의 정보 등을 받아와 변경한다.
    /// </summary>
    /// <param name="slotIndex">현재 클릭한 슬롯 인덱스</param>
    /// <param name="newColor">현재 클릭한 슬롯에게 부여할 색상</param>
    /// <param name="slotSpriteIndex">현재 클릭한 슬롯의 Sprite 인덱스</param>
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
    /// 슬롯(추리 선택지)의 현재 텍스트 색상을 가져오는 함수
    /// </summary>
    /// <param name="slotIndex">현재 텍스트 색상을 저장할 슬롯 인덱스</param>
    /// <returns></returns>
    public Color GetSlotTextColor(int slotIndex)
    {
        Text buttonText = slots[slotIndex].GetComponentInChildren<Text>();
        return buttonText.color;
    }



    /// <summary>
    /// 선택된 슬롯의 텍스트 정보 등을 받아와 변경한다.
    /// </summary>
    /// <param name="slotIndex">현재 클릭한 슬롯 인덱스</param>
    /// <param name="newColor">현재 클릭한 슬롯의 텍스트에 부여할 색상</param>
    public void ChangeSlotTextColor(int slotIndex, Color newColor)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            // 클릭한 버튼에서 Text 컴포넌트 찾기
            Text buttonText = slots[slotIndex].GetComponentInChildren<Text>();

            if (buttonText != null)
            {
                buttonText.color = newColor;
            }
            else
            {
                Debug.LogError("클릭한 버튼 자식 오브젝트에 Text 컴포넌트가 없습니다.");
            }
        }

        else
        {
            Debug.LogError("유효하지 않은 버튼 인덱스입니다.");
        }
    }



    /// <summary>
    /// 탭 이동마다 선택지 데이터가 갱신된다.
    /// 그 데이터를 기반으로 슬롯 선택 시 선택한 데이터가 텍스트로 보여지게 된다.
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
      //  Debug.Log("데이터 갱신");
    }

    private void OnDisable()
    {
        TabButtonController.OnAnswerUpdated -= UpdateChoiceText;
      //  Debug.Log("반납? ");
    }



    /// <summary>
    /// 탭 이동마다 선택지 데이터가 갱신된다.
    /// 데이터가 갱신될 때 이전 탭에서 선택한 데이터 정보(텍스트, 색상 등)을 초기화 한다.
    /// </summary>
    /// <param name="answersText">현재 클릭한 탭에 대한 선택지 데이터 정보들</param>
    void UpdateChoiceText(string[] answersText) 
    {
        isTabMove = true;
      // Debug.Log(isTabMove);  ==> True

        if(isTabMove == true)
        {
            // 여기에서 기존 클릭한 버튼 색깔 초기화
            for (int i = 0; i < slots.Length; i++)
            {
                ChangeSlotColor(i, originalColor, 1); // originalColor로 버튼 색상 초기화
                ChangeSlotTextColor(i, originalTextColor);
            }

            // 여기에서 Text 초기화
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
                // 탭 마다 정해진 데이터 : answerText
                // 탭에 보여질 최대 데이터 개수(슬롯의 길이에 따라 달라짐) : cuurrentAnswerData
                // answersText currentAnswerData 배열이 더 큰 경우,
                // 즉, 탭에 보여줄 데이터 개수가 비는 경우에는 빈 문자열 또는 다른 값으로 초기화 한다.
                currentAnswerData[i] = ""; // 또는 다른 기본 값으로 초기화
            }
        }

       isTabMove = false;
      // Debug.Log(isTabMove);    ==> false
    }
}
