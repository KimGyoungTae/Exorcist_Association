using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

   
    public bool isChange = false;
    
    private ChangeScene changeScene;
    //private BackScene backScene;


    void Awake()
    {
        changeScene = GetComponent<ChangeScene>();
      //  backScene = GetComponent<BackScene>();
    
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
       // yield return new WaitForSeconds(1.0f);

       changeScene.ChangeSceneBtn();
    
      
      //backScene.BackSceneBtn();
      //yield return new WaitForSeconds(backScene.GetSceneTransitionTime());
     
      
        

        // 페이드 아웃
        while (alpha.a > 0f)
        {
       //     Debug.Log("fade out");
             time += Time.deltaTime / F_time;
             alpha.a = Mathf.Lerp(1, 0, time);
             Panel.color = alpha;          
      
             yield return null;
        }


        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
