using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemDialogue
{
    [Tooltip(" ������ ��ġ ")]
    public string place;

    [Tooltip(" ������ �̸� ")]
    public string itemName;

    [Tooltip("��� ����")]
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
