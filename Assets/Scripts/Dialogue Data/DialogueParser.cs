using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��ȭ ��ũ��Ʈ�� CSV ���Ϸ� �����´�.
/// CSV ���� �����͸� �Ľ��Ѵ�.
/// </summary>
public class DialogueParser : MonoBehaviour
{
   
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // csv ���� ��������

        string[] data = csvData.text.Split(new char[] { '\n' }); // ���� ������ �ɰ���.

       
      
        for(int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); // �޸� ������ �ɰ�.

            Dialogue dialogue = new Dialogue(); // ��� ����Ʈ ����
            dialogue.name = row[1];

            // �����Ͱ� �� �Ľ̵ƴ��� Ȯ���ϱ� ����..
            // Debug.Log(row[1]); //�̸� �� �� ���

            List<string> contextList = new List<string>();
            do
            {
                contextList.Add(row[2]);
            //  Debug.Log(row[2]); // ���� �ݺ��ؼ� ���� ���� ��.

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

            dialogueList.Add(dialogue); // ĳ���� �̸�, ������ �� �������� dialogueList�� ����

        }
        
        return dialogueList.ToArray(); // ��ȯ�� �� �迭 ���·� ��ȯ
    
    }



    private void Start()
    {

        //Parse("Dialogprologue");    // ���Ժ� ��ȭ
        //Parse("MapDialog"); // �� �� + ���� �ʿ����� ��ȭ
        //Parse("Street Dialog"); // �� �ʿ� ���� ��ȭ
    }
}
