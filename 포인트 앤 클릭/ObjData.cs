using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNPC;

    public Item item;
    public SpriteRenderer image;


    public static ObjData instance;


    private void Awake()
    {
        instance = this;
    }
}
