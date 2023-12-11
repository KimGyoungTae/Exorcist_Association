using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, ItemSaves
{
    public InventoryItem Item;

    public InventoryItem ClickItem()
    {
        return this.Item;
    }
}

public interface ItemSaves
{
    InventoryItem ClickItem();
}
