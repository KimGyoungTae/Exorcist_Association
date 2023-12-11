using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// ��ü���� ������� ����
/// Prologue, ����, ���� ���� �� ���� ������� ����
/// </summary>
public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource musicAudioSource; // ��������� ����� ����� �ҽ�
    public string targetSceneName = "Prologue"; // ���� ��� ���� �̸�
    public string BattleSceneName = "New Battle";
    public string EndingSceneName = "Ending";

    private bool isMusicPlaying = true; // ��������� ��� ������ ���θ� ��Ÿ���� ����

    private void Awake()
    {
        // �ٸ� ������ �̵��ص� �� ������Ʈ�� �ı����� �ʵ��� ����
        // gameObject == SoundManager 
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �� �̺�Ʈ ����
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ���� ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ���� �̸��� ������
        string currentSceneName = scene.name;

        // ��� ���� ���� ���� �̸��� ��
        if (currentSceneName == targetSceneName || currentSceneName == BattleSceneName || currentSceneName == EndingSceneName)
        {
            if (isMusicPlaying)
            {
                // ��������� ��� ���̸� ����
                musicAudioSource.Stop();
                isMusicPlaying = false;
               // Debug.Log("���� ����");
            }
        }
        else
        {
            if (!isMusicPlaying)
            {
                // ��������� �����Ǿ����� �ٽ� ���
                musicAudioSource.Play();
                isMusicPlaying = true;
              //  Debug.Log("���� ���");
            }
        }

        // SoundManger �ߺ� ���� ������ ���� Destroy() �Լ� ȣ��
        if (currentSceneName == "Team Mark")
        {
            Destroy(gameObject);
        }
    }

}
