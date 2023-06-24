using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
{

    public string itemName;
    public Sprite itemImage;


    

    public bool Use()
    {
        return false;
    }
    
}