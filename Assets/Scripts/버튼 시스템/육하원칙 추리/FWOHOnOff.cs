using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ��Ȱ�� �۾��� ���� �� ���� ��ư�� ����ϴ� ��ũ��Ʈ�� �߸� ��ư ��ũ��Ʈ�� �и� ���ѳ���.
/// �߸� UI�� ���� �ݰ� �� �� �ִ� ���
/// 4���� ���� ��� ������ �� �߾� ���(�߸� ����) ��ư Sprite ��ȭ
/// </summary>
public class FWOHOnOff : MonoBehaviour
{

    public GameObject uiPanel; // UI â�� ��Ÿ���� GameObject
    public GameObject MapMenuPanel;
    public Button openButton; // UI â�� ���� ���� ��ư
    public Button closeButton; // UI â�� �ݱ� ���� ��ư

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
        // ó������ UI â�� ��Ȱ��ȭ
        uiPanel.SetActive(false);

        // "����" ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        openButton.onClick.AddListener(OpenUI);

        // "�ݱ�" ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        closeButton.onClick.AddListener(CloseUI);
    }

    // UI â ���� �Լ�
    void OpenUI()
    {
        if(activeFWOHButton)
        {
            uiPanel.SetActive(true);
            MapMenuPanel.SetActive(false);
        }
    }

    // UI â �ݱ� �Լ�
    void CloseUI()
    {
        uiPanel.SetActive(false);
        MapMenuPanel.SetActive(true);
    }
}
