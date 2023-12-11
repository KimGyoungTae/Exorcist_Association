using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClickHandler : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{

    // Panel 및 Button에 대한 참조
    private Selects panelSelection;
    // objectNumber에 따라 다른 색상과 텍스트를 설정할 배열
    public int objectNumber;

    // static 으로 선언 안 할시 이전에 클릭한 objectNumber가 저장이 안되는 문제 발생함.
    private static int previousObjectNumber = -1; // 이전 클릭한 아이콘의 objectNumber

    private Color previousImageColor; // 이전 이미지 색상 저장 변수
    private Color previousTextColor; // 이전 선택지 텍스트 색상 저장 변수

    public AudioSource SlotSoundEffect;


    private void Start()
    {
        panelSelection = GetComponentInParent<Selects>();

        //Debug.Log(panelSelection.gameObject.name);  // Choice Slots
        //Debug.Log(otherPanelSelection.gameObject.name); // Choice Text
    }


    // 선택지 버튼을 클릭할 때 마다
    public void OnPointerClick(PointerEventData eventData)
    {
        // 각 탭마다 선택지 버튼 클릭 시 "확정" 버튼 활성화
        ActiveSafeButton();
      
        // 이전 클릭한 아이콘의 색상을 이전 이미지 색상으로 복원
        if (previousObjectNumber != -1 && previousObjectNumber != objectNumber)
        {
            panelSelection.ChangeSlotColor(previousObjectNumber, previousImageColor, 1);
            panelSelection.ChangeSlotTextColor(previousObjectNumber, previousTextColor);
        }

        SlotSoundEffect.Play();


        // 현재 클릭한 Slot에 대한 색상과 텍스트 -> 항목 반영
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
                Debug.LogError("지정되지 않은 번호 입니다.");
                break;
        }


        // 현재 클릭한 아이콘의 objectNumber를 이전에 클릭한 아이콘으로 설정
        previousObjectNumber = objectNumber;
    }

    private void ActiveSafeButton()
    {
        panelSelection.fWOHBtn.SafeButton.image.sprite = panelSelection.fWOHBtn.SafeButtonOnOffSprites[0];
    }

    // 선택지 버튼을 마우스 클릭을 누르고 땔 때
    public void OnPointerUp(PointerEventData eventData)
    {
        // 현재 이미지 색상 저장
        previousImageColor = panelSelection.GetSlotColor(objectNumber);
        previousTextColor = panelSelection.GetSlotTextColor(objectNumber);

    }
}
