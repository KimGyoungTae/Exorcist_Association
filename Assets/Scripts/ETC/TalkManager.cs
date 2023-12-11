using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    

    void Start()
    {
        talkData = new Dictionary<int, string[]>(); //�ʱ�ȭ
        GenerateData();
    }


    void GenerateData()
    {
        //Add �Լ��� ����Ͽ� ��ȭ ������ �Է� �߰�
        talkData.Add(100, new string[] {"������ ������� �ʾƵ� �� �� ����."});
    }


    //������ ��ȭ ������ ��ȯ�ϴ� �Լ� 
    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)  //�����ִ� ������ �ִ���
        {
            return null;
        }

       else return talkData[id][talkIndex];
    }
}
