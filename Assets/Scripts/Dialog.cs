using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public Character SpeakingCharacter;
    public List<CharDialog> Dialogs;
}

public enum Character
{
    Sergej,
    Electronic,
    Girl,
}

[Serializable]
public class CharDialog
{
    public string ContextName;
    public int Index;
    [TextArea(3, 10)]
    public string Text;
}
