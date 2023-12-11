using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeBattleDialogue : TextDialogSystem
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
    [SerializeField] private GameObject SurveyMainMap;
    [SerializeField] private GameObject Ghost;


    void Start()
    {
        sceneTransition = GetComponent<ManageSceneTransition>();
        StartBeforeBattleDialogue();

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
            StopAllCoroutines();
            skipAction = false;
            return;
        }
    }

    void StartBeforeBattleDialogue()
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
                    }

                    else
                    {

                        // ��� ī��Ʈ �ʱ�ȭ
                        contextCount = 0;

                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());

                            if (lineCount == 6)
                            {
                             //   Debug.Log("�� ��ȭ");
                                ChangeScene();
                            }

                            ChangeCharactoreImage();
                            ShowDialogueName();
                        }

                        // ��� ��ȭ�� ������ ��
                        else
                        {
                            EndDialogue();
                            // ������ ���� ������ �̵��Ѵ�..
                            sceneTransition.FadeScene(8);
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


        else if (dialogues[lineCount].name == "������")
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

        else if (dialogues[lineCount].name == "����")
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

        else if (dialogues[lineCount].name == "�ǹ��� �ͽ�")
        {
            ShowGhost();
        }

        else
        {
            // �߰��� �����̼� ����� ��Ȳ..
            KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
            CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
            HWoo.GetComponent<SpriteRenderer>().sprite = HWooOnOffSprites[1];
        }

    }

    // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ �Լ�
    void InitializeCharacterState()
    {
        KHSStateOnOff = false;
        CJWStateOnOff = true;
        HWooStateOnOff = false;

        // �ʱ� ĳ���� �̹��� ����
        KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[0];
        CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
        HWoo.GetComponent<SpriteRenderer>().sprite = HWooOnOffSprites[1];
    }


    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip ��ư ���� �� �ٽ� ��ȭ ������ ���� ���������� ��ȭ ���θ� false�� ����
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;

        // ���� ������ �̵��Ѵ�..
        sceneTransition.FadeScene(8);
    }

    void ChangeScene()
    {
        CharactorOff(false);
        LeanTween.alpha(SurveyMainMap, 0f, 1f).setDelay(0.3f);
        LeanTween.alpha(BattleBackGround, 1f, 1f);
    }

    void ShowGhost()
    {
        CharactorOff(false);
        LeanTween.alpha(Ghost, 1f, 1f).setDelay(0.3f);
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
