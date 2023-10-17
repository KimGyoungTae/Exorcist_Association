// ��ȭ �ý��� ���� �� ����� ������ �Լ��� ��� �����ϱ� ���� �߻� Ŭ���� ����
// ������ ������ �ʵ��� NPC���� ��ȭ���� On/OFF �̹����� ���� ������ �߻� Ŭ���� �̿� �ϱ�.
// ������ ���� NPC���� ��ȣ�ۿ� ��/������ �Ǵ��� ���� �Լ� -> CheckDialogueState()

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public abstract class TextDialogSystem : MonoBehaviour
{
    [Space]
    [Header("��ȭ ���� ��������")]
    public GameObject KHS;
    public GameObject Partner;

    public GameObject talkPanel;
    public Text TalkText;

    [Space]
    [Header("�� ĳ���͵��� ��ȭ ����")]
    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] PartnerOnOffSprites;

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;
    

    protected Dialogue[] dialogues;
    protected bool isDialogue = false; // ��ȭ���� ��� true.
    protected bool isNext = false; // Ư�� Ű �Է� ���.

    protected int lineCount = 0; // ��� ��ȭ ī��Ʈ.
    protected int contextCount = 0; //��� ī��Ʈ

    // (true: On, false: Off)
    // ĳ���͵��� ����
    protected bool KHSStateOn = true;
    protected bool PartnerStateOn = false;

    //// ��ȭ ���°� ��ȣ�ۿ� ��/�� ���� �Ǵ���.
    //protected InteractionEvent interactionEvent;
    //protected bool dialogueStarted = false; // ��ȭ ���� ���θ� üũ
    //protected bool afterInteraction = false;


    
    
    //public virtual void CheckDialogueState()
    //{
    //    if (Input.GetMouseButtonDown(0) && !dialogueStarted) // ��ȭ�� ���۵��� ���� ���¿����� ��ȭ ����
    //    {
    //        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


    //        if (hit.collider != null && hit.collider.CompareTag("NPC")) // "NPC" �±׷� �ٲټ���
    //        {
    //            ShowDialogue(interactionEvent.GetDialogue(afterInteraction));
    //            dialogueStarted = true; // ��ȭ�� ���۵Ǿ����� ǥ��

    //        }
    //    }
    //}

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
                Debug.Log("2��° �� �̾��..");
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
        KHS.SetActive(isAction);
        Partner.SetActive(isAction);
        
        //DialogueNameTag.SetActive(isAction);
        // NamePanel ���⿡ �ֱ�.
    }


    public virtual void ChangeCharactoreImage()
    {
        // �������� ���¸� �����մϴ�.
        KHSStateOn = !KHSStateOn;
        // true�̸� 0�� �ε����� On �̹�����, false�̸� 1�� �ε����� Off �̹����� �����ɴϴ�.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOn ? 0 : 1];
        // �������� SpriteRenderer�� ����Ͽ� �̹����� �����մϴ�.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;


        // �������� ���¸� �����մϴ�.
        PartnerStateOn = !PartnerStateOn;
        Sprite CJWSprite = PartnerOnOffSprites[PartnerStateOn ? 0 : 1];
        Partner.GetComponent<SpriteRenderer>().sprite = CJWSprite;

    }



    //// - Not Working - 
    //// ���� ������ ���� �� �ִ� �ٸ� �޼ҵ���� �����մϴ�.
    //// ĳ���� �̹����� On/Off �ϴ� ������ �����մϴ�.
    //protected void DisplayCharacterImages(bool showCharacter1, bool showCharacter2)
    //{
 
    //    // �������� ���¸� �����մϴ�.
    //    showCharacter1 = !showCharacter1;
    //    // true�̸� 0�� �ε����� On �̹�����, false�̸� 1�� �ε����� Off �̹����� �����ɴϴ�.
    //    Sprite KHSSprite = KHSOnOffSprites[showCharacter1 ? 0 : 1];
    //    // �������� SpriteRenderer�� ����Ͽ� �̹����� �����մϴ�.
    //    KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;


    //    // �������� ���¸� �����մϴ�.
    //    showCharacter2 = !showCharacter2;
    //    Sprite PartnerSprite = PartnerOnOffSprites[showCharacter2 ? 0 : 1];
    //    Partner.GetComponent<SpriteRenderer>().sprite = PartnerSprite;
    //}
}
