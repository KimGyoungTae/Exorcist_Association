// 각 탭의 슬롯들을 버튼을 클릭하여 좌 우로 이동하게 하는 역할 
// 현재는 사용하지 않는다 - 2023.12.3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;

    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    [SerializeField] Button previousBtn, nextBtn;
    
   // public float initialPosX = 392f; // 초기 Pos X 값

    //private void Start()
    //{
    //    // Rect Transform의 Pos X 값을 초기 값으로 설정합니다.
    //    Vector3 newPosition = levelPagesRect.anchoredPosition;
    //    newPosition.x = initialPosX;
    //    levelPagesRect.anchoredPosition = newPosition;
    //}

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        UpdateArrowButton();
    }

    public void Next()
    {
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if(currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateArrowButton();
    }

    void UpdateArrowButton()
    {
        nextBtn.interactable = true;
        previousBtn.interactable = true;

        if(currentPage == 1)
        {
            previousBtn.interactable = false;
        }

        else if(currentPage == maxPage)
        {
            nextBtn.interactable = false;
        }
    }
}
