using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    

    void Start()
    {
        talkData = new Dictionary<int, string[]>(); //초기화
        GenerateData();
    }


    void GenerateData()
    {
        //Add 함수를 사용하여 대화 데이터 입력 추가
        talkData.Add(100, new string[] {"지금은 사용하지 않아도 될 것 같아."});
    }


    //지정된 대화 문장을 반환하는 함수 
    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)  //남아있는 문장이 있는지
        {
            return null;
        }

       else return talkData[id][talkIndex];
    }
}
