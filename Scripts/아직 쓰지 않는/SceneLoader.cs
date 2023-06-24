using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // CanvasGroup ������Ʈ
    public CanvasGroup fadeCg;

    // Fade in ó�� �ð�
    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;

    // ȣ���� ���� �� �ε� ����� ������ ��ųʸ�
    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();

    // ȣ���� ���� ���� ����
    void InitSceneInfo()
    {
        // ȣ���� ���� ������ ��ųʸ��� �߰�
        loadScenes.Add("MoveMap", LoadSceneMode.Additive); // �̵��� ��
        loadScenes.Add("Map", LoadSceneMode.Additive); // �ٽ� ó�� ȭ��

    }    



    IEnumerator Start()
    {
        InitSceneInfo();

        // ó�� ���İ��� ����(������)
        fadeCg.alpha = 1.0f;

        // �������� ���� �ڷ�ƾ���� ȣ��
        foreach (var _loadScene in loadScenes) 
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));

        }

        // Fade In �Լ� ȣ��
        StartCoroutine(Fade(0.0f));
    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        // �񵿱� ������� ���� �ε��ϰ� �ε尡 �Ϸ�� ������ ���
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        // ȣ��� ���� Ȱ��ȭ
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    // Fade In/Out ��Ű�� �Լ�
    IEnumerator Fade(float finalAlpha)
    {
        // ����Ʈ���� ������ ���� �����ϱ� ���� �������� ���� Ȱ��ȭ
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Map"));
        fadeCg.blocksRaycasts = true;

        // ���밪 �Լ��� ������� ���
        float fadeSpeed = Math.Abs(fadeCg.alpha - finalAlpha) / fadeDuration;

        //���İ��� ����
        while(!Mathf.Approximately(fadeCg.alpha, finalAlpha))
        {
             // MoveToward �Լ��� Lerp �Լ��� ������ �Լ��� ���İ��� �����Ѵ�.
             fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

            yield return null;
        }

        fadeCg.blocksRaycasts = false;

        // fade In�� �Ϸ�� �� SceneLoader ���� ����(Unload)
        SceneManager.UnloadSceneAsync("SceneLoader");


    }

    void Update()
    {
        
    }
}
