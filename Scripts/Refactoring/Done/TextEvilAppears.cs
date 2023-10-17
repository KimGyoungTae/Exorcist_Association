using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextEvilAppears : TextDialogSystem
{
    [Space]
    public GameObject HidePanel;
    public ManageSceneTransition sceneTransition;
    public InteractionEvent interactionEvent;

    private bool afterInteraction = false;

    //// (true: On, false: Off)
    //// ĳ���͵��� ����
    //private bool KHSStateOn = true;
    //private bool CJWStateOn = false;

    private void Update()
    {
        DiaAction();
    }

    public void BattleStart()
    {
        // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ
        InitializeCharacterState();

        ShowDialogue(interactionEvent.GetDialogue(afterInteraction));

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
                        }

                        // ��� ��ȭ�� ������ ��
                        else
                        {
                            EndDialogue();
                            afterInteraction = true;

                            // ���� ���� �� ��ȯ
                            sceneTransition.FadeScene(7);
                            Debug.Log("���� ���� ���");

                            // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ
                            InitializeCharacterState();

                        }
                    }
                }
            }
        }
    }


    // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ �Լ�
    void InitializeCharacterState()
    {
        KHSStateOn = true;
        PartnerStateOn = false;

        // �ʱ� ĳ���� �̹��� ����
        KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[0];
        Partner.GetComponent<SpriteRenderer>().sprite = PartnerOnOffSprites[1];
    }


    public override void SettingUI(bool isAction)
    {
        base.SettingUI(isAction);
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

    public override IEnumerator TypeWriter()
    {
        return base.TypeWriter();
    }

    public override void ChangeCharactoreImage()
    {
        base.ChangeCharactoreImage();
    }
}
