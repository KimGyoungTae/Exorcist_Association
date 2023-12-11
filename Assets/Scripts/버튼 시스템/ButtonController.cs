using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// �� ������ ��ư�� ���� ������ ������ ��ư ���� ���� ������.
/// </summary>
public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [Header("��ư ���� ��ȭ ����")]
    [SerializeField] private Button button;
    [SerializeField] private Sprite HoverImage;
    [SerializeField] private Transform buttonScale;
    private Sprite originImage;
    private Vector3 defaultScale;

    [SerializeField] private ButtonLock btnLock;

    
    void Start()
    {
        Image image = button.GetComponent<Image>();

        // �̹��� ������ ��
        if (image != null) { originImage = image.sprite; }

        if (buttonScale != null)
        {
            // ó�� ũ�� ����
            defaultScale = buttonScale.localScale;
        }

        else
        {
            Debug.LogWarning("buttonScale is not assigned.");
        }
    }


    // ��ư�� ������ �� ȣ��Ǵ� �Լ��Դϴ�.
    // ��ư�� Ŭ�� ���� �� Image ����
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��ư�� Image ������Ʈ�� ã�Ƽ� ������ ���� �̹����� �����մϴ�.
        if (button != null)
        {
            Image image = button.GetComponent<Image>();
            if (image != null && HoverImage != null)
            {
                image.sprite = HoverImage;
            }
        }
    }


    // ��ư���� ���� ���� �� ȣ��Ǵ� �Լ��Դϴ�.
    public void OnPointerUp(PointerEventData eventData)
    {
        // ��ư�� Image ������Ʈ�� ã�Ƽ� ���� �̹����� �����մϴ�.
        if (button != null)
        {
            Image image = button.GetComponent<Image>();
            if (image != null && originImage != null)
            {
                image.sprite = originImage;
            }
        }
    }


    // ��Ȯ�� ��ư�� Ŭ�� ���� �� ����� �ҷ�����
    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log(button.name + " ��� ǥ��");
        if (button.name == "���� ��ư" || button.name == "�޴���" || button.name == "�ӹ� ��ø")
        {
            btnLock.Action();
        }
    }



    // ���콺 ������ ����� �� Hover �̹��� ��ü
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ư�� Image ������Ʈ�� ã�Ƽ� ������ ���� �̹����� �����մϴ�.
        if (button != null)
        {
            Image image = button.GetComponent<Image>();
            if (image != null && HoverImage != null)
            {
                image.sprite = HoverImage;
            }
        }

        if(buttonScale != null)
        {
            // ��ư ũ�� �ø���
            buttonScale.localScale = defaultScale * 1.2f;
        }
        
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ư�� Image ������Ʈ�� ã�Ƽ� ���� �̹����� �����մϴ�.
        if (button != null)
        {
            Image image = button.GetComponent<Image>();
            if (image != null && originImage != null)
            {
                image.sprite = originImage;
            }
        }

        if (buttonScale != null)
        {
            // ó�� ũ��� �ǵ��ư���
            buttonScale.localScale = defaultScale;
        }
    }
}
