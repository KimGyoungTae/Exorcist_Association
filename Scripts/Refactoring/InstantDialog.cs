// 요약 : ??? 버튼을 눌러 추리 작업을 대신하고 전투 씬으로 이동하기 위한 대화
// 캐릭터들의 상태에 따라 On/Off 이미지 바꾸는 응용을 이 코드에서 참고하면 좋을 꺼 같음..

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantDialog : MonoBehaviour
{
    public InteractionEvent interactionEvent;
    public ManageSceneTransition sceneTransition;


    Dialogue[] dialogues;
    bool isDialogue = false; // 대화중일 경우 true.
    bool isNext = false; // 특정 키 입력 대기.

    int lineCount = 0; // 사람 대화 카운트.
    int contextCount = 0; //대사 카운트

    public GameObject talkPanel;
    public Text TalkText;


    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    [Space]
    [Header("대화 상대 가져오기")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HidePanel;

    
    bool afterInteraction = false;


    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;

    public GameObject KHSObject;
    public GameObject CJWObject;

    // (true: On, false: Off)
    // 캐릭터들의 상태
    private bool KHSStateOn = true;
    private bool CJWStateOn = false;



    void Update()
    {    
        DiaAction(); // 대화 진행도를 체크하는 부분도 Update()에서 처리합니다.
    }

    // 대화 시작 시 캐릭터 상태 초기화 함수
    void InitializeCharacterState()
    {
        KHSStateOn = true;
        CJWStateOn = false;

        // 초기 캐릭터 이미지 설정
        KHSObject.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[0];
        CJWObject.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
    }


    public void BattleStart()
    {
        // 대화 시작 시 캐릭터 상태 초기화
        InitializeCharacterState();

        ShowDialogue(interactionEvent.GetDialogue(afterInteraction));

    }

    void DiaAction()
    {
        if (isDialogue) // 대화중 이면서
        {
            if (isNext) // 다음 키 입력이 가능할 때
            {
                if (Input.GetMouseButtonDown(0)) // 마우스 버튼을 누를 때
                {

                    isNext = false;
                    TalkText.text = "";

                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    else
                    {
                        // 대사 카운트 초기화
                        contextCount = 0;

                        // 각 캐릭터의 대사를 모두 마칠 때 이미지 상태 On / OFF 변경
                        ChangeCharactoreImage();


                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }

                        // 모든 대화가 끝났을 때
                        else
                        {
                            EndDialogue();
                            afterInteraction = true;

                            // 전투 영상 씬 전환
                            sceneTransition.FadeScene(7);
                            Debug.Log("전투 영상 재생");

                            // 대화 종료 시 캐릭터 상태 초기화
                            InitializeCharacterState();
                            
                        }
                    }
                }
            }
        }
    }


    public void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        isDialogue = true; // 대화가 시직할 때 "대화중이다". 알림
        TalkText.text = "";
        dialogues = Parm_dialogues;

        StartCoroutine(TypeWriter());
    }


    void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;

        SettingUI(false); // 대화 UI 끄기

    
}

    IEnumerator TypeWriter()
    {
        //대화 UI활성화
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            TalkText.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;

    }

    void SettingUI(bool isAction)
    {
        talkPanel.SetActive(isAction);
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        HidePanel.SetActive(!isAction);

        // NamePanel 여기에 넣기.
    }


    public void ChangeCharactoreImage()
    {
        // 강햬성의 상태를 변경합니다.
        KHSStateOn = !KHSStateOn;
        // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOn ? 0 : 1];
        // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
        KHSObject.GetComponent<SpriteRenderer>().sprite = KHSSprite;


        // 차지원의 상태를 변경합니다.
        CJWStateOn = !CJWStateOn;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOn ? 0 : 1];
        CJWObject.GetComponent<SpriteRenderer>().sprite = CJWSprite;

    }

}

