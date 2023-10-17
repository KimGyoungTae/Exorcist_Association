//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ClickItemInteraction : MonoBehaviour
//{
//    ItemDialogue[] itemDialogues;

//    bool isDialogue = false; // 대화중일 경우 true.
//    bool isNext = false; // 특정 키 입력 대기.

//    int lineCount = 0; // 사람 대화 카운트.
//    int contextCount = 0; //대사 카운트

//    public GameObject talkPanel;
//    public Text TalkText;

//    public ItemInteraction itemInteraction;

//    [Header("텍스트 출력 딜레이")]
//    [SerializeField] float textDelay;

//    bool dialogueStarted = false; // 대화 시작 여부를 체크
//    bool afterInteraction = false;

//    void Start()
//    {
//        // 대화가 시작되지 않았음을 표시
//        dialogueStarted = false;
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && !dialogueStarted) // 대화가 시작되지 않은 상태에서만 대화 시작
//        {
//            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);


//            if (hit.collider != null && hit.collider.CompareTag("Item"))
//            {
//               // Debug.Log(hit.collider.gameObject.name);

//                GameObject clickedObject = hit.collider.gameObject;
//                ShowDialogue(itemInteraction.GetItemDialogue(afterInteraction, clickedObject));
//                dialogueStarted = true; // 대화가 시작되었음을 표시
//            }
//        }

//        DiaAction(); // 대화 진행도를 체크하는 부분도 Update()에서 처리합니다.
//    }

//    void DiaAction()
//    {
//        if (isDialogue) // 대화중 이면서
//        {
//            if (isNext) // 다음 키 입력이 가능할 때
//            {
//                if (Input.GetMouseButtonDown(0)) // 마우스 버튼을 누를 때
//                {

//                    isNext = false;
//                    TalkText.text = "";

//                    if (++contextCount < itemDialogues[lineCount].contexts.Length)
//                    {
//                        StartCoroutine(TypeWriter());
//                    }

//                    else
//                    {
//                        // 대사 카운트 초기화
//                        contextCount = 0;

//                        if (++lineCount < itemDialogues.Length)
//                        {
//                            StartCoroutine(TypeWriter());
//                        }

//                        // 모든 대화가 끝났을 때
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
//        isDialogue = true; // 대화가 시직할 때 "대화중이다". 알림
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

//        SettingUI(false); // 대화 UI 끄기
//    }

//    IEnumerator TypeWriter()
//    {
//        //대화 UI활성화
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

//        // NamePanel 여기에 넣기.
//    }
//}
