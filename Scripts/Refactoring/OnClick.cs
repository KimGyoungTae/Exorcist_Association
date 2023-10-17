// ��ȭ ��밡 3���� �� ��� & �� �찡 ���� �� ����/������ ���� ���濡 ���� �ڵ�

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{

    Dialogue[] dialogues;
    bool isDialogue = false; // ��ȭ���� ��� true.
    bool isNext = false; // Ư�� Ű �Է� ���.

    int lineCount = 0; // ��� ��ȭ ī��Ʈ.
    int contextCount = 0; //��� ī��Ʈ


    public GameObject talkPanel;
    public Text TalkText;


    // public InteractionEvent interactionEvent = new InteractionEvent();
    public InteractionEvent interactionEvent;


    // ������ ĳ���͵��� On���� / OFF ���� �̹����� ������� ������ �迭
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;

    public GameObject KHSObject; 
    public GameObject CJWObject;

    // (true: On, false: Off)
    private bool KHSStateOn = false;
    private bool CJWStateOn = true;

    // ���찡 ���ϰ� �� �� ���� ������ ���� ���� ���� ����
    private bool prevKHS;
    private bool prevCJW;
    private bool prevState = false;
    private int count = 0; // ù ��° & �� ��° �϶� ���� ���� 


    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;


    private ManageSceneTransition sceneTransition;

    bool afterInteraction = false;


    void Start()
    {
        ShowDialogue(interactionEvent.GetDialogue(afterInteraction));
        sceneTransition = GetComponent<ManageSceneTransition>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DiaAction();
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

                   if(++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    else
                    {

                        if (prevState && count % 2 == 1)
                        {
                            Debug.Log("���� ���� ");

                            KHSStateOn = prevKHS;
                            CJWStateOn = prevCJW;
                            ChangeCharactoreImage();

                            prevState = false;
                        }

                        // ��� ī��Ʈ �ʱ�ȭ
                        contextCount = 0;

                        // �� ĳ������ ��縦 ��� ��ĥ �� �̹��� ���� On / OFF ����
                        ChangeCharactoreImage();

                        //Debug.Log(KHSStateOn);
                        //Debug.Log(CJWStateOn);


                        if (++lineCount < dialogues.Length)
                        {
                           // Debug.Log(dialogues[lineCount].name);



                            CharactorOffImage();
                            StartCoroutine(TypeWriter());

                        }

                        // ��� ��ȭ�� ������ ��
                        else 
                        {
                            EndDialogue();

                            // ��� ��� ��� ���� �� ���� �������� �� �̵� 
                            sceneTransition.FadeScene(4);
                            //  sceneTransition.ChangeScene();

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
        isDialogue= false;
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

       // TalkText.text = t_ReplaceText;

        // TalkName.text = dialogues[linecount].name

       for(int i = 0; i< t_ReplaceText.Length; i++)
        {
           TalkText.text += t_ReplaceText[i];
           yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
       
    }
  
    void SettingUI(bool isAction)
    {
        talkPanel.SetActive(isAction);

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

    void CharactorOffImage()
    {
        // ���찡 ���� ��
        if (dialogues[lineCount].name == "�� ��")
        {
            Sprite KHSOffSprite = KHSOnOffSprites[1];
            Sprite CJWOffSprite = CJWOnOffSprites[1];

            KHSObject.GetComponent<SpriteRenderer>().sprite = KHSOffSprite;
            CJWObject.GetComponent<SpriteRenderer>().sprite = CJWOffSprite;

            // ���찡 ���ϱ� �� ������ ������ ���¸� �޾ƿ´�.
            prevKHS = KHSStateOn;
            prevCJW = CJWStateOn;
            count++;

            prevState = true;
        }

    }

}
