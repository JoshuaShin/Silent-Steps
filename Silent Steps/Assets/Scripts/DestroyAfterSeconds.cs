using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float time = 10f;

    void Start () {
        StartCoroutine(DestroyAfterSecondsCoroutine(time));
	}

    IEnumerator DestroyAfterSecondsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
