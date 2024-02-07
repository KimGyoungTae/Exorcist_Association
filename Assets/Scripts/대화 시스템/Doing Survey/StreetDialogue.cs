using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreetDialogue : TextDialogSystem
{

    [Space]
    [Header("ĳ���� ��������")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HWoo;
    public GameObject AcademyNPC;
    public GameObject CafeNPC;
    public GameObject FlowerNPC;

    public GameObject AcademyTag;
    public GameObject CafeTag;
    public GameObject FlowerTag;

    [Space]
    [Header("ĳ���� ��ȭ ���� ��������")]
    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;
    public Sprite[] HWooOnOffSprites;
    public Sprite[] AcademyNPCOnOffSprites;
    public Sprite[] CafeNPCOnOffSprites;
    public Sprite[] FlowerNPCOnOffSprites;

    // (true: On, false: Off)
    // ĳ���͵��� ����
    private bool KHSStateOnOff;
    private bool CJWStateOnOff;
    private bool HWooStateOnOff;
    private bool AcademyNPCOnOff;
    private bool CafeNPCOnOff;
    private bool FlowerNPCOnOff;

    private bool skipAction = false;



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
                            //   Debug.Log(dialogues[lineCount].name);

                            ChangeCharactoreImage();
                            ShowDialogueName();
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
            if (name == "�����п�")
            {
                KHS.SetActive(isDialogue);
                CJW.SetActive(isDialogue);
                AcademyNPC.SetActive(isDialogue);
                AcademyTag.SetActive(isDialogue);
            }

            else if (name == "ī��")
            {
                KHS.SetActive(isDialogue);
                CJW.SetActive(isDialogue);
                HWoo.SetActive(isDialogue);
                CafeNPC.SetActive(isDialogue);
                CafeTag.SetActive(isDialogue);
            }

            else if (name == "����")
            {
                KHS.SetActive(isDialogue);
                FlowerNPC.SetActive(isDialogue);
                FlowerTag.SetActive(isDialogue);
            }

            else Debug.Log("������Ʈ �̸��� ��Ī�� �߸��Ǿ����ϴ�..");
        }

        else Debug.Log("������Ʈ �±׸� ã�� �� �����ϴ�..");
    }

    void CharactorOff(bool isAction)
    {
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        HWoo.SetActive(isAction);

        AcademyNPC.SetActive(isAction);
        CafeNPC.SetActive(isAction);
        FlowerNPC.SetActive(isAction);

        AcademyTag.SetActive(isAction);
        CafeTag.SetActive(isAction);
        FlowerTag.SetActive(isAction);
    }

    // ���� ��ȭ �ϴ� ����� �̸��� �ľ��Ͽ� �˸��� ���º�ȭ�� �̾����� ��.
    void ChangeCharactoreImage()
    {
        if (hit.collider.name == "�����п�")
        {
            if (dialogues[lineCount].name == "����")
            {
                ManageAcademyChangeState(false, false, true);
            }

            else if (dialogues[lineCount].name == "������")
            {
                ManageAcademyChangeState(false, true, false);
            }

            else if (dialogues[lineCount].name == "������")
            {
                ManageAcademyChangeState(true, false, false);
            }

            else
            {
                // �߰��� �����̼� ����� ��Ȳ..
                KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
                AcademyNPC.GetComponent<SpriteRenderer>().sprite = AcademyNPCOnOffSprites[1];
            }

        }

        else if (hit.collider.name == "ī��")
        {
            if (dialogues[lineCount].name == "ī�� ����")
            {
                ManageCafeChangeState(false, false, false, true);
            }
            else if (dialogues[lineCount].name == "����")
            {
                ManageCafeChangeState(false, false, true, false);
            }

            else if (dialogues[lineCount].name == "������")
            {
                ManageCafeChangeState(false, true, false, false);
            }

            else if (dialogues[lineCount].name == "������")
            {
                ManageCafeChangeState(true, false, false, false);
            }

            else
            {
                // �����̼� ���..
            }
        }


        else if (hit.collider.name == "����")
        {
            if (dialogues[lineCount].name == "���� ����")
            {
                ManageFlowerChangeState(false, true);
            }

            else if (dialogues[lineCount].name == "������")
            {
                ManageFlowerChangeState(true, false);
            }

            else
            {
                // �����̼� ���..
            }
        }

        else Debug.Log("ĳ���� ���� ��ȭ�� �̷������ �ʽ��ϴ�.");

    }

    void ManageAcademyChangeState(bool parm_KHSStateOnOff, bool parm_CJWStateOnOff, bool parm_AcademyNPCOnOff)
    {
        // �������� ���¸� �����մϴ�.
        KHSStateOnOff = parm_KHSStateOnOff;
        // true�̸� 0�� �ε����� On �̹�����, false�̸� 1�� �ε����� Off �̹����� �����ɴϴ�.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
        // �������� SpriteRenderer�� ����Ͽ� �̹����� �����մϴ�.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

        // �������� ���¸� �����մϴ�.
        CJWStateOnOff = parm_CJWStateOnOff;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
        CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

        AcademyNPCOnOff = parm_AcademyNPCOnOff;
        Sprite NPCSprite = AcademyNPCOnOffSprites[AcademyNPCOnOff ? 0 : 1];
        AcademyNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
    }

    void ManageCafeChangeState(bool parm_KHSStateOnOff, bool parm_CJWStateOnOff, bool parm_HWooStateOnOff, bool parm_CafeNPCOnOff)
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

        CafeNPCOnOff = parm_CafeNPCOnOff;
        Sprite NPCSprite = CafeNPCOnOffSprites[CafeNPCOnOff ? 0 : 1];
        CafeNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
    }

    void ManageFlowerChangeState(bool parm_KHSStateOnOff, bool parm_FlowerNPCOnOff)
    {
        KHSStateOnOff = parm_KHSStateOnOff;
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

        FlowerNPCOnOff = parm_FlowerNPCOnOff;
        Sprite NPCSprite = FlowerNPCOnOffSprites[FlowerNPCOnOff ? 0 : 1];
        FlowerNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
    }


    // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ �Լ�
    void InitializeCharacterState()
    {
        if (hit.collider.name == "�����п�")
        {
            ManageAcademyChangeState(false, true, false);
        }

        else if (hit.collider.name == "ī��")
        {
            ManageCafeChangeState(false, false, false, false);
        }

        else if (hit.collider.name == "����")
        {
            ManageFlowerChangeState(false, false);
        }

        else Debug.Log("�ݶ��̴� �̸��� ã�� �� �����ϴ�");
    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip ��ư ���� �� �ٽ� ��ȭ ������ ���� ���������� ��ȭ ���θ� false�� ����
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;
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
