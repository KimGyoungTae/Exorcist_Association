using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClickHandler : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{

    // Panel �� Button�� ���� ����
    private Selects panelSelection;
    // objectNumber�� ���� �ٸ� ����� �ؽ�Ʈ�� ������ �迭
    public int objectNumber;

    // static ���� ���� �� �ҽ� ������ Ŭ���� objectNumber�� ������ �ȵǴ� ���� �߻���.
    private static int previousObjectNumber = -1; // ���� Ŭ���� �������� objectNumber

    private Color previousImageColor; // ���� �̹��� ���� ���� ����
    private Color previousTextColor; // ���� ������ �ؽ�Ʈ ���� ���� ����

    public AudioSource SlotSoundEffect;


    private void Start()
    {
        panelSelection = GetComponentInParent<Selects>();

        //Debug.Log(panelSelection.gameObject.name);  // Choice Slots
        //Debug.Log(otherPanelSelection.gameObject.name); // Choice Text
    }


    // ������ ��ư�� Ŭ���� �� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // �� �Ǹ��� ������ ��ư Ŭ�� �� "Ȯ��" ��ư Ȱ��ȭ
        ActiveSafeButton();
      
        // ���� Ŭ���� �������� ������ ���� �̹��� �������� ����
        if (previousObjectNumber != -1 && previousObjectNumber != objectNumber)
        {
            panelSelection.ChangeSlotColor(previousObjectNumber, previousImageColor, 1);
            panelSelection.ChangeSlotTextColor(previousObjectNumber, previousTextColor);
        }

        SlotSoundEffect.Play();


        // ���� Ŭ���� Slot�� ���� ����� �ؽ�Ʈ -> �׸� �ݿ�
        switch (objectNumber)
        {
            case 0:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                //  panelSelection.ChangeChoiceText(texts);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;
            case 1:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                //  panelSelection.ChangeChoiceText(texts);
                panelSelection.ChangeSlotTextColor(objectNumber,Color.white);
                break;

            case 2:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;
            case 3:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;
            case 4:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;
            case 5:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;
            case 6:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;
            case 7:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;
            case 8:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue, 0);
                panelSelection.ChangeSlotTextColor(objectNumber, Color.white);
                break;

            default:
                Debug.LogError("�������� ���� ��ȣ �Դϴ�.");
                break;
        }


        // ���� Ŭ���� �������� objectNumber�� ������ Ŭ���� ���������� ����
        previousObjectNumber = objectNumber;
    }

    private void ActiveSafeButton()
    {
        panelSelection.fWOHBtn.SafeButton.image.sprite = panelSelection.fWOHBtn.SafeButtonOnOffSprites[0];
    }

    // ������ ��ư�� ���콺 Ŭ���� ������ �� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� �̹��� ���� ����
        previousImageColor = panelSelection.GetSlotColor(objectNumber);
        previousTextColor = panelSelection.GetSlotTextColor(objectNumber);

    }
}
