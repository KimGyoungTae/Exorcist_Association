using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CSV ���� �̸��� ���� ������ �˸��� ��ȭ�� �������� ��.
/// 4���� ���� ��(�ε���, ������, ���ð�, ��)���� ���� ��.
/// </summary>
public class DialogueInteraction : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    /// <summary>
    /// </summary>
    /// <param name="TagName">��ȣ�ۿ� �� �±� �̸�</param>
    /// <param name="objName">��ȣ�ۿ� ������Ʈ �̸�</param>
    /// <param name="FileName">CSV ���� �̸�</param>
    public Dialogue[] GetDialogueContents(string TagName, string objName, string FileName)
    {
        // ���Ϻ��� �±׿� �����ϴ� ������ ������ Dictionary
        Dictionary<string, Action> fileActions = new Dictionary<string, Action>
    {
        { "Estate Dialog", () =>
            {
                // TagName�� ���� ���� ����
                if (TagName == "Item")
                {
                    // Item�� ���� ó��
                    if(objName == "���� ����Ʈ")
                    {
                        dialogue.dialogues = SettingDialogue(1, 4);
                    }

                    if(objName == "�簳��")
                    {
                        dialogue.dialogues = SettingDialogue(5, 7);
                    }
                }
                else if (TagName == "NPC")
                {
                    // NPC�� ���� ó��
                    dialogue.dialogues = SettingDialogue(8, 12);
                }
                else
                {
                    // �ٸ� ��쿡 ���� ó��
                    // ...
                }
            }
        },

        { "Street New Dialog", () =>
            {
                // �ٸ� ���Ͽ� ���� ó��
               if(TagName == "NPC")
                {
                    if(objName == "�����п�")
                    {
                        dialogue.dialogues = SettingDialogue(1, 9);
                    }

                    if(objName == "ī��")
                    {
                        dialogue.dialogues = SettingDialogue(10, 23);
                    }

                    if(objName == "����")
                    {
                        dialogue.dialogues = SettingDialogue(24, 26);
                    }
                }

               else { }
            }
        },

        {  "Construction Dialog", () =>
            {
                if(TagName == "Item")
                {
                    dialogue.dialogues = SettingDialogue(1, 4);
                }

                else if(TagName == "NPC")
                {
                    dialogue.dialogues = SettingDialogue(5, 9);
                }

                else { }
            }


        },

        {  "Residential Dialog", () =>
            {
                if(TagName == "NPC")
                {
                    dialogue.dialogues = SettingDialogue(1, 4);
                }

                else { }
            }


        }

        // �߰����� ���� �� ���� ���� ����
    };

        // FileName�� ���� ������ ����
        if (fileActions.TryGetValue(FileName, out Action fileAction))
        {
            fileAction.Invoke();
        }
        else
        {
            // �ٸ� ��쿡 ���� ó��
            Debug.Log("�ùٸ� ���ϸ��� ã�� ���Ͽ����ϴ�..");
        }

        return dialogue.dialogues;
    }

    /// <summary>
    /// </summary>
    /// <param name="p_lineX">��ȭ ó�� ��</param>
    /// <param name="p_lineY">��ȭ ������ ��</param>
    /// <returns></returns>
    Dialogue[] SettingDialogue(int p_lineX, int p_lineY)
    {
        Dialogue[] t_Dialogues = DataBaseManager.instance.GetDialogue(p_lineX, p_lineY);

        return t_Dialogues;
    }


}
