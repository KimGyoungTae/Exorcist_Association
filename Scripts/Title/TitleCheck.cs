// ��� : Ÿ��Ʋ ȭ�� UI üũ -> Ŭ�� �� üũ ȭ�� ǥ��, ���� Ŭ�� �� �� ��ȯ ���� ����
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TitleCheck : MonoBehaviour, IPointerClickHandler

{
    private float newAlphaValue = 1.0f;

    
    private static Image previousClickedImage; // ������ Ŭ���� ������Ʈ�� �����ϱ� ���� ����


    public float doubleClickTimeThreshold = 0.3f; // ����Ŭ�� ���� �ð� ���� (��)
    private float lastClickTime = 0f;

    public BtnType type;

    // ����� ������ �� üũ ǥ��
    public void OnPointerClick(PointerEventData eventData)
    {
        Image image = GetComponent<Image>();

        // ������ Ŭ���� ������Ʈ�� �ִٸ�, �� ������Ʈ�� A���� ���� ���·� �ǵ���
        if (previousClickedImage != null && previousClickedImage != image)
        {
            Color prevColor = previousClickedImage.color;
            prevColor.a = 0.0f; // ������ Alpha ������ �ǵ�����
            previousClickedImage.color = prevColor;
        }

        Color currentColor = image.color;
        currentColor.a = newAlphaValue;

        if (image != null )
        {
            image.color = currentColor;
            previousClickedImage = image; // ���� Ŭ���� ������Ʈ�� ������ Ŭ���� ������Ʈ�� ����
        }


        if (Time.time - lastClickTime < doubleClickTimeThreshold)
        {
            // ����Ŭ�� �߻�
            // ����Ŭ�� �� �ش� ������Ʈ�� �̸��� ����
            OnDoubleClick(image.gameObject.name);
            lastClickTime = 0f; // Ŭ�� �ð� �ʱ�ȭ
        }
        else
        {
            // ù ��° Ŭ�� ����
            lastClickTime = Time.time;
        }
    }


    // ����Ŭ�� �� ����
    private void OnDoubleClick(string objectName)
    {
        //Debug.Log("Double Clicked!");

        // ������Ʈ �̸��� ���� ���ϴ� ������ ����
        if (objectName == "�� �ӹ�")
        {
            type.OnBtnClick();

        }

    }

}
