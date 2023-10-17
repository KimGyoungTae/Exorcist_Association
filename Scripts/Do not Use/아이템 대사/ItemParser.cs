using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemParser : MonoBehaviour
{
    public ItemDialogue[] Parse(string _ItemCSVName)
    {
        List<ItemDialogue> itemList = new List<ItemDialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_ItemCSVName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for(int i = 1; i < data.Length; )
        {
            string[] row = data[i].Split(new char[] { ',' });

            ItemDialogue itemDialogue = new ItemDialogue();

            itemDialogue.place = row[1];  // ������ ���
            itemDialogue.itemName = row[2]; // ������ �̸�

            
            // ���� ���� �� �ݺ� �� �� ������..
            List<string> contextList = new List<string>();
            do
            {
                contextList.Add(row[3]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == ""); //ID�� ����ִ� ���� �ݺ� ����..

            itemDialogue.contexts = contextList.ToArray();

            itemList.Add(itemDialogue); // ���, ������ �̸�, ���� �� �������� itemList�� ����

        }

        return itemList.ToArray(); // ��ȯ�� �迭 ���·� ��ȯ..
    }

    private void Start()
    {
        Parse("ItemDia");
    }
}
