using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public enum MessageStartEvent { None, Footsteps, drips, Scream }
    public enum MessageEndEvent { None, RightBehindYou, FullOfFootsteps, SpawnDoors }

    [SerializeField]
    private TMP_FontAsset fontAsset;
    [SerializeField]
    private float fontSize;
    [SerializeField]
    private string text;
    //[SerializeField]
    //private float typeSpeed;
    [SerializeField]
    private MessageStartEvent startOfMessageEvent;
    [SerializeField]
    private MessageEndEvent endOfMessageEvent;
    public bool isRead = false;

    public TMP_FontAsset FontAsset { get { return fontAsset; } }
    public float FontSize { get { return fontSize; } }
    public string Text { get { return text; } }
    public MessageStartEvent StartOfMessageEvent { get { return startOfMessageEvent; } set { startOfMessageEvent = value; } }
    public MessageEndEvent EndOfMessageEvent { get { return endOfMessageEvent; } set { endOfMessageEvent = value; } }
}
