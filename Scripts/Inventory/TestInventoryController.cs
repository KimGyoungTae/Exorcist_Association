using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventoryController : MonoBehaviour
{
   
    // 버튼 클릭으로 인벤토리 활성화/비활성화 
    // 인벤토리 아이템 갱신 
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
        Debug.Log("버튼 클릭 됨");
    }
}
