using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;

    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    
   // public float initialPosX = 392f; // �ʱ� Pos X ��

    //private void Start()
    //{
    //    // Rect Transform�� Pos X ���� �ʱ� ������ �����մϴ�.
    //    Vector3 newPosition = levelPagesRect.anchoredPosition;
    //    newPosition.x = initialPosX;
    //    levelPagesRect.anchoredPosition = newPosition;
    //}

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;

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
    }
}
