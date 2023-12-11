using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FWOHButtonManager : MonoBehaviour
{
    public Sprite[] onSprites;  // On ������ ��������Ʈ �迭
    public Sprite[] offSprites; // Off ������ ��������Ʈ �迭
    public Button[] buttons;    // 6���� ��ư �迭

    public Button SafeButton;
    public Sprite[] SafeButtonOnOffSprites;

    public Button FinishButton;
    public Sprite[] FinishButtonOnOffSprites;

    private int currentButtonIndex = -1; // ���� Ŭ���� ��ư�� �ε���, �ʱⰪ�� -1
    private bool updateEnabled = true;
    [SerializeField] private ManageSceneTransition sceneTransition;


    void Start()
    {
        // �ʱ�ȭ: ��� ��ư�� �̹����� Off ���·� ����
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = offSprites[i];

            // �� ��ư�� ���� Ŭ�� �̺�Ʈ ���
            // Ŭ�� �̺�Ʈ �ڵ鷯 ���� �Ҵ�
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        FinishButton.image.sprite = FinishButtonOnOffSprites[1];
    }

    private void Update()
    {
        if (!updateEnabled)
            return;

        if (ConfirmManager.Instance.ActiveFinishButton == true)
        {
          //  Debug.Log("�����ϱ� ��ư Ȱ��ȭ");
            FinishButton.image.sprite = FinishButtonOnOffSprites[0];

            // ��� ���� �����Ͱ� ��� �Ǿ��� ���� ���� ������ ������ �����´�.
            int GetCount = ConfirmManager.Instance.GetCorrectCount;
            FinishButton.onClick.AddListener(() => OnFinishButtonClick(GetCount));

            updateEnabled = false; // Update ��Ȱ��ȭ
            return;
        }
    }


    // ���Ͽ�Ģ �� ��ư Ŭ�� �̺�Ʈ �ڵ鷯
    // 1. Ŭ������ �� ����Ŭ���� �̹��� ����
    // 2. ó�� Ŭ�� �� ��ư�� ���ο� �̹��� ��ȭ 
    // 3. ���� �ٸ� ��ư Ŭ�� �ÿ��� ���� Ŭ���� �̹����� ���� �̹����� ���� ��ȭ��Ų �Ŀ� 
    // 4. �� ������ ���Ӱ� Ŭ���� ��ư �̹����� ���ο� �̹����� ��ȭ�Ѵ�.
    public void OnButtonClick(int buttonIndex)
    {
        //  Debug.Log("Button Clicked: " + buttonIndex);

        // ���� Ŭ���� ��ư�� ������ Ŭ���� ��ư�� �ٸ� ��쿡�� ó��
        if (currentButtonIndex != buttonIndex)
        {
            // ������ Ŭ���� ��ư�� �̹����� Off ���·� ����
            if (currentButtonIndex >= 0 && currentButtonIndex < buttons.Length)
            {
                buttons[currentButtonIndex].image.sprite = offSprites[currentButtonIndex];
            }

            // ���� Ŭ���� ��ư�� �̹����� On ���·� ����
            buttons[buttonIndex].image.sprite = onSprites[buttonIndex];

            // ���� Ŭ���� ��ư�� �ε����� ������Ʈ
            currentButtonIndex = buttonIndex;
        }

        // Debug.Log("�� Ŭ��");
        SafeButton.image.sprite = SafeButtonOnOffSprites[1];
    }


    // ����� �ǿ��� "�����ϱ�" ��ư�� ������ �� ��� ���� ������ �´��� Ȯ��
    // ������ ���� ������ ���� �г�Ƽ�� �������� �ο��� �����̾����� �г�Ƽ�� ���� ������ ������ ��.
    void OnFinishButtonClick(int correctCount)
    {
        if (correctCount == 5)
        {
            // ��� ���� ������ ���� ���
            Debug.Log("��� ������ ������ϴ�!! ������ �����մϴ�.");
            PenaltyManager.instance.GetCorrectAnswer(correctCount);
            // ���� ���� �ϱ� �� ��� ����
            sceneTransition.FadeScene(7); 
        }

        else
        {
            Debug.Log("��� ������ ������ ���Ͽ� �г�Ƽ�� �ο� �ް� ������ �����մϴ�.");
            PenaltyManager.instance.GetCorrectAnswer(correctCount);
            sceneTransition.FadeScene(7);
        }
    }
}

