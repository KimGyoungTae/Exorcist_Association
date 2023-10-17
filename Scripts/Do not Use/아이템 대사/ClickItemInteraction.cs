//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ClickItemInteraction : MonoBehaviour
//{
//    ItemDialogue[] itemDialogues;

//    bool isDialogue = false; // ��ȭ���� ��� true.
//    bool isNext = false; // Ư�� Ű �Է� ���.

//    int lineCount = 0; // ��� ��ȭ ī��Ʈ.
//    int contextCount = 0; //��� ī��Ʈ

//    public GameObject talkPanel;
//    public Text TalkText;

//    public ItemInteraction itemInteraction;

//    [Header("�ؽ�Ʈ ��� ������")]
//    [SerializeField] float textDelay;

//    bool dialogueStarted = false; // ��ȭ ���� ���θ� üũ
//    bool afterInteraction = false;

//    void Start()
//    {
//        // ��ȭ�� ���۵��� �ʾ����� ǥ��
//        dialogueStarted = false;
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && !dialogueStarted) // ��ȭ�� ���۵��� ���� ���¿����� ��ȭ ����
//        {
//            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


//            if (hit.collider != null && hit.collider.CompareTag("Item"))
//            {
//               // Debug.Log(hit.collider.gameObject.name);

//                GameObject clickedObject = hit.collider.gameObject;
//                ShowDialogue(itemInteraction.GetItemDialogue(afterInteraction, clickedObject));
//                dialogueStarted = true; // ��ȭ�� ���۵Ǿ����� ǥ��
//            }
//        }

//        DiaAction(); // ��ȭ ���൵�� üũ�ϴ� �κе� Update()���� ó���մϴ�.
//    }

//    void DiaAction()
//    {
//        if (isDialogue) // ��ȭ�� �̸鼭
//        {
//            if (isNext) // ���� Ű �Է��� ������ ��
//            {
//                if (Input.GetMouseButtonDown(0)) // ���콺 ��ư�� ���� ��
//                {

//                    isNext = false;
//                    TalkText.text = "";

//                    if (++contextCount < itemDialogues[lineCount].contexts.Length)
//                    {
//                        StartCoroutine(TypeWriter());
//                    }

//                    else
//                    {
//                        // ��� ī��Ʈ �ʱ�ȭ
//                        contextCount = 0;

//                        if (++lineCount < itemDialogues.Length)
//                        {
//                            StartCoroutine(TypeWriter());
//                        }

//                        // ��� ��ȭ�� ������ ��
//                        else
//                        {
//                            EndDialogue();
//                            dialogueStarted = false;
//                            afterInteraction = true;

//                        }
//                    }
//                }
//            }
//        }
//    }

//    public void ShowDialogue(ItemDialogue[] Parm_dialogues)
//    {
//        isDialogue = true; // ��ȭ�� ������ �� "��ȭ���̴�". �˸�
//        TalkText.text = "";
//        itemDialogues = Parm_dialogues;

//        StartCoroutine(TypeWriter());
//    }


//    void EndDialogue()
//    {
//        isDialogue = false;
//        contextCount = 0;
//        lineCount = 0;
//        itemDialogues = null;
//        isNext = false;

//        SettingUI(false); // ��ȭ UI ����
//    }

//    IEnumerator TypeWriter()
//    {
//        //��ȭ UIȰ��ȭ
//        SettingUI(true);

//        string t_ReplaceText = itemDialogues[lineCount].contexts[contextCount];
//        t_ReplaceText = t_ReplaceText.Replace("'", ",");

//        for (int i = 0; i < t_ReplaceText.Length; i++)
//        {
//            TalkText.text += t_ReplaceText[i];
//            yield return new WaitForSeconds(textDelay);
//        }

//        isNext = true;

//    }

//    void SettingUI(bool isAction)
//    {
//        talkPanel.SetActive(isAction);

//        // NamePanel ���⿡ �ֱ�.
//    }
//}
