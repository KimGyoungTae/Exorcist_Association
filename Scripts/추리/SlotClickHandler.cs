using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClickHandler : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{

    // Panel �� Button�� ���� ����
    private Selects panelSelection;
    public Selects otherPanelSelection;

    public int objectNumber;
    // objectNumber�� ���� �ٸ� ����� �ؽ�Ʈ�� ������ �迭
    public string texts;

    // static ���� ���� �� �ҽ� ������ Ŭ���� objectNumber�� ������ �ȵǴ� ���� �߻���.
    private static int previousObjectNumber = -1; // ���� Ŭ���� �������� objectNumber
    private Color previousImageColor; // ���� �̹��� ���� ���� ����

    private void Start()
    {
        panelSelection = GetComponentInParent<Selects>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        //int N = panelSelection.slots.Length;


        //bool allSlotsRed = true;

        //// ��� Slot�� Red���� Ȯ��
        //for (int i = 0; i < N; i++)
        //{
        //    if (!panelSelection.slots[i].GetComponent<Image>().color.Equals(Color.red))
        //    {
        //        allSlotsRed = false;
        //        break;
        //    }
        //}


        //// ��� Slot�� Red�� ���� ���� Red�� �ƴ� Slot�� �ִ� ��쿡 ���� �б�
        //if (allSlotsRed)
        //{
        //    // ��� Slot�� Red�� ��� ó���� ����
        //    print("���� ������ �̵��� �� �ֽ��ϴ�..");
        //}
        //else
        //{
        //    // ���� Red�� �ƴ� Slot�� �ִ� ��� ó���� ����
        //    print("���� ��� ���Ÿ� Ȯ������ ���߽��ϴ�..");
        //}



        // ���� Ŭ���� �������� ������ ���� �̹��� �������� ����
        if (previousObjectNumber != -1 && previousObjectNumber != objectNumber)
        {
            panelSelection.ChangeSlotColor(previousObjectNumber, previousImageColor);
        }

        Debug.Log(previousObjectNumber);
        Debug.Log(objectNumber);


        // ���� Ŭ���� Slot�� ���� ����� �ؽ�Ʈ -> �׸� �ݿ�
        switch (objectNumber)
        {
            case 0:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue);
                otherPanelSelection.ChangeChoiceText(texts);
                break;
            case 1:
                panelSelection.ChangeSlotColor(objectNumber, Color.blue);
                otherPanelSelection.ChangeChoiceText(texts);
                break;
        }


        // ���� Ŭ���� �������� objectNumber�� ������ Ŭ���� ���������� ����
        previousObjectNumber = objectNumber;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� �̹��� ���� ����
        previousImageColor = panelSelection.GetSlotColor(objectNumber);
    }
}
