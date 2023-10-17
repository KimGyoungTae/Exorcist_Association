using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, ItemSaves
{
   // public static ItemController instance;
    public InventoryItem Item;


    //private void Awake()
    //{
    //    instance = this;
    //}

    //public void Pickup()
    //{
    //    InventoryManager.Instance.Add(Item);

    //}

    public InventoryItem ClickItem()
    {
        return this.Item;
    }
}

public interface ItemSaves
{
    InventoryItem ClickItem();
}
