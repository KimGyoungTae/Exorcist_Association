using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CSV 파일 이름에 따라 각각의 알맞은 대화가 나오도록 함.
/// 4개의 조사 맵(부동산, 공사장, 주택가, 상가)에만 적용 됨.
/// </summary>
public class DialogueInteraction : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    /// <summary>
    /// </summary>
    /// <param name="TagName">상호작용 할 태그 이름</param>
    /// <param name="objName">상호작용 오브젝트 이름</param>
    /// <param name="FileName">CSV 파일 이름</param>
    public Dialogue[] GetDialogueContents(string TagName, string objName, string FileName)
    {
        // 파일별로 태그에 대응하는 동작을 정의한 Dictionary
        Dictionary<string, Action> fileActions = new Dictionary<string, Action>
    {
        { "Estate Dialog", () =>
            {
                // TagName에 따라 동작 정의
                if (TagName == "Item")
                {
                    // Item에 대한 처리
                    if(objName == "신축 아파트")
                    {
                        dialogue.dialogues = SettingDialogue(1, 4);
                    }

                    if(objName == "재개발")
                    {
                        dialogue.dialogues = SettingDialogue(5, 7);
                    }
                }
                else if (TagName == "NPC")
                {
                    // NPC에 대한 처리
                    dialogue.dialogues = SettingDialogue(8, 12);
                }
                else
                {
                    // 다른 경우에 대한 처리
                    // ...
                }
            }
        },

        { "Street New Dialog", () =>
            {
                // 다른 파일에 대한 처리
               if(TagName == "NPC")
                {
                    if(objName == "영어학원")
                    {
                        dialogue.dialogues = SettingDialogue(1, 9);
                    }

                    if(objName == "카페")
                    {
                        dialogue.dialogues = SettingDialogue(10, 23);
                    }

                    if(objName == "꽃집")
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

        // 추가적인 파일 및 동작 정의 가능
    };

        // FileName에 대한 동작을 수행
        if (fileActions.TryGetValue(FileName, out Action fileAction))
        {
            fileAction.Invoke();
        }
        else
        {
            // 다른 경우에 대한 처리
            Debug.Log("올바른 파일명을 찾지 못하였습니다..");
        }

        return dialogue.dialogues;
    }

    /// <summary>
    /// </summary>
    /// <param name="p_lineX">대화 처음 줄</param>
    /// <param name="p_lineY">대화 마지막 줄</param>
    /// <returns></returns>
    Dialogue[] SettingDialogue(int p_lineX, int p_lineY)
    {
        Dialogue[] t_Dialogues = DataBaseManager.instance.GetDialogue(p_lineX, p_lineY);

        return t_Dialogues;
    }


}
