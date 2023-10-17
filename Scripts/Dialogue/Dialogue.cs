using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip(" ĳ���� �̸� ")]
    public string name;

    [Tooltip("��� ����")]
    public string[] contexts;

}

[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;

    [Space]
    public Vector2 lineAfter;
    public Dialogue[] dialoguesAfter;

    

}