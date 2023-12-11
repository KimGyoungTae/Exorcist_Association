using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeSurveyDialogue : TextDialogSystem
{

    [Space]
    [Header("��ȭ ��� ��������")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HidePanel;
    public InteractionEvent interactionEvent;

    private bool afterInteraction = false;

    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;

    // (true: On, false: Off)
    // ĳ���͵��� ����
    private bool KHSStateOn = false;
    private bool CJWStateOn = true;

    // ���� ������ �� �� �� �ѹ� ������ �����ϱ� ���� ���� ����
    // ���� �ʿ��� �����ϱ� �� ��ȭ�� ���� �Ŀ� �ٸ� ������ �̵� ��, �ٽ� ���� ������ ���ƿ� ��
    // ���� �� ��ȭ�� ��� ����Ǵ� ������ ���� ������.
    private static bool hasStarted = false;
    private bool skipAction = false;

    private void Start()
    {
        if (!hasStarted)
        {
            StartDialogSurvey();
            //   Debug.Log("���̵� ���� ��ȭ ����");
        }

        hasStarted = true;

        if (SkipButton != null)
        {
            // SKip ��ư Ŭ�� �̺�Ʈ �߰�
            SkipButton.onClick.AddListener(ClickSkipButton);
        }
    }

    private void Update()
    {
        DiaAction();

        if (skipAction)
        {
            //  Debug.Log("��ŵ �ߵ�");
            StopAllCoroutines();
            skipAction = false;
            return;
        }
    }


    // 2023.10.25 ���� �ӹ���ø ��ư�� Ŭ������ �� ������ �߸� â�� 
    // ??? ��ư Ŭ�� �÷� �ٲ��ְ� ��..
    // ���߿� ���� ���� �鰡�� �� ��ȭ �� ����� �Ѵٸ� BattleStart()�޼��带 �ҷ����� ��..
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

                        // �� ĳ������ ��縦 ��� ��ĥ �� �̹��� ���� On / OFF ����
                        ChangeCharactoreImage();


                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                            ShowDialogueName();
                        }

                        // ��� ��ȭ�� ������ ��
                        else
                        {
                            EndDialogue();
                            afterInteraction = true;

                            //   Debug.Log("��ȭ ������ ���� ���� ����");

                        }
                    }
                }
            }
        }
    }



    void ChangeCharactoreImage()
    {
        // �������� ���¸� �����մϴ�.
        KHSStateOn = !KHSStateOn;
        // true�̸� 0�� �ε����� On �̹�����, false�̸� 1�� �ε����� Off �̹����� �����ɴϴ�.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOn ? 0 : 1];
        // �������� SpriteRenderer�� ����Ͽ� �̹����� �����մϴ�.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;


        // �������� ���¸� �����մϴ�.
        CJWStateOn = !CJWStateOn;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOn ? 0 : 1];
        CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip ��ư ���� �� �ٽ� ��ȭ ������ ���� ���������� ��ȭ ���θ� false�� ����
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
