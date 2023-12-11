using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolDialogue : TextDialogSystem
{
    [Space]
    [Header("캐릭터 가져오기")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HWoo;

    [Space]
    [Header("캐릭터 대화 상태 가져오기")]
    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;
    public Sprite[] HWooOnOffSprites;

    // (true: On, false: Off)
    // 캐릭터들의 상태
    private bool KHSStateOnOff;
    private bool CJWStateOnOff;
    private bool HWooStateOnOff;

    private bool skipAction = false;
    private bool afterInteraction = false;

    private ManageSceneTransition sceneTransition;
    public InteractionEvent interactionEvent;

  

    void Start()
    {
        sceneTransition = GetComponent<ManageSceneTransition>();
        StartSchoolDialogue();

        if (SkipButton != null)
        {
            // SKip 버튼 클릭 이벤트 추가
            SkipButton.onClick.AddListener(ClickSkipButton);
        }
    }

   
    void Update()
    {
        // 대화 진행도를 체크하는 부분도 Update()에서 처리합니다.
        DiaAction();

        if (skipAction)
        {
            //  Debug.Log("스킵 발동");
            StopAllCoroutines();
            skipAction = false;
            return;
        }
    }

    void StartSchoolDialogue()
    {
        ShowDialogue(interactionEvent.GetDialogue(afterInteraction));

        startInteraction = true;
        if (startInteraction == true)
        {
          //  Debug.Log(dialogues[lineCount].name);
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

                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                      //    Debug.Log(dialogues[lineCount].name);

                            ChangeCharactoreImage();
                            ShowDialogueName();
                        }

                        // 모든 대화가 끝났을 때
                        else
                        {
                            EndDialogue();
                            // 모든 대사 출력 끝날 시 지도 페이지로 씬 이동 
                            sceneTransition.FadeScene(4);
 
                        }
                    }
                }
            }
        }
    }


    // 현재 대화 하는 사람의 이름을 파악하여 알맞은 상태변화로 이어지게 함.
    void ChangeCharactoreImage()
    {
        if (dialogues[lineCount].name == "차지원")
        {
            KHSStateOnOff = false;
            Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
            KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

            CJWStateOnOff = true;
            Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
            CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

            HWooStateOnOff = false;
            Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
            HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;
        }


        else if (dialogues[lineCount].name == "강혜성")
        {
            KHSStateOnOff = true;
            Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
            KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

            CJWStateOnOff = false;
            Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
            CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

            HWooStateOnOff = false;
            Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
            HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;
        }

        else if (dialogues[lineCount].name == "현 우")
        {
            KHSStateOnOff = false;
            Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
            KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

            CJWStateOnOff = false;
            Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
            CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

            HWooStateOnOff = true;
            Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
            HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;
        }

        else Debug.Log("캐릭터 상태 변화가 이루어지지 않습니다.");

    }

    // 대화 시작 시 캐릭터 상태 초기화 함수
    void InitializeCharacterState()
    {
        KHSStateOnOff = false;
        CJWStateOnOff = true;
        HWooStateOnOff = false;

        // 초기 캐릭터 이미지 설정
        KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
        CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[0];
        HWoo.GetComponent<SpriteRenderer>().sprite = HWooOnOffSprites[1];
    }


    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip 버튼 누른 후 다시 대화 진행을 위해 강제적으로 대화 여부를 false로 진행
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;
        sceneTransition.FadeScene(4);
    }

    public override void ChangeCharacterUI(string tag, string name)
    {
        throw new System.NotImplementedException();
    }


    public override void CheckDialogueState()
    {
        base.CheckDialogueState();
    }

    public override void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        base.ShowDialogue(Parm_dialogues);
        InitializeCharacterState();
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

    public override void SettingUI(bool isAction)
    {
        base.SettingUI(isAction);
    }
}
