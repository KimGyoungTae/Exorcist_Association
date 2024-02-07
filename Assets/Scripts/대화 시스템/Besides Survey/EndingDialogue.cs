using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDialogue : TextDialogSystem
{
    [Space]
    [Header("ĳ���� ��������")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HWoo;

    [Space]
    [Header("ĳ���� ��ȭ ���� ��������")]
    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;
    public Sprite[] HWooOnOffSprites;

    // (true: On, false: Off)
    // ĳ���͵��� ����
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

    // ����
    public AudioSource EndingCutSceneMusic;

    void Start()
    {
        sceneTransition = GetComponent<ManageSceneTransition>();
        StartEndingDialogue();

        if (SkipButton != null)
        {
            // SKip ��ư Ŭ�� �̺�Ʈ �߰�
            SkipButton.onClick.AddListener(ClickSkipButton);
        }
    }
    void Update()
    {
        // ��ȭ ���൵�� üũ�ϴ� �κе� Update()���� ó���մϴ�.
        DiaAction();

        if (skipAction)
        {
            //  Debug.Log("��ŵ �ߵ�");
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
        if (isDialogue) // ��ȭ�� �̸鼭
        {

            if (isNext) // ���� Ű �Է��� ������ ��
            {
                if (Input.GetMouseButtonDown(0)) // ���콺 ��ư�� ���� ��
                {

                    isNext = false;
                    TalkText.text = "";


                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());

                        if (lineCount == 25 && contextCount == 2)
                        {
                            //   Debug.Log("�ҸӴ� �ƽ� 2");
                            SecondGrandMotherRememberCutScene();
                        }
                    }

                    else
                    {
                        // ��� ī��Ʈ �ʱ�ȭ
                        contextCount = 0;

                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());

                            if (lineCount == 12)
                            {
                                //    Debug.Log("�ƽ� ���.");
                                ActiveCutScene();
                            }

                            if (lineCount == 20)
                            {
                                //     Debug.Log("�ҸӴ� �ƽ� 1");
                                FirstGrandMotherRememberCutScene();
                            }

                            if (lineCount == 26)
                            {
                                //    Debug.Log("������ ���� �ƽ�");
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
                            sceneTransition.FadeScene(10); // ����ǥ ������ �̵�..
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


    // ���� ��ȭ �ϴ� ����� �̸��� �ľ��Ͽ� �˸��� ���º�ȭ�� �̾����� ��.
    void ChangeCharactoreImage()
    {
        if (dialogues[lineCount].name == "������")
        {
            ManageChangeState(false, true, false);
        }


        else if (dialogues[lineCount].name == "������")
        {
            ManageChangeState(true, false, false);
        }

        else if (dialogues[lineCount].name == "����")
        {
            ManageChangeState(false, false, true);

        }

        else
        {
            // �߰��� �����̼� ����� ��Ȳ..
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

    // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ �Լ�
    void InitializeCharacterState()
    {
        ManageChangeState(true, false, false);
    }


    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip ��ư ���� �� �ٽ� ��ȭ ������ ���� ���������� ��ȭ ���θ� false�� ����
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;
        sceneTransition.FadeScene(10); // ����ǥ ������ �̵�..
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
