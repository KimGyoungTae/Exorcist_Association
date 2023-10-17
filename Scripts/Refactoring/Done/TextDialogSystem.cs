// 대화 시스템 로직 중 공통된 변수와 함수를 묶어서 정리하기 위해 추상 클래스 정의
// 각각의 조사할 맵들의 NPC들의 대화상태 On/OFF 이미지를 전달 받으면 추상 클래스 이용 하기.
// 조사할 맵의 NPC들의 상호작용 전/후인지 판단을 위한 함수 -> CheckDialogueState()

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public abstract class TextDialogSystem : MonoBehaviour
{
    [Space]
    [Header("대화 정보 가져오기")]
    public GameObject KHS;
    public GameObject Partner;

    public GameObject talkPanel;
    public Text TalkText;

    [Space]
    [Header("각 캐릭터들의 대화 상태")]
    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] PartnerOnOffSprites;

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;
    

    protected Dialogue[] dialogues;
    protected bool isDialogue = false; // 대화중일 경우 true.
    protected bool isNext = false; // 특정 키 입력 대기.

    protected int lineCount = 0; // 사람 대화 카운트.
    protected int contextCount = 0; //대사 카운트

    // (true: On, false: Off)
    // 캐릭터들의 상태
    protected bool KHSStateOn = true;
    protected bool PartnerStateOn = false;

    //// 대화 상태가 상호작용 전/후 인지 판단함.
    //protected InteractionEvent interactionEvent;
    //protected bool dialogueStarted = false; // 대화 시작 여부를 체크
    //protected bool afterInteraction = false;


    
    
    //public virtual void CheckDialogueState()
    //{
    //    if (Input.GetMouseButtonDown(0) && !dialogueStarted) // 대화가 시작되지 않은 상태에서만 대화 시작
    //    {
    //        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


    //        if (hit.collider != null && hit.collider.CompareTag("NPC")) // "NPC" 태그로 바꾸세요
    //        {
    //            ShowDialogue(interactionEvent.GetDialogue(afterInteraction));
    //            dialogueStarted = true; // 대화가 시작되었음을 표시

    //        }
    //    }
    //}

    public virtual void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        isDialogue = true; // 대화가 시직할 때 "대화중이다". 알림
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

        SettingUI(false); // 대화 UI 끄기
    }

    public virtual IEnumerator TypeWriter()
    {
        // 대화 UI 활성화
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");


        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            if (t_ReplaceText[i] == '|')
            {
                Debug.Log("2번째 줄 이어가기..");
                TalkText.text += "\n";

                // "\n"을 기준으로 문자열을 나눕니다.
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
        // NamePanel 여기에 넣기.
    }


    public virtual void ChangeCharactoreImage()
    {
        // 강햬성의 상태를 변경합니다.
        KHSStateOn = !KHSStateOn;
        // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOn ? 0 : 1];
        // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;


        // 차지원의 상태를 변경합니다.
        PartnerStateOn = !PartnerStateOn;
        Sprite CJWSprite = PartnerOnOffSprites[PartnerStateOn ? 0 : 1];
        Partner.GetComponent<SpriteRenderer>().sprite = CJWSprite;

    }



    //// - Not Working - 
    //// 공통 동작을 묶을 수 있는 다른 메소드들을 정의합니다.
    //// 캐릭터 이미지를 On/Off 하는 로직을 구현합니다.
    //protected void DisplayCharacterImages(bool showCharacter1, bool showCharacter2)
    //{
 
    //    // 강햬성의 상태를 변경합니다.
    //    showCharacter1 = !showCharacter1;
    //    // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
    //    Sprite KHSSprite = KHSOnOffSprites[showCharacter1 ? 0 : 1];
    //    // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
    //    KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;


    //    // 차지원의 상태를 변경합니다.
    //    showCharacter2 = !showCharacter2;
    //    Sprite PartnerSprite = PartnerOnOffSprites[showCharacter2 ? 0 : 1];
    //    Partner.GetComponent<SpriteRenderer>().sprite = PartnerSprite;
    //}
}
