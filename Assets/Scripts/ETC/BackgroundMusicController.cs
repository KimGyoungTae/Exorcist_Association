using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 전체적인 배경음악 관리
/// Prologue, 전투, 엔딩 씬에 들어갈 때는 배경음악 끄기
/// </summary>
public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource musicAudioSource; // 배경음악을 재생할 오디오 소스
    public string targetSceneName = "Prologue"; // 비교할 대상 씬의 이름
    public string BattleSceneName = "New Battle";
    public string EndingSceneName = "Ending";

    private bool isMusicPlaying = true; // 배경음악이 재생 중인지 여부를 나타내는 변수

    private void Awake()
    {
        // 다른 씬으로 이동해도 이 오브젝트가 파괴되지 않도록 설정
        // gameObject == SoundManager 
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 시 이벤트 연결
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 연결 해제
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 현재 씬의 이름을 가져옴
        string currentSceneName = scene.name;

        // 대상 씬과 현재 씬의 이름을 비교
        if (currentSceneName == targetSceneName || currentSceneName == BattleSceneName || currentSceneName == EndingSceneName)
        {
            if (isMusicPlaying)
            {
                // 배경음악이 재생 중이면 중지
                musicAudioSource.Stop();
                isMusicPlaying = false;
               // Debug.Log("음악 중지");
            }
        }
        else
        {
            if (!isMusicPlaying)
            {
                // 배경음악이 중지되었으면 다시 재생
                musicAudioSource.Play();
                isMusicPlaying = true;
              //  Debug.Log("음악 재생");
            }
        }

        // SoundManger 중복 생성 방지를 위해 Destroy() 함수 호출
        if (currentSceneName == "Team Mark")
        {
            Destroy(gameObject);
        }
    }

}
