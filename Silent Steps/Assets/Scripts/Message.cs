using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    [SerializeField]
    private TMP_FontAsset fontAsset;
    [SerializeField]
    private float fontSize;
    [SerializeField]
    private string text;
    //[SerializeField]
    //private float typeSpeed;

    public TMP_FontAsset FontAsset { get { return fontAsset; } }
    public float FontSize { get { return fontSize; } }
    public string Text { get { return text; } }
}
