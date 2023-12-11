using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ��ȭ �ý��� ���� �� ����� ������ �Լ� ���� ��� �����ϱ� ���� �߻� Ŭ���� ����
/// </summary>
public abstract class TextDialogSystem : MonoBehaviour
{

    [Header("��ȭ ��� UI ��������")]
    [SerializeField] protected GameObject talkPanel;
    [SerializeField] protected Text TalkText;
    [SerializeField] protected Button SkipButton;

    [Header("��ȭ �̸� UI ��������")]
    [SerializeField] protected GameObject namePanel;
    [SerializeField] protected Text nameText;

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;

  
    protected Dialogue[] dialogues;
    protected bool isDialogue = false; // ��ȭ���� ��� true.
    protected bool isNext = false; // Ư�� Ű �Է� ���.

    protected int lineCount = 0; // ��� ��ȭ ī��Ʈ.
    protected int contextCount = 0; //��� ī��Ʈ


    // ��ȭ ���°� ��ȣ�ۿ� ��/�� ���� �Ǵ���.
    [SerializeField] protected DialogueInteraction interaction;
    protected bool dialogueStarted = false; // ��ȭ ���� ���θ� üũ
    protected bool startInteraction = false;
    [SerializeField] protected string currentFileName;

    protected RaycastHit2D hit;

    public virtual void CheckDialogueState()
    {
        // ��ȭ�� ���۵��� ���� ���¿����� ��ȭ ����
        if (Input.GetMouseButtonDown(0) && !dialogueStarted) 
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


            if (hit.collider != null)
            {
                
                currentFileName = DataBaseManager.instance.csv_FileName;
                ShowDialogue(interaction.GetDialogueContents(hit.collider.tag, hit.collider.name, currentFileName));

                ChangeCharacterUI(hit.collider.tag, hit.collider.name);

                dialogueStarted = true; // ��ȭ�� ���۵Ǿ����� ǥ��
                startInteraction = true;

                if (startInteraction == true)
                {
                  //  Debug.Log(dialogues[lineCount].name);
                    ShowDialogueName();
                    startInteraction = false;
                }
            }
        }
    }


    /// <summary>
    /// �� �ʿ��� �������� ĳ����, ������ ������ ���� �ٸ��Ƿ� 
    /// ������ ��ũ��Ʈ�� �˸°� ������ ���� �߻�޼ҵ�� ���� ��.
    /// Tag or Name�� ���� UI�� ��� ������ ������ �Ǵ��Ѵ�.
    /// </summary>
    /// <param name="tag">��ȣ�ۿ� �� �±� �̸�</param>
    /// <param name="name">��ȣ�ۿ� ������Ʈ �̸�</param>
    public abstract void ChangeCharacterUI(string tag, string name);


    public virtual void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        isDialogue = true; // ��ȭ�� ������ �� "��ȭ���̴�". �˸�
        TalkText.text = "";
        dialogues = Parm_dialogues;

        StartCoroutine(TypeWriter());
    }


    public virtual void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;

        SettingUI(false); // ��ȭ UI ����
    }

    public virtual void ShowDialogueName()
    {
        nameText.text = dialogues[lineCount].name;
    }

    public virtual IEnumerator TypeWriter()
    {
        // ��ȭ UI Ȱ��ȭ
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");


        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            if (t_ReplaceText[i] == '|')
            {
              //  Debug.Log("2��° �� �̾��..");
                TalkText.text += "\n";

                // "\n"�� �������� ���ڿ��� �����ϴ�.
                string[] twoLines = t_ReplaceText.Split('|');

                // Debug.Log(twoLines[1]);
                string twoLineText = twoLines[1];

                // Debug.Log(t_ReplaceText[i]);
                t_ReplaceText = t_ReplaceText.Replace('|', ' ');

                // Debug.Log(twoLineText.Length);
                for (char j = t_ReplaceText[i + 1]; j < twoLineText.Length; j++)
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

    public virtual void SettingUI(bool isAction)
    {
        talkPanel.SetActive(isAction);
      
        // NamePanel ���⿡ �ֱ�.
        namePanel.SetActive(isAction);
    }

}
