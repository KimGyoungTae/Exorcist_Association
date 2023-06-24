using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    protected GameObject target;
    public GameManager manager;

    //[Header("¿Œ∫•≈‰∏Æ")]
    //Inventory inventory;

    void Update()
   {
        if (Input.GetMouseButtonDown(0))
        {
           ShootRay();

        }
   }

     void ShootRay()
    {
            target = null;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
                target = hit.collider.gameObject;
                manager.Action(target);

                HitCheckObject(hit);
            }
    }
 
   void HitCheckObject(RaycastHit2D targeted)
    {
        ISaveItem clickInterface = targeted.transform.GetComponent<ISaveItem>();

        if(clickInterface != null)
        {
            Item item = clickInterface.ClickItem();
           
            if (manager.isAction == false)  // UI √¢¿Ã ¥›»˙ ∂ß
            {
                Destroy(target);
              //  inventory.AddItem(item, target);
                Inventory.instance.AddItem(item, target);
                
             }
         }
    }
 }
