using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float delay = 2f;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
