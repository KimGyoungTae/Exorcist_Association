// ��� : ??? ��ư�� ���� �߸� �۾��� ����ϰ� ���� ������ �̵��ϱ� ���� ��ȭ
// ĳ���͵��� ���¿� ���� On/Off �̹��� �ٲٴ� ������ �� �ڵ忡�� �����ϸ� ���� �� ����..

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantDialog : MonoBehaviour
{
    public InteractionEvent interactionEvent;
    public ManageSceneTransition sceneTransition;


    Dialogue[] dialogues;
    bool isDialogue = false; // ��ȭ���� ��� true.
    bool isNext = false; // Ư�� Ű �Է� ���.

    int lineCount = 0; // ��� ��ȭ ī��Ʈ.
    int contextCount = 0; //��� ī��Ʈ

    public GameObject talkPanel;
    public Text TalkText;


    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;

    [Space]
    [Header("��ȭ ��� ��������")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HidePanel;

    
    bool afterInteraction = false;


    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;

    public GameObject KHSObject;
    public GameObject CJWObject;

    // (true: On, false: Off)
    // ĳ���͵��� ����
    private bool KHSStateOn = true;
    private bool CJWStateOn = false;



    void Update()
    {    
        DiaAction(); // ��ȭ ���൵�� üũ�ϴ� �κе� Update()���� ó���մϴ�.
    }

    // ��ȭ ���� �� ĳ���� ���� �ʱ�ȭ �Լ�
    void InitializeCharacterState()
    {
        KHSStateOn = true;
        CJWStateOn = false;

        // �ʱ� ĳ���� �̹��� ����
        KHSObject.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[0];
        CJWObject.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
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


    public void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        isDialogue = true; // ��ȭ�� ������ �� "��ȭ���̴�". �˸�
        TalkText.text = "";
        dialogues = Parm_dialogues;

        StartCoroutine(TypeWriter());
    }


    void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;

        SettingUI(false); // ��ȭ UI ����

    
}

    IEnumerator TypeWriter()
    {
        //��ȭ UIȰ��ȭ
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            TalkText.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;

    }

    void SettingUI(bool isAction)
    {
        talkPanel.SetActive(isAction);
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        HidePanel.SetActive(!isAction);

        // NamePanel ���⿡ �ֱ�.
    }


    public void ChangeCharactoreImage()
    {
        // �������� ���¸� �����մϴ�.
        KHSStateOn = !KHSStateOn;
        // true�̸� 0�� �ε����� On �̹�����, false�̸� 1�� �ε����� Off �̹����� �����ɴϴ�.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOn ? 0 : 1];
        // �������� SpriteRenderer�� ����Ͽ� �̹����� �����մϴ�.
        KHSObject.GetComponent<SpriteRenderer>().sprite = KHSSprite;


        // �������� ���¸� �����մϴ�.
        CJWStateOn = !CJWStateOn;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOn ? 0 : 1];
        CJWObject.GetComponent<SpriteRenderer>().sprite = CJWSprite;

    }

}

