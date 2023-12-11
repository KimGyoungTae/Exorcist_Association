using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 각 씬들의 버튼에 대한 정보를 가지고 버튼 상태 등을 제어함.
/// </summary>
public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [Header("버튼 상태 변화 정보")]
    [SerializeField] private Button button;
    [SerializeField] private Sprite HoverImage;
    [SerializeField] private Transform buttonScale;
    private Sprite originImage;
    private Vector3 defaultScale;

    [SerializeField] private ButtonLock btnLock;

    
    void Start()
    {
        Image image = button.GetComponent<Image>();

        // 이미지 존재할 시
        if (image != null) { originImage = image.sprite; }

        if (buttonScale != null)
        {
            // 처음 크기 저장
            defaultScale = buttonScale.localScale;
        }

        else
        {
            Debug.LogWarning("buttonScale is not assigned.");
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
            if (image != null && HoverImage != null)
            {
                image.sprite = HoverImage;
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
        if (button.name == "지도 버튼" || button.name == "메뉴명" || button.name == "임무 수첩")
        {
            btnLock.Action();
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

        if(buttonScale != null)
        {
            // 버튼 크기 늘리기
            buttonScale.localScale = defaultScale * 1.2f;
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

        if (buttonScale != null)
        {
            // 처음 크기로 되돌아가기
            buttonScale.localScale = defaultScale;
        }
    }
}
