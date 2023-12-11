using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 4���� ���� ��(�ε���, ������, ���ð�, ��) �̿ܿ� ��� �������� ��ȭ�� ���� ��.
/// </summary>
public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;


    /// <summary>
    /// �����ͺ��̽��� ��� ���� ��� ���� ������ ������ ȣ�� ��.
    /// </summary>
    /// <param name="checkInteraction">��ȣ�ۿ� ���� Ȯ��</param>
    /// <returns></returns>
    public Dialogue[] GetDialogue(bool checkInteraction)
    {
        // ��ȣ�ۿ� �� ��ȭ
        if (!checkInteraction)
        {
            dialogue.dialogues = SettingDialogue((int)dialogue.line.x, (int)dialogue.line.y);
            return dialogue.dialogues;
        }
       
        else
        {
            // ��ȣ�ۿ� ���� ��ȭ
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
