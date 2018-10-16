using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ClickToMove : MonoBehaviour
{
    public GameObject footstep;
    public float movementSpeed = 0.7f;
    public float objectInterationDistance = 0.2f;
    public float footstepFrequency = 0.4f;
    
    private Tween currentTween = null;
    private Transform clickedObject;
    private bool isObjectClicked;
    private bool isMoving;

    private Vector3 previousPosition;
    private float nextFootstepTime = 0f;

    void Awake()
    {

    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject() && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("Room"))
                {
                    isMoving = true;
                    isObjectClicked = false;
                    MoveTo(hit.point);
                }
                else
                {
                    isMoving = true;
                    isObjectClicked = true;
                    clickedObject = hit.transform;

                    MoveTo(clickedObject.position);
                }
            }
        }

        if (isObjectClicked)
        {
            ObjectClicked();
        }

        // Force a footstep when stopping
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


    private void ObjectClicked()
    {     
        if (clickedObject == null)
        {
            return;
        }
        
        float distanceFromObject = Vector3.Distance(transform.position, clickedObject.position);
        
        if (distanceFromObject <= objectInterationDistance)
        {
            isObjectClicked = false;
            MoveStop();

            if (clickedObject.CompareTag("Door"))
            {
                RoomManager.instance.ChangeRoom(clickedObject.GetComponent<Door>());
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
        isMoving = false;

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
        if (isMoving && Time.time > nextFootstepTime)
        {
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }

    private void MakeFootstepForced()
    {
        if (isMoving && Time.time > nextFootstepTime - footstepFrequency/2.5f)
        {
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }
}


