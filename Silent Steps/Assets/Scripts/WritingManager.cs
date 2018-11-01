using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WritingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelWriting;
    [SerializeField]
    private TMP_Text textWriting;
    private Message currentMessage;

    public static WritingManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OpenPanelWriting(Message writing)
    {
        currentMessage = writing;
        SoundManager.instance.PlayClickSfx();

        if (!currentMessage.isRead)
        {
            PlayStartOfMessageEvent();
        }
        
        SetText(writing);
        panelWriting.SetActive(true);
    }

    public void ClosePanelWriting()
    {
        if (!currentMessage.isRead)
        {
            currentMessage.isRead = true;
            PlayEndOfMessageEvent();
        }
        
        panelWriting.SetActive(false);
    }

    private void PlayStartOfMessageEvent()
    {
        if (currentMessage.StartOfMessageEvent == Message.MessageStartEvent.Footsteps)
        {
            SoundManager.instance.PlayFootstepsSfx();
        }
        else if (currentMessage.StartOfMessageEvent == Message.MessageStartEvent.drips)
        {
            SoundManager.instance.PlayDripsSfx();
        }
        else if (currentMessage.StartOfMessageEvent == Message.MessageStartEvent.Scream)
        {
            SoundManager.instance.PlayScreamSfx();
        }
    }

    private void PlayEndOfMessageEvent()
    {
        if (currentMessage.EndOfMessageEvent == Message.MessageEndEvent.RightBehindYou)
        {
            EventManager.instance.PlayRightBehindYou();
        }
        else if (currentMessage.EndOfMessageEvent == Message.MessageEndEvent.FullOfFootsteps)
        {
            EventManager.instance.PlayFullOfFootsteps();
            Destroy(currentMessage.gameObject); // TODO: Find more permanent solution
        }
        else if (currentMessage.EndOfMessageEvent == Message.MessageEndEvent.SpawnDoors)
        {
            EventManager.instance.PlaySpawnDoors();
        }
    }

    private void SetText(Message writing)
    {
        textWriting.font = writing.FontAsset;
        textWriting.fontSize = writing.FontSize;
        textWriting.text = writing.Text;
    }
}
