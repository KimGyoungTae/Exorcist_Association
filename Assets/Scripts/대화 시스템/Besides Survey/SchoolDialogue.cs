using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolDialogue : TextDialogSystem
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



    void Start()
    {
        sceneTransition = GetComponent<ManageSceneTransition>();
        StartSchoolDialogue();

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
                            //    Debug.Log(dialogues[lineCount].name);

                            ChangeCharactoreImage();
                            ShowDialogueName();
                        }

                        // ��� ��ȭ�� ������ ��
                        else
                        {
                            EndDialogue();
                            // ��� ��� ��� ���� �� ���� �������� �� �̵� 
                            sceneTransition.FadeScene(4);

                        }
                    }
                }
            }
        }
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

        else if (dialogues[lineCount].name == "�� ��")
        {
            ManageChangeState(false, false, true);
        }

        else Debug.Log("ĳ���� ���� ��ȭ�� �̷������ �ʽ��ϴ�.");

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
        ManageChangeState(false, true, false);
    }


    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip ��ư ���� �� �ٽ� ��ȭ ������ ���� ���������� ��ȭ ���θ� false�� ����
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
