using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuBtnType : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
  
    public Button button;
    public Sprite changeImage;
    private Sprite originImage;

    public ButtonLock btnLock;

    public Sprite HoverImage;


    private bool isObjectActive = false;
    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
       Image image = button.GetComponent<Image>();
        
        // �̹��� ������ ��
        if(image != null )
        {
            originImage = image.sprite;
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
            if (image != null && changeImage != null)
            {
                image.sprite = changeImage;
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
        if(button.name == "���� ��ư" || button.name == "�޴���")
        {
            btnLock.Action();
        }

        if(button.name == "�ӹ� ��ø")
        {
            // ���� ���
            isObjectActive = !isObjectActive;
            // ������Ʈ ���¿� ���� Ȱ��/��Ȱ�� ����
            selectedObject.SetActive(isObjectActive);
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
    }




}
