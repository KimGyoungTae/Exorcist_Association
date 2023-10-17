using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;

    // 인벤토리 패널 변수 및 패널의 활성 / 비활성 변수
    public GameObject inventoryPanel;
    bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

    // public List<Item> items;


    public static InventoryUI instance;




    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();

        //인벤토리 아이템이 변경될 때 슬롯 UI를 업데이트 하는 이벤트 리스너
        inven.onChangeItem += RedrawSlotUI;


        inventoryPanel.SetActive(activeInventory);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void bringInventory()
    {
        activeInventory = !activeInventory;
        inventoryPanel.SetActive(activeInventory);
    }


    public void FreshSlot()
    {
        int i = 0;
        
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }


    // 슬롯 UI를 다시 그리는 메소드
    // 기존 슬롯을 지우고, 인벤토리에 있는 아이템들에 대해 슬롯 업데이트 진행
    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++  )
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i< inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
