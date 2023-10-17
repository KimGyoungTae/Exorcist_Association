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

   // private int target = 6; // 목표 수
   //private int correct = 0; // 수집 개수

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

        Debug.Log("아이템 담는 중...");
        items.Add(_item);
       // ui.FreshSlot();

        if (onChangeItem != null)
        {
            onChangeItem.Invoke();
        }
    }



    //public void PrintItem(Item _item)
    //{
    //    // 멤버 변수이냐 / 지역 변수이냐 위치 수정으로 필요한 단서 개수 올바르게 출력
    //    int correct = 0; // 수집 개수


    //    Debug.Log("현재 " + _item.itemName1 + " " + item1.Count + "개 입니다.");
    //    Debug.Log("현재 " + _item.itemName2 + " " + item2.Count + "개 입니다.");
    //    Debug.Log("현재 " + _item.itemName3 + " " + item3.Count + "개 입니다.");

    //    correct += (item1.Count + item2.Count + item3.Count);
    //    Debug.Log("필요한 단서의 개수는 " + (target - correct) + "개입니다.");

    //}


}
