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
        talkData.Add(1000, new string[] {"�� ������ �ʾҾ�.." , "�ʵ� �� �ǽ��ϰ� �ִ°Ŵ�?"});

        talkData.Add(100, new string[] { "Į�� �ǰ� �����ִ�.", "�տ� ��ü�� ������ �ִ� �ɱ�?", "�ϴ� �����ϰ� �κ��丮�� Ȯ������." });

        talkData.Add(200, new string[] { "���� �ΰ�? �ϴ� �����ϰ�,", "�κ��丮�� Ȯ���غ���." });

        talkData.Add(300, new string[] { "�ǰ����� ��?", "�ܼ��� �� �� ������", "�ϴ� �����ϰ� �κ��丮�� Ȯ������." });





        //talkData.Add(2000, new string[] {"�������� ���" , "�ʹ� ��� ���� ��..", "�տ� �ִ� ǥ������ Ȯ���غ���."});
      

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
