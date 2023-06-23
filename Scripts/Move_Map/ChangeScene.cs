using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChangeScene : MonoBehaviour
{

    public Image Panel;
    float time = 0f;
    float F_time = 1f;


    public float GetSceneTransitionTime()
    {
        return 2.0f;
    }


    public void ChangeSceneBtn()
    {
        SceneManager.LoadScene("MoveMap");
        //SceneManager.LoadScene("SceneLoader");
    }

    public void StartSceneFade()
    {
        StartCoroutine(FadeFlow());

    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;


        // ���̵� ��        
        while (alpha.a < 1f)
        {

            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;

        }

        time = 0f;

        SceneManager.LoadScene("Map");

        // ���̵� �ƿ�
        while (alpha.a > 0f)
        {
          //  Debug.Log("fade out");
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;

            yield return null;
        }


        Panel.gameObject.SetActive(false);
        yield return null;
    }


}
