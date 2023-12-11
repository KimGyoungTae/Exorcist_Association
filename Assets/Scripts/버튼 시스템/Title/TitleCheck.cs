using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 타이틀 화면 UI 체크
/// 클릭 시 체크 화면 표시, 더블 클릭 시 씬 전환 역할 수행
/// </summary>
public class TitleCheck : MoveMapType, IPointerClickHandler

{
    private float newAlphaValue = 1.0f;
    private static Image previousClickedImage; // 이전에 클릭한 오브젝트를 저장하기 위한 변수

    public float doubleClickTimeThreshold = 0.3f; // 더블클릭 감지 시간 간격 (초)
    private float lastClickTime = 0f;

    public AudioSource TitleCheckSoundEffect;
    

    // 목록을 눌럿을 때 체크 표시
    public void OnPointerClick(PointerEventData eventData)
    {
        Image image = GetComponent<Image>();

        // 이전에 클릭한 오브젝트가 있다면, 그 오브젝트의 A값을 원래 상태로 되돌림
        if (previousClickedImage != null && previousClickedImage != image)
        {
            Color prevColor = previousClickedImage.color;
            prevColor.a = 0.0f; // 원래의 Alpha 값으로 되돌리기
            previousClickedImage.color = prevColor;
        }

        Color currentColor = image.color;
        currentColor.a = newAlphaValue;

        if (image != null )
        {
            image.color = currentColor;
            previousClickedImage = image; // 현재 클릭한 오브젝트를 이전에 클릭한 오브젝트로 저장
        }


        if (Time.time - lastClickTime < doubleClickTimeThreshold)
        {
            // 더블클릭 발생
            // 더블클릭 시 해당 오브젝트의 이름을 전달
            OnDoubleClick(image.gameObject.name);
            lastClickTime = 0f; // 클릭 시간 초기화
           // TitleCheckSoundEffect.Play();
        }
        else
        {
            // 첫 번째 클릭 저장
            lastClickTime = Time.time;
        }

        TitleCheckSoundEffect.Play();
    }


    // 더블클릭 시 반응
    private void OnDoubleClick(string objectName)
    {
       // Debug.Log("Double Clicked!");

        // 오브젝트 이름에 따라 원하는 반응을 수행
        if (objectName == "새 임무" || objectName == "임무 종료")
        {
            OnBtnClick();
            TitleCheckSoundEffect.Stop();
        }
    }

}
