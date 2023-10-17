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

            itemDialogue.place = row[1];  // 아이템 장소
            itemDialogue.itemName = row[2]; // 아이템 이름

            
            // 대사는 여러 번 반복 될 수 있으니..
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
            } while (row[0].ToString() == ""); //ID가 비어있는 동안 반복 진행..

            itemDialogue.contexts = contextList.ToArray();

            itemList.Add(itemDialogue); // 장소, 아이템 이름, 대사들 한 묶음으로 itemList에 저장

        }

        return itemList.ToArray(); // 반환은 배열 형태로 반환..
    }

    private void Start()
    {
        Parse("ItemDia");
    }
}
