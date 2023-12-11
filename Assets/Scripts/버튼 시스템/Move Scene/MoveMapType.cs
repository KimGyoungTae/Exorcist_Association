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
/// 이동할 맵 버튼 타입(ButtonType)을 판단하고 씬 이동만 제어함.
/// 이 스크립트는 이동할 맵(씬)의 정보를 담고 있는 버튼 한테 적용되어 있음.
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
            // 버튼에 클릭 이벤트를 추가
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
               // Debug.Log("부동산");
                break;

            case ButtonType.Construction:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
                //  Debug.Log("공사장");
                break;

            case ButtonType.Street:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
                //  Debug.Log("상가");
                break;

            case ButtonType.Residential:
                MapWalkingSoundEffect.Play();
                sceneTransition.FadeScene(((int)currentType));
                //  Debug.Log("주택가");
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
                // 처리할 버튼 타입이 없을 때의 기본 로직
                break;
        }
    }

    // 게임 종료 함수
    private void QuitGame()
    {
#if UNITY_EDITOR
        // 에디터에서는 Play 모드를 정지합니다.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 앱에서는 앱을 종료합니다.
        Application.Quit();
#endif
    }
}
