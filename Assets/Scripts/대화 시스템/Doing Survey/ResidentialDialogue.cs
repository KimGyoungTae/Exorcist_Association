using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentialDialogue : TextDialogSystem
{
    [Space]
    [Header("ĳ���� ��������")]
    public GameObject KHS;
    public GameObject ResidentialNPC;
    public GameObject NameTag;

    [Space]
    [Header("ĳ���� ��ȭ ���� ��������")]
    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] ResidentialNPCOnOffSprites;

    // (true: On, false: Off)
    // ĳ���͵��� ����
    private bool KHSStateOnOff;
    private bool ResidentialNPCOnOff;

    private bool skipAction = false;

    [SerializeField] private GameObject ResidentialBackGround;
    [SerializeField] private GameObject ResidentialCutScene;


    // Start is called before the first frame update
    void Start()
    {
        VisitManager.instance.VisitScene();

        // ��ȭ�� ���۵��� �ʾ����� ǥ��
        dialogueStarted = false;

        if (SkipButton != null)
        {
            // SKip ��ư Ŭ�� �̺�Ʈ �߰�
            SkipButton.onClick.AddListener(ClickSkipButton);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDialogueState();

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
                            ChangeCharactoreImage();
                            ShowDialogueName();

                            if (lineCount == 2)
                            {
                                ChangeScene();
                            }
                        }

                        // ��� ��ȭ�� ������ ��
                        else
                        {
                            EndDialogue();
                            dialogueStarted = false;
                            //  afterInteraction = true;
                            //  startInteraction = false;
                        }
                    }
                }
            }
        }
    }

    public override void ChangeCharacterUI(string tag, string name)
    {
        if (tag == "NPC")
        {
            CharactorOff(isDialogue);
        }
    }

    void CharactorOff(bool isAction)
    {
        KHS.SetActive(isAction);
        ResidentialNPC.SetActive(isAction);
        NameTag.SetActive(isAction);
    }

    // ���� ��ȭ �ϴ� ����� �̸��� �ľ��Ͽ� �˸��� ���º�ȭ�� �̾����� ��.
    void ChangeCharactoreImage()
    {
        if (dialogues[lineCount].name == "� ����")
        {
            ManageChangeState(false, true);
        }


        else if (dialogues[lineCount].name == "������")
        {
            ManageChangeState(true, false);
        }

        else Debug.Log("ĳ���� ���� ��ȭ�� �̷������ �ʽ��ϴ�.");
    }


    void ManageChangeState(bool parm_KHSStateOnOff, bool parm_ResidentialNPCOnOff)
    {
        // �������� ���¸� �����մϴ�.
        KHSStateOnOff = parm_KHSStateOnOff;
        // true�̸� 0�� �ε����� On �̹�����, false�̸� 1�� �ε����� Off �̹����� �����ɴϴ�.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
        // �������� SpriteRenderer�� ����Ͽ� �̹����� �����մϴ�.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

        ResidentialNPCOnOff = parm_ResidentialNPCOnOff;
        Sprite NPCSprite = ResidentialNPCOnOffSprites[ResidentialNPCOnOff ? 0 : 1];
        ResidentialNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
    }


    // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ �Լ�
    void InitializeCharacterState()
    {
        ManageChangeState(false, false);
    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip ��ư ���� �� �ٽ� ��ȭ ������ ���� ���������� ��ȭ ���θ� false�� ����
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;
    }

    void ChangeScene()
    {
        CharactorOff(false);
        LeanTween.alpha(ResidentialBackGround, 0f, 1f).setDelay(0.3f);
        LeanTween.alpha(ResidentialCutScene, 1f, 1f);
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
        LeanTween.alpha(ResidentialCutScene, 0f, 1f);
        LeanTween.alpha(ResidentialBackGround, 1f, 1f);
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
