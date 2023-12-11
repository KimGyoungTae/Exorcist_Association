using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 대화 시스템 로직 중 공통된 변수와 함수 등을 묶어서 정리하기 위해 추상 클래스 정의
/// </summary>
public abstract class TextDialogSystem : MonoBehaviour
{

    [Header("대화 출력 UI 가져오기")]
    [SerializeField] protected GameObject talkPanel;
    [SerializeField] protected Text TalkText;
    [SerializeField] protected Button SkipButton;

    [Header("대화 이름 UI 가져오기")]
    [SerializeField] protected GameObject namePanel;
    [SerializeField] protected Text nameText;

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

  
    protected Dialogue[] dialogues;
    protected bool isDialogue = false; // 대화중일 경우 true.
    protected bool isNext = false; // 특정 키 입력 대기.

    protected int lineCount = 0; // 사람 대화 카운트.
    protected int contextCount = 0; //대사 카운트


    // 대화 상태가 상호작용 전/후 인지 판단함.
    [SerializeField] protected DialogueInteraction interaction;
    protected bool dialogueStarted = false; // 대화 시작 여부를 체크
    protected bool startInteraction = false;
    [SerializeField] protected string currentFileName;

    protected RaycastHit2D hit;

    public virtual void CheckDialogueState()
    {
        // 대화가 시작되지 않은 상태에서만 대화 시작
        if (Input.GetMouseButtonDown(0) && !dialogueStarted) 
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


            if (hit.collider != null)
            {
                
                currentFileName = DataBaseManager.instance.csv_FileName;
                ShowDialogue(interaction.GetDialogueContents(hit.collider.tag, hit.collider.name, currentFileName));

                ChangeCharacterUI(hit.collider.tag, hit.collider.name);

                dialogueStarted = true; // 대화가 시작되었음을 표시
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
    /// 각 맵에서 보여지는 캐릭터, 조사할 아이템 등이 다르므로 
    /// 각각의 스크립트에 알맞게 구현을 위해 추상메소드로 정의 함.
    /// Tag or Name에 따라 UI에 어떻게 보여줄 것인지 판단한다.
    /// </summary>
    /// <param name="tag">상호작용 할 태그 이름</param>
    /// <param name="name">상호작용 오브젝트 이름</param>
    public abstract void ChangeCharacterUI(string tag, string name);


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

    public virtual void ShowDialogueName()
    {
        nameText.text = dialogues[lineCount].name;
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
              //  Debug.Log("2번째 줄 이어가기..");
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
      
        // NamePanel 여기에 넣기.
        namePanel.SetActive(isAction);
    }

}
