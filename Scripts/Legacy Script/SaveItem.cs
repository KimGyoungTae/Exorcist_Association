using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveItem : MonoBehaviour, ISaveItem
{
    [Header("������")]
    public Item item;
    [Header("������ �̹���")]
    public SpriteRenderer itemImage;

    void Start()
    {
        itemImage.sprite = item.itemImage;
    }

    public Item ClickItem()
    {
        return this.item;
    }
}


public interface ISaveItem
{
    Item ClickItem();
}
