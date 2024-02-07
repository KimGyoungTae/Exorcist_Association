using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDialogue : TextDialogSystem
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

    [SerializeField] private GameObject BattleBackGround;
    [SerializeField] private GameObject FirstCutScene;
    [SerializeField] private GameObject GrandmotherCutScene1;
    [SerializeField] private GameObject GrandmotherCutScene2;
    [SerializeField] private GameObject FinalBackGround;

    // 사운드
    public AudioSource EndingCutSceneMusic;

    void Start()
    {
        sceneTransition = GetComponent<ManageSceneTransition>();
        StartEndingDialogue();

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

    void StartEndingDialogue()
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

                        if (lineCount == 25 && contextCount == 2)
                        {
                            //   Debug.Log("할머니 컷신 2");
                            SecondGrandMotherRememberCutScene();
                        }
                    }

                    else
                    {
                        // 대사 카운트 초기화
                        contextCount = 0;

                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());

                            if (lineCount == 12)
                            {
                                //    Debug.Log("컷신 재생.");
                                ActiveCutScene();
                            }

                            if (lineCount == 20)
                            {
                                //     Debug.Log("할머니 컷신 1");
                                FirstGrandMotherRememberCutScene();
                            }

                            if (lineCount == 26)
                            {
                                //    Debug.Log("마지막 엔딩 컷신");
                                FinalCutScene();
                            }

                            if (lineCount == 28)
                            {
                                CharactorOff(true);
                            }

                            ChangeCharactoreImage();
                            ShowDialogueName();
                        }
                        else
                        {
                            EndDialogue();
                            sceneTransition.FadeScene(10); // 성적표 씬으로 이동..
                        }
                    }
                }
            }
        }
    }

    void CharactorOff(bool isAction)
    {
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        HWoo.SetActive(isAction);
    }


    // 현재 대화 하는 사람의 이름을 파악하여 알맞은 상태변화로 이어지게 함.
    void ChangeCharactoreImage()
    {
        if (dialogues[lineCount].name == "차지원")
        {
            ManageChangeState(false, true, false);
        }


        else if (dialogues[lineCount].name == "강혜성")
        {
            ManageChangeState(true, false, false);
        }

        else if (dialogues[lineCount].name == "현우")
        {
            ManageChangeState(false, false, true);

        }

        else
        {
            // 중간에 나레이션 대사인 상황..
            KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
            CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
            HWoo.GetComponent<SpriteRenderer>().sprite = HWooOnOffSprites[1];
        }

    }

    void ManageChangeState(bool parm_KHSStateOnOff, bool parm_CJWStateOnOff, bool parm_HWooStateOnOff)
    {
        KHSStateOnOff = parm_KHSStateOnOff;
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

        CJWStateOnOff = parm_CJWStateOnOff;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
        CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

        HWooStateOnOff = parm_HWooStateOnOff;
        Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
        HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;
    }

    // 대화 시작 시 캐릭터 상태 초기화 함수
    void InitializeCharacterState()
    {
        ManageChangeState(true, false, false);
    }


    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip 버튼 누른 후 다시 대화 진행을 위해 강제적으로 대화 여부를 false로 진행
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;
        sceneTransition.FadeScene(10); // 성적표 씬으로 이동..
    }

    void ActiveCutScene()
    {
        CharactorOff(false);
        //EndingCutSceneMusic.Play();
        LeanTween.alpha(BattleBackGround, 0f, 1f).setDelay(0.3f);
        LeanTween.alpha(FirstCutScene, 1f, 1f);
    }

    void FirstGrandMotherRememberCutScene()
    {
        LeanTween.alpha(FirstCutScene, 0f, 1f);
        LeanTween.alpha(GrandmotherCutScene1, 1f, 1f).setDelay(0.3f);
    }

    void SecondGrandMotherRememberCutScene()
    {
        LeanTween.alpha(GrandmotherCutScene1, 0f, 1f);
        LeanTween.alpha(GrandmotherCutScene2, 1f, 1f).setDelay(0.3f);
    }

    void FinalCutScene()
    {
        LeanTween.alpha(GrandmotherCutScene2, 0f, 2f);
        LeanTween.alpha(FinalBackGround, 1f, 1f).setDelay(1f);
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
        CharactorOff(false);
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
