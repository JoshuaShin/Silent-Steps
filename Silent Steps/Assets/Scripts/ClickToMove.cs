using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickToMove : MonoBehaviour {

    public float movementSpeed = 0.7f;
    public float doorOpeningDistance = 0.2f;

    private Tween currentTween = null;
    private Transform clickedDoor;
    private bool walking;
    private bool doorClicked;

    void Awake() {

    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
            if (Physics.Raycast(ray, out hit, 100)) {

                if (hit.collider.CompareTag("Room")) {
                    walking = true;
                    doorClicked = false;

                    MoveTo(hit.point);
                }

                else if (hit.collider.CompareTag("Door")) {
                    walking = true;
                    doorClicked = true;
                    clickedDoor = hit.transform;

                    MoveTo(clickedDoor.position);
                }
            }
        }

        if (doorClicked) {
            DoorClicked();
        }

        /*
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
                walking = false;
        }
        */

        else
        {
            walking = true;
        }

        //anim.SetBool("IsWalking", walking);
    }


    private void DoorClicked() {
        if (clickedDoor == null)
            return;


        float distanceFromDoor = Vector3.Distance(transform.position, clickedDoor.position);

        if (distanceFromDoor >= doorOpeningDistance)
        {

            walking = true;
        }

        if (distanceFromDoor <= doorOpeningDistance)
        {

            transform.LookAt(clickedDoor);
            //Vector3 dirToShoot = clickedDoor.transform.position - transform.position;
            /*
            if (Time.time > nextFire)
            {
                nextFire = Time.time + shootRate;
                //shootingScript.Shoot(dirToShoot);
            }
            */

            MoveStop();
            walking = false;
            doorClicked = false;
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
        if (currentTween != null) {
            currentTween.Kill();
        }
    }

    private void TurnTo(Vector3 target)
    {
        transform.right = target - transform.position;
    }

}


