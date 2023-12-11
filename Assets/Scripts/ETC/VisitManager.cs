using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 4���� ���� ������ ��(���� ���� ��) visitCount�� ������.
/// �� �� �湮�� ���� �� �̻� �߰��� �������� �ʴ´�.
/// 4���� ���� �� ���� ������ �� �߾� ��� ��ư�� ������ �ȴ�.
/// </summary>
public class VisitManager : MonoBehaviour
{
    // Singleton ������ ����Ͽ� ���������� ���� ������ �ν��Ͻ� ����
    public static VisitManager instance;
    public int visitCount = 0; // �湮 Ƚ��

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            // ���� �Ŵ��� ������Ʈ�� �� ��ȯ �� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ���� ���� �Ǵ� ���� �޴��� ���ư� �� �湮 ���� �ʱ�ȭ
        ResetVisitStatus();
    }

    public void VisitScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // �ش� ���� ó�� �湮�Ǿ����� Ȯ��
        if (!PlayerPrefs.HasKey(sceneName))
        {
            PlayerPrefs.SetInt(sceneName, 1); // �湮 ���θ� ����
            PlayerPrefs.Save();

            visitCount++; // Count ���� ����

            // �ߺ� �湮�� �����ϱ� ���� �湮 ������ �˸�
            Debug.Log("Visited " + sceneName + " | Visit Count: " + visitCount);
        }
        else
        {
            // �ߺ� �湮�� �����Ǿ����� �˸�
            Debug.Log("Already visited " + sceneName + " | Visit Count: " + visitCount);
        }
    }


    // ������ ���� ������ �� �湮ī��Ʈ �ʱ�ȭ
    public void ResetVisitStatus()
    {
        // �湮 ���� �ʱ�ȭ
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        visitCount = 0;
        Debug.Log("Visit status reset");
    }
}
