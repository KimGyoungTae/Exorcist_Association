using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 원활한 작업을 위해 씬 마다 버튼을 담당하는 스크립트와 추리 버튼 스크립트와 분리 시켜놓음.
/// 추리 UI를 열고 닫고 할 수 있는 기능
/// 4개의 맵을 모두 조사할 시 중앙 결계(추리 진행) 버튼 Sprite 변화
/// </summary>
public class FWOHOnOff : MonoBehaviour
{

    public GameObject uiPanel; // UI 창을 나타내는 GameObject
    public GameObject MapMenuPanel;
    public Button openButton; // UI 창을 열기 위한 버튼
    public Button closeButton; // UI 창을 닫기 위한 버튼

    [Space]
    public Sprite[] FWOHButtonOnOffSprites;
    private bool activeFWOHButton = false;


    private void Awake()
    {
        int currentVisitCount = VisitManager.instance.visitCount;

        if (currentVisitCount == 4)
        {
            openButton.image.sprite = FWOHButtonOnOffSprites[0];
            activeFWOHButton = true;
        }

        else
        {
            openButton.image.sprite = FWOHButtonOnOffSprites[1];
            activeFWOHButton = false;
        }
            
    }

    private void Start()
    {
        // 처음에는 UI 창을 비활성화
        uiPanel.SetActive(false);

        // "열기" 버튼에 클릭 이벤트를 추가
        openButton.onClick.AddListener(OpenUI);

        // "닫기" 버튼에 클릭 이벤트를 추가
        closeButton.onClick.AddListener(CloseUI);
    }

    // UI 창 열기 함수
    void OpenUI()
    {
        if(activeFWOHButton)
        {
            uiPanel.SetActive(true);
            MapMenuPanel.SetActive(false);
        }
    }

    // UI 창 닫기 함수
    void CloseUI()
    {
        uiPanel.SetActive(false);
        MapMenuPanel.SetActive(true);
    }
}
