using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Item", menuName = "Inventroy/Create Item")]
public class InventoryItem : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemType;
    public Sprite icon;
    public string itemInfo;

    

}
