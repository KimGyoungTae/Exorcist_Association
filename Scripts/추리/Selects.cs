using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selects : MonoBehaviour
{
    // 증거 리스트 내에서 항목을 선택 할 수 있다.
    public GameObject[] slots;


    // 슬롯(아이콘)의 현재 색상을 가져오는 함수
    public Color GetSlotColor(int slotIndex)
    {
        // 슬롯(아이콘)의 Image 컴포넌트를 가져온 후 색상을 반환
        Image slotImage = slots[slotIndex].GetComponent<Image>();
        return slotImage.color;
    }


    // 증거 리스트 내 항목을 선택할 수 있다.
    public void ChangeSlotColor(int slotIndex, Color newColor)
    {
        GameObject slot = slots[slotIndex];
        Image slotImage = slot.GetComponent<Image>();
        slotImage.color = newColor;
    }

    // 리스트 내 아이템(증거) 선택 시 육하원칙 공간에 그 증거가 반영된다.
    public void ChangeChoiceText(string newText)
    {
     //   Button button = GetComponentInChildren<Button>();
        Text buttonText = GetComponent<Text>();
       // button.image.color = newColor;
        buttonText.text = newText;
    }
}
