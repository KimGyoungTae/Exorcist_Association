using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClickHandler : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{

    // Panel 및 Button에 대한 참조
    private Selects panelSelection;
    public Selects otherPanelSelection;

    public int objectNumber;
    // objectNumber에 따라 다른 색상과 텍스트를 설정할 배열
    public string texts;

    // static 으로 선언 안 할시 이전에 클릭한 objectNumber가 저장이 안되는 문제 발생함.
    private static int previousObjectNumber = -1; // 이전 클릭한 아이콘의 objectNumber
    private Color previousImageColor; // 이전 이미지 색상 저장 변수

    private void Start()
    {
        panelSelection = GetComponentInParent<Selects>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        //int N = panelSelection.slots.Length;


        //bool allSlotsRed = true;

        //// 모든 Slot이 Red인지 확인
        //for (int i = 0; i < N; i++)
        //{
        //    if (!panelSelection.slots[i].GetComponent<Image>().color.Equals(Color.red))
        //    {
        //        allSlotsRed = false;
        //        break;
        //    }
        //}


        //// 모든 Slot이 Red일 때와 아직 Red가 아닌 Slot이 있는 경우에 대한 분기
        //if (allSlotsRed)
        //{
        //    // 모든 Slot이 Red인 경우 처리할 로직
        //    print("전투 씬으로 이동할 수 있습니다..");
        //}
        //else
        //{
        //    // 아직 Red가 아닌 Slot이 있는 경우 처리할 로직
        //    print("아직 모든 증거를 확보하지 못했습니다..");
        //}



        // 이전 클릭한 아이콘의 색상을 이전 이미지 색상으로 복원
        if (previousObjectNumber != -1 && previousObjectNumber != objectNumber)
        {
            panelSelection.ChangeSlotColor(previousObjectNumber, previousImageColor);
        }

        Debug.Log(previousObjectNumber);
        Debug.Log(objectNumber);


        // 현재 클릭한 Slot에 대한 색상과 텍스트 -> 항목 반영
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


        // 현재 클릭한 아이콘의 objectNumber를 이전에 클릭한 아이콘으로 설정
        previousObjectNumber = objectNumber;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        // 현재 이미지 색상 저장
        previousImageColor = panelSelection.GetSlotColor(objectNumber);
    }
}
