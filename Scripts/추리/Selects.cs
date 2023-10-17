using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selects : MonoBehaviour
{
    // ���� ����Ʈ ������ �׸��� ���� �� �� �ִ�.
    public GameObject[] slots;


    // ����(������)�� ���� ������ �������� �Լ�
    public Color GetSlotColor(int slotIndex)
    {
        // ����(������)�� Image ������Ʈ�� ������ �� ������ ��ȯ
        Image slotImage = slots[slotIndex].GetComponent<Image>();
        return slotImage.color;
    }


    // ���� ����Ʈ �� �׸��� ������ �� �ִ�.
    public void ChangeSlotColor(int slotIndex, Color newColor)
    {
        GameObject slot = slots[slotIndex];
        Image slotImage = slot.GetComponent<Image>();
        slotImage.color = newColor;
    }

    // ����Ʈ �� ������(����) ���� �� ���Ͽ�Ģ ������ �� ���Ű� �ݿ��ȴ�.
    public void ChangeChoiceText(string newText)
    {
     //   Button button = GetComponentInChildren<Button>();
        Text buttonText = GetComponent<Text>();
       // button.image.color = newColor;
        buttonText.text = newText;
    }
}
