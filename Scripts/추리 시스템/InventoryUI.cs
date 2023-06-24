using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

   // public List<Item> items;

    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();


        inven.onChangeItem += RedrawSlotUI;


        inventoryPanel.SetActive(activeInventory);
    }

    private void Update()
    {
       
    }

    public void bringInventory()
    {
        activeInventory = !activeInventory;
        inventoryPanel.SetActive(activeInventory);
    }


    public void FreshSlot()
    {
        int i = 0;
        //for (; i < items.Count && i < slots.Length; i++)
        //{
        //    Debug.Log("¾ÆÀÌÅÛ -> ½½·Ô...");
        //    slots[i].item = items[i];

        //}

        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }



    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
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
