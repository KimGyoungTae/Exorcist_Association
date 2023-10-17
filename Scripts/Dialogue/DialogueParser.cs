using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
   
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // csv 파일 가져오기

        string[] data = csvData.text.Split(new char[] { '\n' }); // 엔터 단위로 쪼갠다.
        

        for(int i = 1; i < data.Length; )
        {
            string[] row = data[i].Split(new char[] { ',' }); // 콤마 단위로 쪼갬.

            Dialogue dialogue = new Dialogue(); // 대사 리스트 생성
            dialogue.name = row[1];

           // Debug.Log(row[1]); //이름 한 줄 출력

            List<string> contextList = new List<string>();
            do
            {
                contextList.Add(row[2]);
            //   Debug.Log(row[2]); // 대사는 반복해서 여러 줄이 뜸.


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
        //  Parse("Estateprologue");

        //Parse("Dialogprologue");    // 도입부 대화
        //Parse("MapDialog"); // 각 맵 + 메인 맵에서의 대화
        //Parse("Street Dialog"); // 상가 맵에 따른 대화 

        // Parse("SampleTwoLine");
        Parse("SampleTwoLine3");

    }
}
