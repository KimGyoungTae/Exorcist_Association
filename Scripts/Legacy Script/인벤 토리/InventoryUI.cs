using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;

    // �κ��丮 �г� ���� �� �г��� Ȱ�� / ��Ȱ�� ����
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

        //�κ��丮 �������� ����� �� ���� UI�� ������Ʈ �ϴ� �̺�Ʈ ������
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


    // ���� UI�� �ٽ� �׸��� �޼ҵ�
    // ���� ������ �����, �κ��丮�� �ִ� �����۵鿡 ���� ���� ������Ʈ ����
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
