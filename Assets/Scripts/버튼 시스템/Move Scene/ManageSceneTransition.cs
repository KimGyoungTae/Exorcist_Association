using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// typeNumber에 따라 씬 이동과 페이드 인 아웃 효과 진행
/// </summary>
public class ManageSceneTransition : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public bool isChange = false;

   
    public void FadeScene(int typeNumber)
    {
       // int typeNumber = 0;
        StartCoroutine(FadeFlow(typeNumber));
    }

    public void ChangeStartScene()
    {
        SceneManager.LoadScene("Team Mark");
        InitializeData();
    }

    void InitializeData()
    {
        VisitManager.instance.ResetVisitStatus();
        PenaltyManager.instance.correctAnswerCount = 0;
        InventoryManager.Instance.RemoveAllItems();
    }

  

    public void MapChange(int typeNumber)
    {
       

        if (typeNumber == 0)
        {
            SceneManager.LoadScene("Estate");
            // 씬 이동 시 무조건 인벤토리 창 닫기 
            InventoryManager.Instance.CloseUI();
        }

        else if (typeNumber == 1)
        {
            SceneManager.LoadScene("Construction");
            InventoryManager.Instance.CloseUI();
        }

        else if (typeNumber == 2)
        {
            SceneManager.LoadScene("Street");
            InventoryManager.Instance.CloseUI();
        }

        else if (typeNumber == 3)
        {
            SceneManager.LoadScene("Residential");
            InventoryManager.Instance.CloseUI();
        }

        else if (typeNumber == 4)
        {
            SceneManager.LoadScene("Main Page");
            InventoryManager.Instance.CloseUI();
        }

        else if (typeNumber == 5)
        {
            SceneManager.LoadScene("Dialog");
        }

        else if (typeNumber == 6)
        {
            SceneManager.LoadScene("Prologue");        
        }


        // 이후 부터는 ButtonType 열거형 타입과 무관하게 쓰임..
        else if (typeNumber == 7)
        {
            SceneManager.LoadScene("New Before Battle");
        }

        else if (typeNumber == 8)
        {
            SceneManager.LoadScene("New Battle");
        }

        else if(typeNumber == 9)
        {
            SceneManager.LoadScene("Ending");
        }

        else if(typeNumber == 10)
        {
            SceneManager.LoadScene("Grade Report");
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

