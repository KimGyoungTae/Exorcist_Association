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

    public void Action(GameObject getObjcet)
    {
        
        //talkPanel.SetActive(true);
        scanObject = getObjcet;

        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNPC);
           
       talkPanel.SetActive(isAction);   // isAction == false , 조사 Ul 창 닫겠다.
    }

    void Talk(int id, bool isNPC)
    {
       string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null )
        {
            isAction = false;
            talkIndex = 0; //대화가 끝날 때 0으로 초기화 , 다른 사물하고도 계속 대화를 진행 하기 위함.
            return;  // 끝 , void 함수에서 return은 강제 종료 역할.
        }
    
       if (isNPC)
        {
            TalkText.text = talkData;
        } 

       else
        {
            TalkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }

}
