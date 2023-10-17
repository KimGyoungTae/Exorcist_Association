// ��� : �� �ǹ� �̸��� ���� �ٸ� ��ȭ ����ؿ���
// �� ���� ��ȭ ��밡 3���̶� ��ũ��Ʈ�� �ϳ� �� ����� ����.  -> ���� �ʿ�..

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    //[SerializeField] ItemDialogueEvent itemDialogueEvent;

    [SerializeField] DialogueEvent dialogue;


    // �����ͺ��̽��� �� ��° �ٺ��� ~���� ������ ������ ȣ��
    
    public Dialogue[] GetItemDialogue(string Name)
    {

       // Debug.Log(Name);

        if(Name == "�����п�")
        {
            dialogue.dialogues = SettingDialogue(1, 2);
          
        }

        if (Name == "ī��")
        {
            dialogue.dialogues = SettingDialogue(3, 3);

        }

        if (Name == "����")
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
