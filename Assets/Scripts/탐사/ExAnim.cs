using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAnim : MonoBehaviour
{
    float time;
    bool CheckAnim;

    // Start is called before the first frame update
    void Start()
    {
        CheckAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckAnim = true;
        }

        if (CheckAnim == true)
        {
            PlayAnim();
        }
    }


    public void PlayAnim()
    {
        transform.localScale = Vector3.one * (1 + time);
        time += Time.deltaTime;

        if (time > 0.3f)
        {
            resetAnim();
        }
    }

    public void resetAnim()
    {
        time = 0;
        transform.localScale = Vector3.one;
        CheckAnim = false;
    }
}
