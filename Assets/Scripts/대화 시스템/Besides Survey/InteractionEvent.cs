using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 4개의 조사 맵(부동산, 공사장, 주택가, 상가) 이외에 모든 씬에서의 대화에 적용 됨.
/// </summary>
public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;


    /// <summary>
    /// 데이터베이스에 어디 부터 어디 까지 가져올 것인지 호출 함.
    /// </summary>
    /// <param name="checkInteraction">상호작용 유무 확인</param>
    /// <returns></returns>
    public Dialogue[] GetDialogue(bool checkInteraction)
    {
        // 상호작용 전 대화
        if (!checkInteraction)
        {
            dialogue.dialogues = SettingDialogue((int)dialogue.line.x, (int)dialogue.line.y);
            return dialogue.dialogues;
        }
       
        else
        {
            // 상호작용 이후 대화
            dialogue.dialoguesAfter = SettingDialogue((int)dialogue.lineAfter.x, (int)dialogue.lineAfter.y);
            return dialogue.dialoguesAfter;
        }

    }


    Dialogue[] SettingDialogue(int p_lineX, int p_lineY)
    {
        //dialogue.dialogues = DataBaseManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);

        Dialogue[] t_Dialogues = DataBaseManager.instance.GetDialogue(p_lineX, p_lineY);

        return t_Dialogues;
    }
}
