using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;

    private void Awake()
    {
        instance = this; 
    }

    public List<Item> itemDB = new List<Item>();

    //  public GameObject fieldItemPrefab;
    //  public Vector3[] pos;

    //private void Start()
    //{
    //    for(int i = 0; i< 5; i++)
    //    {
    //       GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
    //       go.GetComponent<FieldItems>().setItem(itemDB[Random.Range(0,3)]);

    //    }
    //}

    private void Start()
    {
        for( int i = 0; i<5; i++)
        {
     //   ObjData().setItem(itemDB[Random.Range(0, 3)]);
        }
    }
}
