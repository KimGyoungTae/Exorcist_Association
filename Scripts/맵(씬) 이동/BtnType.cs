// ��� : ��ư�� �������� �����Ͽ� ����ڰ� Ȯ���� �� �ְ� ����, ��ư Ÿ�Կ� ���� � ������ ��ȯ�� �� ������ ������.
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
            // ó�� ũ�� ����
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
             //   Debug.Log("�ε���");
                break;

            case ButtonType.Construction:
                sceneTransition.FadeScene(((int)currentType));
              //  Debug.Log("������");
                break;

            case ButtonType.Street:
                sceneTransition.FadeScene(((int)currentType));
              //  Debug.Log("��");
                break;

            case ButtonType.Residential:
                sceneTransition.FadeScene(((int)currentType));
              //  Debug.Log("���ð�");
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
        // ��ư ũ�� �ø���
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ó�� ũ��� �ǵ��ư���
        buttonScale.localScale = defaultScale;
    }
}
