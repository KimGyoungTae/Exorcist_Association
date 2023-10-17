using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
   
    public List<Item> items = new List<Item>();
    public static Inventory instance;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

   // private int target = 6; // ��ǥ ��
   //private int correct = 0; // ���� ����

    public InventoryUI ui;

    private void Awake() {

        

        if (instance == null)
        {
            instance = this;
        }
        
        ui.FreshSlot();
    }


    public void AddItem(Item _item, GameObject obj)
    {

        Debug.Log("������ ��� ��...");
        items.Add(_item);
       // ui.FreshSlot();

        if (onChangeItem != null)
        {
            onChangeItem.Invoke();
        }
    }



    //public void PrintItem(Item _item)
    //{
    //    // ��� �����̳� / ���� �����̳� ��ġ �������� �ʿ��� �ܼ� ���� �ùٸ��� ���
    //    int correct = 0; // ���� ����


    //    Debug.Log("���� " + _item.itemName1 + " " + item1.Count + "�� �Դϴ�.");
    //    Debug.Log("���� " + _item.itemName2 + " " + item2.Count + "�� �Դϴ�.");
    //    Debug.Log("���� " + _item.itemName3 + " " + item3.Count + "�� �Դϴ�.");

    //    correct += (item1.Count + item2.Count + item3.Count);
    //    Debug.Log("�ʿ��� �ܼ��� ������ " + (target - correct) + "���Դϴ�.");

    //}


}
