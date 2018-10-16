using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickToMove : MonoBehaviour
{
    public GameObject footstep;
    public float movementSpeed = 0.7f;
    public float objectInterationDistance = 0.2f;
    public float footstepFrequency = 0.4f;
    
    private Tween currentTween = null;
    private Transform clickedObject;
    private bool objectClicked;
    private bool moving;

    private Vector3 previousPosition;
    private float nextFootstepTime = 0f;

    void Awake()
    {

    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("Room"))
                {
                    moving = true;
                    objectClicked = false;
                    MoveTo(hit.point);
                }
                else
                {
                    moving = true;
                    objectClicked = true;
                    clickedObject = hit.transform;

                    MoveTo(clickedObject.localPosition);
                }
            }
        }

        if (objectClicked)
        {
            ObjectClicked();
        }

        // Force a footstep when stopping
        if (previousPosition == transform.position && moving)
        {
            MakeFootstepForced();
        }
        
        // Check if gameObject is moving and update moving
        if (previousPosition == transform.position)
        {
            moving = false;
        }
        else
        {
            moving = true;
            previousPosition = transform.position;
        }

        MakeFootstep();
    }


    private void ObjectClicked()
    {     
        if (clickedObject == null)
        {
            return;
        }
        
        float distanceFromObject = Vector3.Distance(transform.position, clickedObject.position);
        
        if (distanceFromObject <= objectInterationDistance)
        {
            objectClicked = false;
            MoveStop();

            if (clickedObject.CompareTag("Door"))
            {
                Debug.Log("DOOR OPENED");
            }
            else if (clickedObject.CompareTag("Writing"))
            {
                WritingManager.instance.OpenPanelWriting(clickedObject.GetComponent<Writing>());
            }

        }
    }

    private void MoveTo(Vector3 target)
    {
        MoveStop();
        TurnTo(target);

        float distance = Vector3.Distance(transform.position, target);
        currentTween = transform.DOMove(target, distance / movementSpeed);
    }

    private void MoveStop()
    {
        moving = false;

        if (currentTween != null)
        {
            currentTween.Kill();
        }
    }

    private void TurnTo(Vector3 target)
    {
        transform.right = target - transform.position;
        //MakeFootstepForced();
    }

    private void MakeFootstep()
    {
        if (moving && Time.time > nextFootstepTime)
        {
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }

    private void MakeFootstepForced()
    {
        if (moving && Time.time > nextFootstepTime - footstepFrequency/2.5f)
        {
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }
}


