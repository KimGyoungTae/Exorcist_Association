// �� �ʿ� ��� ��� �̹����� Ŭ������ �� ������ ��ȭ�� ���� �ڵ�

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickType : MonoBehaviour
{
    Dialogue[] dialogues;
    bool isDialogue = false; // ��ȭ���� ��� true.
    bool isNext = false; // Ư�� Ű �Է� ���.

    int lineCount = 0; // ��� ��ȭ ī��Ʈ.
    int contextCount = 0; //��� ī��Ʈ

    public GameObject talkPanel;
    public Text TalkText;

    public InteractionEvent interactionEvent;
   
    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;


    [Space]
    [Header("��ȭ ��� ��������")]
    public GameObject KHS;
    public GameObject Partner;
    public GameObject DialogueNameTag;


    bool dialogueStarted = false; // ��ȭ ���� ���θ� üũ
    bool afterInteraction = false;

    void Start()
    {
        // ��ȭ�� ���۵��� �ʾ����� ǥ��
        dialogueStarted = false;
    }

    void Update()
    {
        CheckDialogueState();

        DiaAction(); // ��ȭ ���൵�� üũ�ϴ� �κе� Update()���� ó���մϴ�.
    }

    void CheckDialogueState()
    {
        if (Input.GetMouseButtonDown(0) && !dialogueStarted) // ��ȭ�� ���۵��� ���� ���¿����� ��ȭ ����
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


            if (hit.collider != null && hit.collider.CompareTag("NPC")) // "NPC" �±׷� �ٲټ���
            {
                ShowDialogue(interactionEvent.GetDialogue(afterInteraction));
                dialogueStarted = true; // ��ȭ�� ���۵Ǿ����� ǥ��

            }
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
    }

    IEnumerator TypeWriter()
    {
        // ��ȭ UI Ȱ��ȭ
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");


        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            if (t_ReplaceText[i] == '|')
            {
                Debug.Log("2��° �� �̾��..");
                TalkText.text += "\n";

                // "\n"�� �������� ���ڿ��� �����ϴ�.
                string[] twoLines = t_ReplaceText.Split('|');
               
                // Debug.Log(twoLines[1]);
                string twoLineText = twoLines[1];

                // Debug.Log(t_ReplaceText[i]);
                t_ReplaceText = t_ReplaceText.Replace('|', ' ');

                // Debug.Log(twoLineText.Length);
             for (char j = t_ReplaceText[i + 1];  j < twoLineText.Length; j++)
             {
                    TalkText.text += twoLineText[j];
                    yield return new WaitForSeconds(textDelay);
             }

            }

            TalkText.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
    }

    void SettingUI(bool isAction)
    {
        talkPanel.SetActive(isAction);
        KHS.SetActive(isAction);
        Partner.SetActive(isAction);
        DialogueNameTag.SetActive(isAction);

        // NamePanel ���⿡ �ֱ�.
    }

}


