using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowScoreReport : MonoBehaviour
{

    [SerializeField] GameObject[] ScoreReports;

    void Start()
    {
        for(int i = 0; i < ScoreReports.Length; i++)
        {
            if(PenaltyManager.instance.correctAnswerCount == i)
            {
                ScoreReports[i].SetActive(true);
                return;
            }
        }
    }
}
