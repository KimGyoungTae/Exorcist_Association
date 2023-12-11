using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아직 사용하지 않을 버튼들에게 적용이 된다.
/// 버튼을 클릭하면 아직 사용할 수 없다고 대화 내용이 뜨게 된다.
/// </summary>
public class ButtonLock : MonoBehaviour
{
    [Header("대사 출력할 오브젝트 정보")]
    [SerializeField] private int id;

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;
    [Space]

    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text TalkText;

    [SerializeField] private bool isAction;
    [SerializeField] private int talkIndex;

    [Space]
    [Header("일정 시간 후 자동 대화 패널 비활성화 시간")]
    public float displayTime = 1.3f;   // 대화 패널이 활성화된 상태로 유지할 시간

    public void Action()
    {
        Talk(id);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        
        // 대화 내용을 다 출력하고 Panel이 닫힐 때 
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0; //대화가 끝날 때 0으로 초기화 , 다른 사물하고도 계속 대화를 진행 하기 위함.

            return;  // 끝 , void 함수에서 return은 강제 종료 역할.
        }

        // 코루틴을 사용하여 텍스트를 딜레이와 함께 한 단어씩 보여줍니다.
        StartCoroutine(DisplayText(talkData));
        // 일정 시간 후에 대화 패널을 비활성화하는 코루틴 시작
        StartCoroutine(HideDialogueAfterTime());


        isAction = true;
        talkIndex++;
    }

    IEnumerator DisplayText(string text)
    {
        TalkText.text = ""; // 텍스트를 보여주기 전에 먼저 초기화합니다.

        foreach (char character in text)
        {
            TalkText.text += character;
            yield return new WaitForSeconds(textDelay);
            // 다음 글자를 보여주기 전에 지정한 딜레이만큼 대기합니다.       
        }
    }

    private IEnumerator HideDialogueAfterTime()
    {
        // 일정 시간 동안 대화 패널을 활성화한 후 비활성화
        yield return new WaitForSeconds(displayTime);
        talkPanel.SetActive(false);
    }
}
