using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
