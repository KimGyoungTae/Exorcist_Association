using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 갱신된 정답 개수를 파악하여 다른 씬에서도 쓸 수 있게 하도록 만듬.
/// 정답 개수는 마지막 성적표 출력 씬에서 적용 된다.
/// </summary>
public class PenaltyManager : MonoBehaviour
{
    // Singleton 패턴을 사용하여 전역적으로 접근 가능한 인스턴스 생성
    public static PenaltyManager instance;
    public int correctAnswerCount = 0; // 정답 횟수


    private void Awake()
    {
        if (instance == null)
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


   public void GetCorrectAnswer(int getCount)
    {
        correctAnswerCount = getCount;
    }

}
