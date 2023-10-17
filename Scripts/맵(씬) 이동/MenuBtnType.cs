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
        
        // 이미지 존재할 시
        if(image != null )
        {
            originImage = image.sprite;
        }
    }


    // 버튼을 눌렀을 때 호출되는 함수입니다.
    // 버튼을 클릭 했을 때 Image 변경
    public void OnPointerDown(PointerEventData eventData)
    {
        // 버튼의 Image 컴포넌트를 찾아서 눌렸을 때의 이미지로 변경합니다.
        if (button != null)
        {
            Image image = button.GetComponent<Image>();
            if (image != null && changeImage != null)
            {
                image.sprite = changeImage;
            }
        }
    }


    // 버튼에서 손을 땠을 때 호출되는 함수입니다.
    public void OnPointerUp(PointerEventData eventData)
    {
        // 버튼의 Image 컴포넌트를 찾아서 원래 이미지로 변경합니다.
        if (button != null)
        {
            Image image = button.GetComponent<Image>();
            if (image != null && originImage != null)
            {
                image.sprite = originImage;
            }
        }
    }


    // 정확히 버튼을 클릭 했을 시 목록을 불러오기
    public void OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log(button.name + " 목록 표시");
        if(button.name == "지도 버튼" || button.name == "메뉴명")
        {
            btnLock.Action();
        }

        if(button.name == "임무 수첩")
        {
            // 상태 토글
            isObjectActive = !isObjectActive;
            // 오브젝트 상태에 따라 활성/비활성 설정
            selectedObject.SetActive(isObjectActive);
        }
        
    }



    // 마우스 포인터 닿았을 때 Hover 이미지 교체
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼의 Image 컴포넌트를 찾아서 눌렸을 때의 이미지로 변경합니다.
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
        // 버튼의 Image 컴포넌트를 찾아서 원래 이미지로 변경합니다.
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
