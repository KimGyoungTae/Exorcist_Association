// �� ĳ���� 3��� ������ ��ȭ�� ���� �ӽ� �ڵ� 

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StreetDialog : MonoBehaviour
{
    Dialogue[] dialogues;
    bool isDialogue = false; // ��ȭ���� ��� true.
    bool isNext = false; // Ư�� Ű �Է� ���.

    int lineCount = 0; // ��� ��ȭ ī��Ʈ.
    int contextCount = 0; //��� ī��Ʈ

    public GameObject talkPanel;
    public Text TalkText;

    //public InteractionEvent interactionEvent;
    public ItemInteraction itemInteraction;

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;


    [Space]
    [Header("��ȭ ��� ��������")]
    public GameObject KHS;
    public GameObject Partner1;
    public GameObject Partner2;
    public GameObject Partner3;

    public GameObject DialogueNameTag1;
    public GameObject DialogueNameTag2;
    public GameObject DialogueNameTag3;

    bool dialogueStarted = false; // ��ȭ ���� ���θ� üũ
    bool afterInteraction = false;



    void Start()
    {
        // ��ȭ�� ���۵��� �ʾ����� ǥ��
        dialogueStarted = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !dialogueStarted) // ��ȭ�� ���۵��� ���� ���¿����� ��ȭ ����
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


            if (hit.collider != null && hit.collider.CompareTag("NPC")) // "NPC" �±׷� �ٲټ���
            {
                // ShowDialogue(interactionEvent.GetDialogue(afterInteraction));
                ShowDialogue(itemInteraction.GetItemDialogue(hit.collider.name));
                dialogueStarted = true; // ��ȭ�� ���۵Ǿ����� ǥ��

                CharactorSetting(hit.collider.name);
            }
        }

        DiaAction(); // ��ȭ ���൵�� üũ�ϴ� �κе� Update()���� ó���մϴ�.
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
                        }

                        // ��� ��ȭ�� ������ ��
                        else
                        {
                            EndDialogue();
                            dialogueStarted = false;
                            afterInteraction = true;
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
        CharactorOff(false);
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
        // Partner.SetActive(isAction);
       //  DialogueNameTag.SetActive(isAction);

        // NamePanel ���⿡ �ֱ�.

    }

    void CharactorSetting(string ActorName)
    {
        // Debug.Log(ActorName);

        if (ActorName == "�����п�")
        {
            Partner1.SetActive(isDialogue);
            DialogueNameTag1.SetActive(isDialogue);

        }

        if (ActorName == "ī��")
        {
            Partner2.SetActive(isDialogue);
            DialogueNameTag2.SetActive(isDialogue);
        }

        if (ActorName == "����")
        {
            Partner3.SetActive(isDialogue);
            DialogueNameTag3.SetActive(isDialogue);
        }
    }

    void CharactorOff(bool isAction)
    {
        Partner1.SetActive(isAction);
        Partner2.SetActive(isAction);
        Partner3.SetActive(isAction);

        DialogueNameTag1.SetActive(isAction);
        DialogueNameTag2.SetActive(isAction);
        DialogueNameTag3.SetActive(isAction);
    }

}
