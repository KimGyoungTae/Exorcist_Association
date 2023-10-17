using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemDialogue
{
    [Tooltip(" 아이템 위치 ")]
    public string place;

    [Tooltip(" 아이템 이름 ")]
    public string itemName;

    [Tooltip("대사 내용")]
    public string[] contexts;
}

[System.Serializable]
public class ItemDialogueEvent
{
    
    public Vector2 itemline;
   // public int newline;

    public ItemDialogue[] itemDialogues;

    [Space]
    public Vector2 itemlineAfter;
    public ItemDialogue[] itemDialoguesAfter;

}
