using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenToPosition : MonoBehaviour
{
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float duration;

    void Start ()
    {
        MoveTo(target);
    }

    private void MoveTo(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        transform.DOMove(target, duration);
    }
}
