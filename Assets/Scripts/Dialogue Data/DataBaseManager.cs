using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 파싱된 대화 데이터를 데이터베이스에 저장 및 관리한다.
/// </summary>
public class DataBaseManager : MonoBehaviour
{
  
    public static DataBaseManager instance;
    public string csv_FileName;

    // < 인덱스, 데이터 >
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false; // 전부 다 저장 됐는지 확인

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);

            // 모든 데이터가 담겼다면 ~
            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]); // 모든 대화 데이터 딕셔너리에 저장
            }

            isFinish = true;

        }

    }

    public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for(int i = 0; i <= _EndNum - _StartNum; i++)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialogueList.ToArray();

    }
}
