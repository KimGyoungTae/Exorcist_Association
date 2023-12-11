using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// JSON 파일로 저장된 각 탭의 선택지 데이터를 가져오고, 관리한다.
/// </summary>
public class TextTapDataManager : MonoBehaviour
{
    public TextAsset data;
    private AllData datas;

    private void Awake()
    {
       datas = JsonUtility.FromJson<AllData>(data.text);

        // 정보가 잘 들어왔는지 확인
        //foreach(var Variable in datas.FWOH)
        //{
        //    print(Variable.TabType);
        //    print(Variable.Answer1);
          
        //}

    }

    public string[] GetAnswersForTab(TabType tabType)
    {
        string[] answers = new string[9]; // Answer1부터 Answer7까지의 배열

        foreach (var data in datas.FWOH)
        {
            if (data.TabType == tabType.ToString())
            {
                answers[0] = data.Answer1;
                answers[1] = data.Answer2;
                answers[2] = data.Answer3;
                answers[3] = data.Answer4;
                answers[4] = data.Answer5;
                answers[5] = data.Answer6;
                answers[6] = data.Answer7;
                answers[7] = data.Answer8;
                answers[8] = data.Answer9;
                return answers;
            }
        }

        return answers; // 기본 반환 경로
    }

}

[System.Serializable]
public class AllData
{
    public TapData[] FWOH;
}

[System.Serializable]
// 문자로 되어있는 Json 파일을 데이터로 변경
public class TapData
{
    // Json과 변수 선언 이름의 대/소문자가 일치해야함. 그렇지 않으면 null 데이터가 찍히게 된다.
    public string TabType;
    public string Answer1;
    public string Answer2;
    public string Answer3;
    public string Answer4;
    public string Answer5;
    public string Answer6;
    public string Answer7;
    public string Answer8;
    public string Answer9;

}
