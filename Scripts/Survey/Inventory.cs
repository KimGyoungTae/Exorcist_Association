using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    //public List<Item> items;
    //public List<Item, GameObject> items1 = new List<Item, GameObject>();

    public List<GameObject> item1 = new List<GameObject>();
    public List<GameObject> item2 = new List<GameObject>();
    public List<GameObject> item3 = new List<GameObject>();


    //private Dictionary<Item, int> itemQuantities = new Dictionary<Item, int>();

 
    private int target = 6; // ��ǥ ��
    //private int correct = 0; // ���� ����
  
    public void AddItem(Item _item, GameObject obj)
    {
        switch (obj.name)
        {
            case "Knife" :
                item1.Add(obj);
                break;

            case "���":
                item2.Add(obj);
                break;

            case "����":
                item3.Add(obj);
                break;
        }

    }

    public void PrintItem(Item _item)
    {
        // ��� �����̳� / ���� �����̳� ��ġ �������� �ʿ��� �ܼ� ���� �ùٸ��� ���
        int correct = 0; // ���� ����


        Debug.Log("���� " + _item.itemName1 + " " + item1.Count + "�� �Դϴ�.");
        Debug.Log("���� " + _item.itemName2 + " " + item2.Count + "�� �Դϴ�.");
        Debug.Log("���� " + _item.itemName3 + " " + item3.Count + "�� �Դϴ�.");

        correct += (item1.Count + item2.Count + item3.Count);
        Debug.Log("�ʿ��� �ܼ��� ������ " + (target - correct) + "���Դϴ�.");

    }
}
