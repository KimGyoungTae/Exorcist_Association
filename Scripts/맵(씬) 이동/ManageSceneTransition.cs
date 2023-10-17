using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageSceneTransition : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public bool isChange = false;


    void Awake()
    {

    }

    public void FadeScene(int typeNumber)
    {
       // int typeNumber = 0;
        StartCoroutine(FadeFlow(typeNumber));
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Team Mark");

    }

    public void MapChange(int typeNumber)
    {
        if (typeNumber == 0)
        {
            SceneManager.LoadScene("Estate");
        }

        else if (typeNumber == 1)
        {
            SceneManager.LoadScene("Construction");
        }

        else if (typeNumber == 2)
        {
            SceneManager.LoadScene("Street");
        }

        else if (typeNumber == 3)
        {
            SceneManager.LoadScene("Residential");
        }

        else if (typeNumber == 4)
        {
            SceneManager.LoadScene("Main Map");
        }

        else if (typeNumber == 5)
        {
            SceneManager.LoadScene("Dialog");
          
        }


        else if (typeNumber == 6)
        {
            SceneManager.LoadScene("Prologue");
          
        }


        else if (typeNumber == 7)
        {
            SceneManager.LoadScene("Battle");
        }



        else
        {
            Debug.Log("맵을 찾을 수 없습니다.");
        }

    }

    IEnumerator FadeFlow(int typeNumber)
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;


        // 페이드 인        
        while (alpha.a < 1f)
        {

            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;

        }

       time = 0f;
       //ChangeScene();
       MapChange(typeNumber);

        // 페이드 아웃
        while (alpha.a > 0f)
        {
            // Debug.Log("fade out");
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;

            yield return null;
        }


        Panel.gameObject.SetActive(false);
        yield return null;
    }
}

