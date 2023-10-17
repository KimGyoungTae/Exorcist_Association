using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip(" 캐릭터 이름 ")]
    public string name;

    [Tooltip("대사 내용")]
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
