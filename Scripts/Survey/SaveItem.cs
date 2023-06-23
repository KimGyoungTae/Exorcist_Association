using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveItem : MonoBehaviour, ISaveItem
{
 
    public Item item;
 
    public Item ClickItem()
    {
        return this.item;
    }
}


public interface ISaveItem
{
    Item ClickItem();
}
