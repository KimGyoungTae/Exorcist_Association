using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text TalkText;
    public GameObject scanObject;
    public bool isAction; 

    public int talkIndex;

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    private bool isDisplayingText = false; // 대화 출력 중인지 여부를 나타내는 변수
    private Coroutine currentCoroutine; // 현재 실행 중인 코루틴을 추적하는 변수

    public void Action(GameObject getObjcet)
    {
        scanObject = getObjcet;
        
      // Debug.Log(isDisplayingText);
       
        PointObject pointObject = scanObject.GetComponent<PointObject>();

        //Debug.Log(objData.id);
        Talk(pointObject.id, pointObject.isNPC);

        talkPanel.SetActive(isAction);   // isAction == false , 조사 Ul 창 닫겠다.
    }


    //// 클릭 이벤트를 관리하는 함수
    //public void HandleClick(GameObject clickedObject)
    //{



    //    if (!isDisplayingText)
    //    {
    //        isDisplayingText = true;
    //        // StartCoroutine(ProcessClick(clickedObject));

    //        //if (currentCoroutine != null)
    //        //{
    //        //    StopCoroutine(currentCoroutine); // 이미 실행 중인 코루틴을 중지
    //        //}
    //        currentCoroutine = StartCoroutine(ProcessClick(clickedObject));

    //    }
    //}

    //// 클릭된 오브젝트의 정보를 처리하고 대화 출력을 시작하는 함수
    //IEnumerator ProcessClick(GameObject clickedObject)
    //{
    //    scanObject = clickedObject;

    //   // Debug.Log(scanObject.name);  -> 여기가 문제!

    //    ObjData objData = scanObject.GetComponent<ObjData>();
    //    Talk(objData.id, objData.isNPC);
    //    talkPanel.SetActive(isAction);

    //    while (isDisplayingText)
    //    {
    //        yield return null; // 대화가 끝날 때까지 대기
    //    }


    //    // 코루틴 종료
    //    currentCoroutine = null;
    //    yield break;

    //}


    void Talk(int id, bool isNPC)
    {
      //  Debug.Log(id); // ->scanObject는 가져와지지만, id가 이번엔 안가져와짐..

       string talkData = talkManager.GetTalk(id, talkIndex);

      
        if (talkData == null )
        {
            isAction = false;
            talkIndex = 0; //대화가 끝날 때 0으로 초기화 , 다른 사물하고도 계속 대화를 진행 하기 위함.

            // 대화가 끝난 후 scanObject 초기화
            scanObject = null;


            //StopCoroutine(currentCoroutine); // 이미 실행 중인 코루틴을 중지

            return;  // 끝 , void 함수에서 return은 강제 종료 역할.
        }

        else
        {
            if (!isDisplayingText)
            {
             //   Debug.Log("대화 시작");
               
             // 코루틴을 사용하여 텍스트를 딜레이와 함께 한 단어씩 보여줍니다.
                StartCoroutine(DisplayText(talkData));
                isDisplayingText = true;
                isAction = true;
                talkIndex++;
            }
           
        //  StartCoroutine(DisplayText(talkData));
            //isAction = true;
            //talkIndex++;
        }
           
        

    }

    IEnumerator DisplayText(string text)
    {
        TalkText.text = ""; // 텍스트를 보여주기 전에 먼저 초기화합니다.

        //foreach (char character in text)
        //{
        //    TalkText.text += character;
        //    yield return new WaitForSeconds(textDelay);
        //    // 다음 글자를 보여주기 전에 지정한 딜레이만큼 대기합니다.       
        //}

        for (int i = 0; i < text.Length; i++)
        {
            
            TalkText.text += text[i];
            yield return new WaitForSeconds(textDelay);
        }

       // Debug.Log("대화 끝남");
        isDisplayingText = false; // 대화가 끝나면 변수를 변경하여 다음 클릭을 허용
    }
}
