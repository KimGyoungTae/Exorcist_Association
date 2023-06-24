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
        talkData.Add(1000, new string[] {"난 죽이지 않았어.." , "너도 날 의심하고 있는거니?"});

        talkData.Add(100, new string[] { "칼에 피가 묻어있다.", "앞에 시체와 연관이 있는 걸까?", "일단 수집하고 인벤토리를 확인하자." });

        talkData.Add(200, new string[] { "부적 인가? 일단 수집하고,", "인벤토리를 확인해보자." });

        talkData.Add(300, new string[] { "피가묻은 못?", "단서가 될 수 있으니", "일단 수집하고 인벤토리를 확인하자." });





        //talkData.Add(2000, new string[] {"으스스한 기분" , "너무 기분 나쁜 걸..", "앞에 있는 표지판을 확인해보자."});
      

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
