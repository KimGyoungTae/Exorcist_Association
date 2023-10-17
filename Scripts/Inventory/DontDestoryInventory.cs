using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryInventory : MonoBehaviour
{
    public static DontDestoryInventory donDestoryInstance;

    private void Awake()
    {

        if (donDestoryInstance == null)
        {
            donDestoryInstance = this;
            DontDestroyOnLoad(gameObject);
        }


        else
        {
            Destroy(gameObject);
        }

    }
}
