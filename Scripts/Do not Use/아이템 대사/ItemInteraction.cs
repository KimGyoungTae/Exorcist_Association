// 요약 : 상가 건물 이름에 따라 다른 대화 출력해오기
// 한 씬에 대화 상대가 3명이라 스크립트를 하나 더 만든거 같음.  -> 수정 필요..

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    //[SerializeField] ItemDialogueEvent itemDialogueEvent;

    [SerializeField] DialogueEvent dialogue;


    // 데이터베이스에 몇 번째 줄부터 ~까지 가져올 것인지 호출
    
    public Dialogue[] GetItemDialogue(string Name)
    {

       // Debug.Log(Name);

        if(Name == "영어학원")
        {
            dialogue.dialogues = SettingDialogue(1, 2);
          
        }

        if (Name == "카페")
        {
            dialogue.dialogues = SettingDialogue(3, 3);

        }

        if (Name == "꽃집")
        {
            dialogue.dialogues = SettingDialogue(4, 5);

        }


        return dialogue.dialogues;
    }


    Dialogue[] SettingDialogue(int p_lineX, int p_lineY)
    {
        Dialogue[] t_Dialogues = DataBaseManager.instance.GetDialogue(p_lineX, p_lineY);

        return t_Dialogues;
    }

}
