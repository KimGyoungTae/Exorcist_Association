using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� : �Ľ��� �����͸� �����ͺ��̽����� ���� �� ����

public class DataBaseManager : MonoBehaviour
{
  
    public static DataBaseManager instance;

    [SerializeField] string csv_FileName;

    //[SerializeField] string item_FileName;

    // < �ε���, ������ >
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    //Dictionary<int, ItemDialogue> itemDiaDic = new Dictionary<int, ItemDialogue>();

    public static bool isFinish = false; // ���� �� ���� �Ƴ�?

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);

            // ��� �����Ͱ� ���ٸ� ~
            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]); // ��� ��ȭ ������ ��ųʸ��� ����
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
