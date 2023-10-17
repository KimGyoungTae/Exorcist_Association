// 요약 : 버튼의 스케일을 조정하여 사용자가 확인할 수 있게 돕고, 버튼 타입에 따라 어떤 씬으로 전환할 지 역할을 수행함.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public ButtonType currentType;
    Vector3 defaultScale;

    
    public Transform buttonScale;

    public ManageSceneTransition sceneTransition;

    private void Start()
    {
        
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

    public void OnBtnClick()
    {
        switch(currentType)
        {
            case ButtonType.Estate:
                sceneTransition.FadeScene(((int)currentType));
             //   Debug.Log("부동산");
                break;

            case ButtonType.Construction:
                sceneTransition.FadeScene(((int)currentType));
              //  Debug.Log("공사장");
                break;

            case ButtonType.Street:
                sceneTransition.FadeScene(((int)currentType));
              //  Debug.Log("상가");
                break;

            case ButtonType.Residential:
                sceneTransition.FadeScene(((int)currentType));
              //  Debug.Log("주택가");
                break;

            case ButtonType.Return:
                sceneTransition.FadeScene(((int)currentType));
                break;

            case ButtonType.Dialog:
                sceneTransition.FadeScene(((int)currentType));
                break;

            case ButtonType.Prologue:
                sceneTransition.FadeScene((int)currentType);
                break;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼 크기 늘리기
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 처음 크기로 되돌아가기
        buttonScale.localScale = defaultScale;
    }
}
