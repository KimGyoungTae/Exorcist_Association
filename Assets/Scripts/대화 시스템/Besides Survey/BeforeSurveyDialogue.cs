using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeSurveyDialogue : TextDialogSystem
{

    [Space]
    [Header("대화 상대 가져오기")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HidePanel;
    public InteractionEvent interactionEvent;

    private bool afterInteraction = false;

    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;

    // (true: On, false: Off)
    // 캐릭터들의 상태
    private bool KHSStateOn = false;
    private bool CJWStateOn = true;

    // 메인 맵으로 들어갈 때 딱 한번 실행을 유도하기 위한 정적 변수
    // 메인 맵에서 조사하기 전 대화를 실행 후에 다른 씬으로 이동 후, 다시 메인 맵으로 돌아올 때
    // 조사 전 대화가 계속 실행되는 문제를 막기 위함임.
    private static bool hasStarted = false;
    private bool skipAction = false;

    private void Start()
    {
        if (!hasStarted)
        {
            StartDialogSurvey();
            //   Debug.Log("가이드 라인 대화 진행");
        }

        hasStarted = true;

        if (SkipButton != null)
        {
            // SKip 버튼 클릭 이벤트 추가
            SkipButton.onClick.AddListener(ClickSkipButton);
        }
    }

    private void Update()
    {
        DiaAction();

        if (skipAction)
        {
            //  Debug.Log("스킵 발동");
            StopAllCoroutines();
            skipAction = false;
            return;
        }
    }


    // 2023.10.25 기존 임무수첩 버튼을 클릭했을 때 열리는 추리 창을 
    // ??? 버튼 클릭 시로 바꿔주게 됨..
    // 나중에 따로 전투 들가기 전 대화 씬 재생을 한다면 BattleStart()메서드를 불러오면 됨..
    void StartDialogSurvey()
    {
        ShowDialogue(interactionEvent.GetDialogue(afterInteraction));

        startInteraction = true;
        if (startInteraction == true)
        {
            Debug.Log(dialogues[lineCount].name);
            ShowDialogueName();
            startInteraction = false;
        }
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
                            ShowDialogueName();
                        }

                        // 모든 대화가 끝났을 때
                        else
                        {
                            EndDialogue();
                            afterInteraction = true;

                            //   Debug.Log("대화 마무리 조사 진행 가능");

                        }
                    }
                }
            }
        }
    }



    void ChangeCharactoreImage()
    {
        // 강햬성의 상태를 변경합니다.
        KHSStateOn = !KHSStateOn;
        // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOn ? 0 : 1];
        // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;


        // 차지원의 상태를 변경합니다.
        CJWStateOn = !CJWStateOn;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOn ? 0 : 1];
        CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip 버튼 누른 후 다시 대화 진행을 위해 강제적으로 대화 여부를 false로 진행
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;
    }

    public override void SettingUI(bool isAction)
    {
        base.SettingUI(isAction);
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        HidePanel.SetActive(!isAction);
    }

    public override void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        base.ShowDialogue(Parm_dialogues);
    }


    public override void EndDialogue()
    {
        base.EndDialogue();
    }

    public override void ShowDialogueName()
    {
        base.ShowDialogueName();
    }

    public override IEnumerator TypeWriter()
    {
        return base.TypeWriter();
    }

    public override void ChangeCharacterUI(string tag, string name)
    {
        throw new System.NotImplementedException();
    }

}
