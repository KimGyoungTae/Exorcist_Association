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

        talkData.Add(10, new string[] { "... 어, 책상에 뭔가 많은 서류가...", "음... 재개발... ... 동의서... 보상금...?", "이 지역에서 재개발과 관련된 일이 있었나?", "관련해서 이야기를 잘 알고 있는 사람을 만날 수 있으면 좋을텐데..." });

        talkData.Add(20, new string[] { "앗, 돌아다니면서 자주 본 신축 아파트 분양 광고다!", "옛날 주택가를 밀고 아파트로 바꿨다고 하던데...", "그러고보니 생각보다 많은 차질이 있었다고 하던데,", "지금은 괜찮은걸까?" });


        talkData.Add(30, new string[] { "아, 이건 새로 만들어진다는 아파트의 위치네!", "지도를 잘 보니 이 근처 주택가를 허물고 짓는 거잖아?", "오래된 주택이 많아서 계속 살던 어르신들도 많았다는 메모가 있네.", "... ... 음... ..." });


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
