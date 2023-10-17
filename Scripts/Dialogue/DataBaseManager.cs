using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 역할 : 파싱한 데이터를 데이터베이스에서 저장 및 관리

public class DataBaseManager : MonoBehaviour
{
  
    public static DataBaseManager instance;

    [SerializeField] string csv_FileName;

    //[SerializeField] string item_FileName;

    // < 인덱스, 데이터 >
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    //Dictionary<int, ItemDialogue> itemDiaDic = new Dictionary<int, ItemDialogue>();

    public static bool isFinish = false; // 전부 다 저장 됐나?

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


            //if (item_FileName != null)
            //{
            //    ItemParser itemParser = GetComponent<ItemParser>();
            //    ItemDialogue[] itemDialogues = itemParser.Parse(item_FileName);

            //    for (int j = 0; j < itemDialogues.Length; j++)
            //    {
            //        itemDiaDic.Add(j + 1, itemDialogues[j]);
            //    }


            //}


            //else { return; }

            isFinish = true;

        }

    }

    public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for(int i = 0; i<= _EndNum - _StartNum; i++)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialogueList.ToArray();

    }

    //public ItemDialogue[] GetItemDialogue(int _StartNum, int _EndNum)
    //{
    //    List<ItemDialogue> itemDialogues = new List<ItemDialogue>();

    //    for (int i = 0; i <= _EndNum - _StartNum; i++)
    //    {
    //        itemDialogues.Add(itemDiaDic[_StartNum + i]);
    //    }

    //    return itemDialogues.ToArray();

    //}
}
