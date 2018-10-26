using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Footstep : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2f;

    void Awake()
    {
        FadeFootstepAlpha();
        Destroy(gameObject, lifeTime);
    }

    private void FadeFootstepAlpha()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        currentColor.a = 0;
        spriteRenderer.DOColor(currentColor, lifeTime);
    }
}
