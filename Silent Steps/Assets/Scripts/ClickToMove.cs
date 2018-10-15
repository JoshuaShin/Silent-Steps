using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickToMove : MonoBehaviour {

    public GameObject footstep;
    public float movementSpeed = 0.7f;
    public float doorOpeningDistance = 0.2f;
    public float footstepFrequency = 0.4f;
    
    private Tween currentTween = null;
    private Transform clickedDoor;
    private bool moving;
    private bool doorClicked;

    private Vector3 previousPosition;
    private float nextFootstepTime = 0f;

    void Awake() {

    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
            if (Physics.Raycast(ray, out hit, 100)) {

                if (hit.collider.CompareTag("Room")) {
                    moving = true;
                    doorClicked = false;

                    MoveTo(hit.point);
                }

                else if (hit.collider.CompareTag("Door")) {
                    moving = true;
                    doorClicked = true;
                    clickedDoor = hit.transform;

                    MoveTo(clickedDoor.position);
                }
            }
        }

        if (doorClicked) {
            DoorClicked();
        }

        // Force a footstep when stopping
        if (previousPosition == transform.position && moving) {
            MakeFootstepForced();
        }
        
        // Check if gameObject is moving
        if (previousPosition == transform.position) {
            moving = false;
        }
        else {
            moving = true;
            previousPosition = transform.position;
        }

        MakeFootstep();
    }


    private void DoorClicked() {     
        if (clickedDoor == null) {
            return;
        }
        
        float distanceFromDoor = Vector3.Distance(transform.position, clickedDoor.position);
        
        if (distanceFromDoor <= doorOpeningDistance) {
            doorClicked = false;
            MoveStop();
            Debug.Log("DOOR OPENED");
        }
    }

    private void MoveTo(Vector3 target) {
        MoveStop();
        TurnTo(target);

        float distance = Vector3.Distance(transform.position, target);
        currentTween = transform.DOMove(target, distance / movementSpeed);
    }

    private void MoveStop() {
        moving = false;

        if (currentTween != null) {
            currentTween.Kill();
        }

        //MakeFootstepForced();
    }

    private void TurnTo(Vector3 target) {
        transform.right = target - transform.position;
        //MakeFootstepForced();
    }

    private void MakeFootstep() {
        if (moving && Time.time > nextFootstepTime) {
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }

    private void MakeFootstepForced() {
        if (moving && Time.time > nextFootstepTime - footstepFrequency/2.5f) {
            nextFootstepTime = Time.time + footstepFrequency;
            GameObject step = Instantiate(footstep);
            step.transform.position = transform.position;
            step.transform.rotation = transform.rotation;
        }
    }
}


