using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    Estate,
    Construction,
    Street,
    Residential,
    Return,
    Dialog,
    Prologue,
    GameEnd,
    None
}


/// <summary>
/// �̵��� �� ��ư Ÿ��(ButtonType)�� �Ǵ��ϰ� �� �̵��� ������.
/// �� ��ũ��Ʈ�� �̵��� ��(��)�� ������ ��� �ִ� ��ư ���� ����Ǿ� ����.
/// </summary>
public class MoveMapType : MonoBehaviour
{

    public AudioSource MapWalkingSoundEffect;

    [SerializeField]
    private ButtonType currentType;
    [SerializeField]
    private ManageSceneTransition sceneTransition;
    [SerializeField]
    private Button MapBtn;


    void Start()
    {
        if(MapBtn != null)
        {
            // ��ư�� Ŭ�� �̺�Ʈ�� �߰�
            MapBtn.onClick.AddListener(OnBtnClick);
        }
    }

    protected void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.Estate:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
               // Debug.Log("�ε���");
                break;

            case ButtonType.Construction:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
                //  Debug.Log("������");
                break;

            case ButtonType.Street:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
                //  Debug.Log("��");
                break;

            case ButtonType.Residential:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
                //  Debug.Log("���ð�");
                break;

            case ButtonType.Return:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
                break;

            case ButtonType.Dialog:
                sceneTransition.FadeScene(((int)currentType));
                break;

            case ButtonType.Prologue:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene((int)currentType);
                break;

            case ButtonType.GameEnd:
                QuitGame();
                break;

            default:
                // ó���� ��ư Ÿ���� ���� ���� �⺻ ����
                break;
        }
    }

    // ���� ���� �Լ�
    private void QuitGame()
    {
#if UNITY_EDITOR
        // �����Ϳ����� Play ��带 �����մϴ�.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� �ۿ����� ���� �����մϴ�.
        Application.Quit();
#endif
    }
}
