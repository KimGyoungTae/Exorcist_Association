using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventoryController : MonoBehaviour
{
   
    // ��ư Ŭ������ �κ��丮 Ȱ��ȭ/��Ȱ��ȭ 
    // �κ��丮 ������ ���� 
    public void GetInventory()
    {
        InventoryManager.Instance.ShowInventory();
        InventoryManager.Instance.ListItems();
    }

    public void GetInventoryInfo()
    {
        InventoryManager.Instance.SetInventoryInfo();
    }

    public void ButtonText()
    {
        Debug.Log("��ư Ŭ�� ��");
    }
}
