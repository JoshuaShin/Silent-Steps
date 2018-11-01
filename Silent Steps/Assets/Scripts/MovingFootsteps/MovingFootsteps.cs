using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MovingFootsteps : MonoBehaviour
{
    public GameObject footstep;
    public float movementSpeed = 0.7f;
    public float footstepFrequency = 0.4f;
    public bool isSilent = false;

    private Tween currentTween = null;
    private Vector3 previousPosition;
    private float nextFootstepTime = 0f;
    protected bool isMoving;

    protected virtual void Update()
    {
        // Force a footstep when naturally stopping
        if (previousPosition == transform.position && isMoving)
        {
            MakeFootstepForced();
        }

        // Check if gameObject is moving and update moving
        if (previousPosition == transform.position)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
            previousPosition = transform.position;
        }

        MakeFootstep();
    }

    protected void MoveTo(Vector3 target)
    {
        MoveStop();
        TurnTo(target);

        float distance = Vector3.Distance(transform.position, target);
        currentTween = transform.DOMove(target, distance / movementSpeed);
    }

    protected void MoveStop()
    {
        //MakeFootstepForced();
        isMoving = false;

        if (currentTween != null)
        {
            currentTween.Kill();
        }
    }

    private void TurnTo(Vector3 target)
    {
        //transform.right = target - transform.position;

        Vector3 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void MakeFootstep()
    {
        if (isMoving && Time.time > nextFootstepTime)
        {
            if (!isSilent)
            {
                SoundManager.instance.PlayFootstepSfx();
            }
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }

    private void MakeFootstepForced()
    {
        if (isMoving && Time.time > nextFootstepTime - footstepFrequency / 2.5f)
        {
            if (!isSilent)
            {
                SoundManager.instance.PlayFootstepSfx();
            }
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }
}
