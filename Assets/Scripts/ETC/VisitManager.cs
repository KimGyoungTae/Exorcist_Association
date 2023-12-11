using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 4개의 맵을 조사할 시(씬에 입장 시) visitCount가 증가함.
/// 한 번 방문한 곳은 더 이상 추가로 증가되지 않는다.
/// 4개의 맵을 한 번씩 조사할 시 중앙 결계 버튼이 열리게 된다.
/// </summary>
public class VisitManager : MonoBehaviour
{
    // Singleton 패턴을 사용하여 전역적으로 접근 가능한 인스턴스 생성
    public static VisitManager instance;
    public int visitCount = 0; // 방문 횟수

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            // 게임 매니저 오브젝트를 씬 전환 시 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 게임 시작 또는 메인 메뉴로 돌아갈 때 방문 여부 초기화
        ResetVisitStatus();
    }

    public void VisitScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // 해당 씬이 처음 방문되었는지 확인
        if (!PlayerPrefs.HasKey(sceneName))
        {
            PlayerPrefs.SetInt(sceneName, 1); // 방문 여부를 저장
            PlayerPrefs.Save();

            visitCount++; // Count 변수 증가

            // 중복 방문을 방지하기 위해 방문 했음을 알림
            Debug.Log("Visited " + sceneName + " | Visit Count: " + visitCount);
        }
        else
        {
            // 중복 방문이 감지되었음을 알림
            Debug.Log("Already visited " + sceneName + " | Visit Count: " + visitCount);
        }
    }


    // 게임을 새로 시작할 때 방문카운트 초기화
    public void ResetVisitStatus()
    {
        // 방문 여부 초기화
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        visitCount = 0;
        Debug.Log("Visit status reset");
    }
}
