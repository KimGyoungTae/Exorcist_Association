using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image itemIcon;

    private Item _item;
  
    public Item item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
            if(_item != null)
            {
                UpdateSlotUI();
            }
            else
            {
                itemIcon.color = new Color(1, 1, 1, 0);
            }
        }
    }


    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.color = new Color(1, 1, 1, 1);
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
}
