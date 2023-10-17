// 대화 상대가 3명일 때 경우 & 현 우가 말할 때 혜성/지원의 상태 변경에 대한 코드

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{

    Dialogue[] dialogues;
    bool isDialogue = false; // 대화중일 경우 true.
    bool isNext = false; // 특정 키 입력 대기.

    int lineCount = 0; // 사람 대화 카운트.
    int contextCount = 0; //대사 카운트


    public GameObject talkPanel;
    public Text TalkText;


    // public InteractionEvent interactionEvent = new InteractionEvent();
    public InteractionEvent interactionEvent;


    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;

    public GameObject KHSObject; 
    public GameObject CJWObject;

    // (true: On, false: Off)
    private bool KHSStateOn = false;
    private bool CJWStateOn = true;

    // 현우가 말하고 난 뒤 상태 변경을 막기 위한 변수 선언
    private bool prevKHS;
    private bool prevCJW;
    private bool prevState = false;
    private int count = 0; // 첫 번째 & 세 번째 일때 상태 유지 


    [Header("텍스트 출력 딜레이")]
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
        if (isDialogue) // 대화중 이면서
        {
            if (isNext) // 다음 키 입력이 가능할 때
            {
                if (Input.GetMouseButtonDown(0)) // 마우스 버튼을 누를 때
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
                            Debug.Log("상태 유지 ");

                            KHSStateOn = prevKHS;
                            CJWStateOn = prevCJW;
                            ChangeCharactoreImage();

                            prevState = false;
                        }

                        // 대사 카운트 초기화
                        contextCount = 0;

                        // 각 캐릭터의 대사를 모두 마칠 때 이미지 상태 On / OFF 변경
                        ChangeCharactoreImage();

                        //Debug.Log(KHSStateOn);
                        //Debug.Log(CJWStateOn);


                        if (++lineCount < dialogues.Length)
                        {
                           // Debug.Log(dialogues[lineCount].name);



                            CharactorOffImage();
                            StartCoroutine(TypeWriter());

                        }

                        // 모든 대화가 끝났을 때
                        else 
                        {
                            EndDialogue();

                            // 모든 대사 출력 끝날 시 지도 페이지로 씬 이동 
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
        isDialogue = true; // 대화가 시직할 때 "대화중이다". 알림
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

        SettingUI(false); // 대화 UI 끄기
    }
 
    IEnumerator TypeWriter()
    {
        //대화 UI활성화
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

        // NamePanel 여기에 넣기.
    }


   
    public void ChangeCharactoreImage()
    {
        // 강햬성의 상태를 변경합니다.
        KHSStateOn = !KHSStateOn;
        // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOn ? 0 : 1];
        // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
        KHSObject.GetComponent<SpriteRenderer>().sprite = KHSSprite;


        // 차지원의 상태를 변경합니다.
        CJWStateOn = !CJWStateOn;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOn ? 0 : 1];
        CJWObject.GetComponent<SpriteRenderer>().sprite = CJWSprite;

    }

    void CharactorOffImage()
    {
        // 현우가 말할 때
        if (dialogues[lineCount].name == "현 우")
        {
            Sprite KHSOffSprite = KHSOnOffSprites[1];
            Sprite CJWOffSprite = CJWOnOffSprites[1];

            KHSObject.GetComponent<SpriteRenderer>().sprite = KHSOffSprite;
            CJWObject.GetComponent<SpriteRenderer>().sprite = CJWOffSprite;

            // 현우가 말하기 전 혜성과 지원의 상태를 받아온다.
            prevKHS = KHSStateOn;
            prevCJW = CJWStateOn;
            count++;

            prevState = true;
        }

    }

}
