using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Writing : MonoBehaviour
{
    [SerializeField]
    private TMP_FontAsset font;
    [SerializeField]
    private float fontSize;
    [SerializeField]
    private string text;

    public TMP_FontAsset Font { get { return font; } }
    public float FontSize { get { return fontSize; } }
    public string Text { get { return text; } }
}
