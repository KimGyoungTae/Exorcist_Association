using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���ŵ� ���� ������ �ľ��Ͽ� �ٸ� �������� �� �� �ְ� �ϵ��� ����.
/// ���� ������ ������ ����ǥ ��� ������ ���� �ȴ�.
/// </summary>
public class PenaltyManager : MonoBehaviour
{
    // Singleton ������ ����Ͽ� ���������� ���� ������ �ν��Ͻ� ����
    public static PenaltyManager instance;
    public int correctAnswerCount = 0; // ���� Ƚ��


    private void Awake()
    {
        if (instance == null)
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


   public void GetCorrectAnswer(int getCount)
    {
        correctAnswerCount = getCount;
    }

}
