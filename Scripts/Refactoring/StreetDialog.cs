// 상가 캐릭터 3명과 혜성의 대화를 위한 임시 코드 

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StreetDialog : MonoBehaviour
{
    Dialogue[] dialogues;
    bool isDialogue = false; // 대화중일 경우 true.
    bool isNext = false; // 특정 키 입력 대기.

    int lineCount = 0; // 사람 대화 카운트.
    int contextCount = 0; //대사 카운트

    public GameObject talkPanel;
    public Text TalkText;

    //public InteractionEvent interactionEvent;
    public ItemInteraction itemInteraction;

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;


    [Space]
    [Header("대화 상대 가져오기")]
    public GameObject KHS;
    public GameObject Partner1;
    public GameObject Partner2;
    public GameObject Partner3;

    public GameObject DialogueNameTag1;
    public GameObject DialogueNameTag2;
    public GameObject DialogueNameTag3;

    bool dialogueStarted = false; // 대화 시작 여부를 체크
    bool afterInteraction = false;



    void Start()
    {
        // 대화가 시작되지 않았음을 표시
        dialogueStarted = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !dialogueStarted) // 대화가 시작되지 않은 상태에서만 대화 시작
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


            if (hit.collider != null && hit.collider.CompareTag("NPC")) // "NPC" 태그로 바꾸세요
            {
                // ShowDialogue(interactionEvent.GetDialogue(afterInteraction));
                ShowDialogue(itemInteraction.GetItemDialogue(hit.collider.name));
                dialogueStarted = true; // 대화가 시작되었음을 표시

                CharactorSetting(hit.collider.name);
            }
        }

        DiaAction(); // 대화 진행도를 체크하는 부분도 Update()에서 처리합니다.
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

                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    else
                    {
                        // 대사 카운트 초기화
                        contextCount = 0;

                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }

                        // 모든 대화가 끝났을 때
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
        isDialogue = true; // 대화가 시직할 때 "대화중이다". 알림
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

        SettingUI(false); // 대화 UI 끄기
        CharactorOff(false);
    }

    IEnumerator TypeWriter()
    {
        //대화 UI활성화
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

        // NamePanel 여기에 넣기.

    }

    void CharactorSetting(string ActorName)
    {
        // Debug.Log(ActorName);

        if (ActorName == "영어학원")
        {
            Partner1.SetActive(isDialogue);
            DialogueNameTag1.SetActive(isDialogue);

        }

        if (ActorName == "카페")
        {
            Partner2.SetActive(isDialogue);
            DialogueNameTag2.SetActive(isDialogue);
        }

        if (ActorName == "꽃집")
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
