using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// JSON ���Ϸ� ����� �� ���� ������ �����͸� ��������, �����Ѵ�.
/// </summary>
public class TextTapDataManager : MonoBehaviour
{
    public TextAsset data;
    private AllData datas;

    private void Awake()
    {
       datas = JsonUtility.FromJson<AllData>(data.text);

        // ������ �� ���Դ��� Ȯ��
        //foreach(var Variable in datas.FWOH)
        //{
        //    print(Variable.TabType);
        //    print(Variable.Answer1);
          
        //}

    }

    public string[] GetAnswersForTab(TabType tabType)
    {
        string[] answers = new string[9]; // Answer1���� Answer7������ �迭

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

        return answers; // �⺻ ��ȯ ���
    }

}

[System.Serializable]
public class AllData
{
    public TapData[] FWOH;
}

[System.Serializable]
// ���ڷ� �Ǿ��ִ� Json ������ �����ͷ� ����
public class TapData
{
    // Json�� ���� ���� �̸��� ��/�ҹ��ڰ� ��ġ�ؾ���. �׷��� ������ null �����Ͱ� ������ �ȴ�.
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
