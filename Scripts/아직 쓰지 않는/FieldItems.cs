using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{

    public Item item;
    public SpriteRenderer image;

    public void setItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;

        image.sprite = item.itemImage;
    }

    public Item GetItem() 
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}
