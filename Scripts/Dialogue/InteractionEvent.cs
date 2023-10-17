using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;
  

    // 데이터베이스에 몇 번째 줄부터 ~까지 가져올 것인지 호출
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
