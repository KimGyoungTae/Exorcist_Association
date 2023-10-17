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

        talkData.Add(10, new string[] { "... ��, å�� ���� ���� ������...", "��... �簳��... ... ���Ǽ�... �����...?", "�� �������� �簳�߰� ���õ� ���� �־���?", "�����ؼ� �̾߱⸦ �� �˰� �ִ� ����� ���� �� ������ �����ٵ�..." });

        talkData.Add(20, new string[] { "��, ���ƴٴϸ鼭 ���� �� ���� ����Ʈ �о� �����!", "���� ���ð��� �а� ����Ʈ�� �ٲ�ٰ� �ϴ���...", "�׷����� �������� ���� ������ �־��ٰ� �ϴ���,", "������ �������ɱ�?" });


        talkData.Add(30, new string[] { "��, �̰� ���� ��������ٴ� ����Ʈ�� ��ġ��!", "������ �� ���� �� ��ó ���ð��� �㹰�� ���� ���ݾ�?", "������ ������ ���Ƽ� ��� ��� ��ŵ鵵 ���Ҵٴ� �޸� �ֳ�.", "... ... ��... ..." });


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
