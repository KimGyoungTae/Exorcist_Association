using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FWOHButtonManager : MonoBehaviour
{
    public Sprite[] onSprites;  // On 상태의 스프라이트 배열
    public Sprite[] offSprites; // Off 상태의 스프라이트 배열
    public Button[] buttons;    // 6개의 버튼 배열

    public Button SafeButton;
    public Sprite[] SafeButtonOnOffSprites;

    public Button FinishButton;
    public Sprite[] FinishButtonOnOffSprites;

    private int currentButtonIndex = -1; // 현재 클릭한 버튼의 인덱스, 초기값은 -1
    private bool updateEnabled = true;
    [SerializeField] private ManageSceneTransition sceneTransition;


    void Start()
    {
        // 초기화: 모든 버튼의 이미지를 Off 상태로 설정
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = offSprites[i];

            // 각 버튼에 대한 클릭 이벤트 등록
            // 클릭 이벤트 핸들러 동적 할당
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
          //  Debug.Log("제출하기 버튼 활성화");
            FinishButton.image.sprite = FinishButtonOnOffSprites[0];

            // 모든 탭의 데이터가 기록 되었을 때에 현재 정답의 개수를 가져온다.
            int GetCount = ConfirmManager.Instance.GetCorrectCount;
            FinishButton.onClick.AddListener(() => OnFinishButtonClick(GetCount));

            updateEnabled = false; // Update 비활성화
            return;
        }
    }


    // 육하원칙 탭 버튼 클릭 이벤트 핸들러
    // 1. 클릭했을 때 이전클릭한 이미지 저장
    // 2. 처음 클릭 한 버튼은 새로운 이미지 변화 
    // 3. 만약 다른 버튼 클릭 시에는 이전 클릭한 이미지를 원래 이미지로 먼저 변화시킨 후에 
    // 4. 그 다음에 새롭게 클릭한 버튼 이미지를 새로운 이미지로 변화한다.
    public void OnButtonClick(int buttonIndex)
    {
        //  Debug.Log("Button Clicked: " + buttonIndex);

        // 현재 클릭한 버튼이 이전에 클릭한 버튼과 다를 경우에만 처리
        if (currentButtonIndex != buttonIndex)
        {
            // 이전에 클릭한 버튼의 이미지를 Off 상태로 변경
            if (currentButtonIndex >= 0 && currentButtonIndex < buttons.Length)
            {
                buttons[currentButtonIndex].image.sprite = offSprites[currentButtonIndex];
            }

            // 현재 클릭한 버튼의 이미지를 On 상태로 변경
            buttons[buttonIndex].image.sprite = onSprites[buttonIndex];

            // 현재 클릭한 버튼의 인덱스를 업데이트
            currentButtonIndex = buttonIndex;
        }

        // Debug.Log("탭 클릭");
        SafeButton.image.sprite = SafeButtonOnOffSprites[1];
    }


    // 답안지 탭에서 "제출하기" 버튼을 눌렀을 때 모든 탭의 정답이 맞는지 확인
    // 본래는 정답 개수에 따른 패널티를 전투에서 부여할 예정이었지만 패널티는 없는 것으로 수정이 됨.
    void OnFinishButtonClick(int correctCount)
    {
        if (correctCount == 5)
        {
            // 모든 탭의 정답이 맞은 경우
            Debug.Log("모든 정답을 맞췄습니다!! 전투에 입장합니다.");
            PenaltyManager.instance.GetCorrectAnswer(correctCount);
            // 전투 입장 하기 전 결계 입장
            sceneTransition.FadeScene(7); 
        }

        else
        {
            Debug.Log("모든 정답을 맞추지 못하여 패널티를 부여 받고 전투에 입장합니다.");
            PenaltyManager.instance.GetCorrectAnswer(correctCount);
            sceneTransition.FadeScene(7);
        }
    }
}

