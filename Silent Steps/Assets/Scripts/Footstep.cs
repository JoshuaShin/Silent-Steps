using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Footstep : MonoBehaviour {

    public float lifetime = 2f;

    void Awake() {
        FadeFootstepAlpha();
        Destroy(gameObject, lifetime);
    }

    private void FadeFootstepAlpha() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        currentColor.a = 0;
        spriteRenderer.DOColor(currentColor, lifetime);
    }
}
