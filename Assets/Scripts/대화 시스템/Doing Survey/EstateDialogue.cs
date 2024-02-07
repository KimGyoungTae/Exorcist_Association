using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;


public class EstateDialogue : TextDialogSystem
{
    [Space]
    [Header("ĳ���� ��������")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject EstateNPC;
    public GameObject NameTag;

    [Space]
    [Header("ĳ���� ��ȭ ���� ��������")]
    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;
    public Sprite[] EstateNPCOnOffSprites;

    // (true: On, false: Off)
    // ĳ���͵��� ����
    private bool KHSStateOnOff;
    private bool CJWStateOnOff;
    private bool EstateNPCOnOff;

    [Space]
    [Header("��׶��� ������Ʈ ��������")]
    public GameObject BackGroundContract;
    public GameObject BackGroundLeaflet;

    private bool skipAction = false;

    private void Start()
    {
        VisitManager.instance.VisitScene();

        // ��ȭ�� ���۵��� �ʾ����� ǥ��
        dialogueStarted = false;
        BackGroundObjectOff(false);

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

        // Skip ��ư Ŭ�� ���� �ڷ�ƾ�� �� �� ������ �Ǹ鼭 ���� �����Ͱ� Text�� �����ִ� ������ �߻�
        // ��ư Ŭ�� ���θ� �Ǵ��Ͽ� ���� Skip ��ư�� Ŭ�� �ߴٸ�
        // ����Ǿ� �ִ� �ڷ�ƾ�� ��� �����ϸ鼭, �����Ͱ� Text�� ���ŵǴ� ������ ������.
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

                            // ������ ������Ʈ ��ȣ�ۿ뵵 SetActive(false)�� �����
                            BackGroundObjectOff(false);
                        }
                    }
                }
            }
        }
    }



    public override void ChangeCharacterUI(string tag, string name)
    {
        if (tag == "Item")
        {
            CharactorOff(false);

            // Name�� ���� ĳ���� & ��׶��� �̹��� ���̰� �� �� ���� �Ǵ� �̾��..
            if (name == "�簳��")
            {
                BackGroundContract.SetActive(true);
                HitCheckObject(hit);
            }

            else if (name == "���� ����Ʈ")
            {
                BackGroundLeaflet.SetActive(true);
                HitCheckObject(hit);
            }

            else Debug.Log("��׶��� ������Ʈ�� �������� �ʽ��ϴ�..");
        }

        if (tag == "NPC")
        {
            CharactorOff(isDialogue);
        }
    }



    void HitCheckObject(RaycastHit2D targeted)
    {
        ItemSaves clickInterface = targeted.transform.GetComponent<ItemSaves>();

        if (clickInterface != null)
        {
            InventoryItem item = clickInterface.ClickItem();
            InventoryManager.Instance.Add(item);
            //target = null;
            //currenttarget = null;
        }
    }

    void BackGroundObjectOff(bool isAction)
    {
        BackGroundContract.SetActive(isAction);
        BackGroundLeaflet.SetActive(isAction);
    }

    void CharactorOff(bool isAction)
    {
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        EstateNPC.SetActive(isAction);
        NameTag.SetActive(isAction);
    }

    // ���� ��ȭ �ϴ� ����� �̸��� �ľ��Ͽ� �˸��� ���º�ȭ�� �̾����� ��.
    void ChangeCharactoreImage()
    {
        if (dialogues[lineCount].name == "�ε��� ����")
        {
            ManageChangeState(false, false, true);
        }

        else if (dialogues[lineCount].name == "������")
        {
            ManageChangeState(false, true, false);
        }

        else if (dialogues[lineCount].name == "������")
        {
            ManageChangeState(true, false, false);
        }
        else Debug.Log("ĳ���� ���� ��ȭ�� ������� �ʾҽ��ϴ�");
    }

    void ManageChangeState(bool parm_KHSStateOnOff, bool parm_CJWStateOnOff, bool parm_EstateNPCOnOff)
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

        EstateNPCOnOff = parm_EstateNPCOnOff;
        Sprite NPCSprite = EstateNPCOnOffSprites[EstateNPCOnOff ? 0 : 1];
        EstateNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
    }

    // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ �Լ�
    void InitializeCharacterState()
    {
        ManageChangeState(false, false, true);
    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip ��ư ���� �� �ٽ� ��ȭ ������ ���� ���������� ��ȭ ���θ� false�� ����
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;

        // ������ ������Ʈ ��ȣ�ۿ뵵 SetActive(false)�� �����
        BackGroundObjectOff(false);
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
