using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class Message
{
    [Header("Text Settings")]

    /// <summary>
    /// Text to show in larger main text element
    /// </summary>
    public string mainText;

    /// <summary>
    /// Font to use for main text
    /// </summary>
    public Font mainTextFont;

    /// <summary>
    /// Size of main text
    /// </summary>
    public int mainTextSize = 60;

    /// <summary>
    /// Delay before showing main text
    /// </summary>
    public float delay;

    [Header("Other Settings")]

    /// <summary>
    /// Duration to show text
    /// </summary>
    public float duration = 3;
}
