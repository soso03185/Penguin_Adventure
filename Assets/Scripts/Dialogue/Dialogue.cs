﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    [TextArea(1, 4)]
    public string[] sentences;
    public string[] Name; 
    public Sprite[] sprites;
    public Sprite[] dialogueWindows;
}