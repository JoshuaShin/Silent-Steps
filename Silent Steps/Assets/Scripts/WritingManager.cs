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
        SetText(writing);
        panelWriting.SetActive(true);
    }

    public void ClosePanelWriting()
    {
        panelWriting.SetActive(false);
    }

    private void SetText(Message writing)
    {
        textWriting.font = writing.FontAsset;
        textWriting.fontSize = writing.FontSize;
        textWriting.text = writing.Text;
    }
}
