using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndApplicationAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float delay = 2f;

    void Start()
    {
        StartCoroutine(EndApplicationCoroutine());
    }

    IEnumerator EndApplicationCoroutine()
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
