using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackScene : MonoBehaviour
{

    public Image Panel;
    float time = 0f;
    float F_time = 1f;


    public float GetSceneTransitionTime()
    {
        return 2.0f;
    }


    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
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
      //  yield return new WaitForSeconds(1.0f);

        BackSceneBtn();

        // 페이드 아웃
        while (alpha.a > 0f)
        {
        //    Debug.Log("fade out");
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;

            yield return null;
        }


        Panel.gameObject.SetActive(false);
        yield return null;
    }
    public void BackSceneBtn()
    {
        SceneManager.LoadScene("Map");
    }
}
