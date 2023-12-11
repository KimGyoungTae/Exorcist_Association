using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 엑셀 대화 스크립트를 CSV 파일로 가져온다.
/// CSV 파일 데이터를 파싱한다.
/// </summary>
public class DialogueParser : MonoBehaviour
{
   
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // csv 파일 가져오기

        string[] data = csvData.text.Split(new char[] { '\n' }); // 엔터 단위로 쪼갠다.

       
      
        for(int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); // 콤마 단위로 쪼갬.

            Dialogue dialogue = new Dialogue(); // 대사 리스트 생성
            dialogue.name = row[1];

            // 데이터가 잘 파싱됐는지 확인하기 위한..
            // Debug.Log(row[1]); //이름 한 줄 출력

            List<string> contextList = new List<string>();
            do
            {
                contextList.Add(row[2]);
            //  Debug.Log(row[2]); // 대사는 반복해서 여러 줄이 뜸.

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while (row[0].ToString() == "");


            dialogue.contexts = contextList.ToArray();

            dialogueList.Add(dialogue); // 캐릭터 이름, 대사들을 한 묶음으로 dialogueList에 저장

        }
        
        return dialogueList.ToArray(); // 반환할 때 배열 형태로 반환
    
    }



    private void Start()
    {

        //Parse("Dialogprologue");    // 도입부 대화
        //Parse("MapDialog"); // 각 맵 + 메인 맵에서의 대화
        //Parse("Street Dialog"); // 상가 맵에 따른 대화
    }
}
