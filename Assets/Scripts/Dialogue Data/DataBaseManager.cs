using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �Ľ̵� ��ȭ �����͸� �����ͺ��̽��� ���� �� �����Ѵ�.
/// </summary>
public class DataBaseManager : MonoBehaviour
{
  
    public static DataBaseManager instance;
    public string csv_FileName;

    // < �ε���, ������ >
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false; // ���� �� ���� �ƴ��� Ȯ��

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
