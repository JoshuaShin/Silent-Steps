using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textMeshProText;

    private int totalVisibleCharacters;
    private int counter;
    private int visibleCount;
    private IEnumerator revealTextCoroutine;

    public static DialogueManager instance = null;

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

    public void DisplayText(TMP_FontAsset fontAsset, float fontSize, string text, float textDuration = 4f)
    {
        if (revealTextCoroutine != null)
        {
            StopCoroutine(revealTextCoroutine);
        }

        textMeshProText.font = fontAsset;
        textMeshProText.fontSize = fontSize;

        revealTextCoroutine = DisplayTextCoroutine(text, textDuration);
        StartCoroutine(revealTextCoroutine);
    }

    IEnumerator DisplayTextCoroutine(string message, float textDuration)
    {
        textMeshProText.text = message;
        yield return new WaitForSeconds(textDuration);
        textMeshProText.text = "";
    }

    public void TypeText(string message, float textDuration = 4f, float timeBetweenLetters = 0.02f)
    {
        if (revealTextCoroutine != null)
        {
            StopCoroutine(revealTextCoroutine);
        }

        revealTextCoroutine = TypeTextCoroutine(message, textDuration, timeBetweenLetters);
        StartCoroutine(revealTextCoroutine);
    }

    IEnumerator TypeTextCoroutine(string message, float textDuration, float timeBetweenLetters)
    {
        textMeshProText.text = message;

        // Force update of the mesh to get updated information
        textMeshProText.maxVisibleCharacters = 0;
        textMeshProText.ForceMeshUpdate();

        yield return null;

        totalVisibleCharacters = textMeshProText.textInfo.characterCount; // Get # of Visible Character in text object

        counter = 0;
        visibleCount = 0;

        while (true)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);

            textMeshProText.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {
                break;
            }

            counter += 1;

            yield return new WaitForSeconds(timeBetweenLetters);
        }

        yield return new WaitForSeconds(textDuration);
        textMeshProText.text = "";
    }

    public void TypeInstantly(TMP_Text textMeshPro)
    {
        StopCoroutine(revealTextCoroutine);
        textMeshPro.maxVisibleCharacters = totalVisibleCharacters;
        textMeshPro.ForceMeshUpdate();
    }
}
