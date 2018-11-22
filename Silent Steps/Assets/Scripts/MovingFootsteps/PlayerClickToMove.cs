using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerClickToMove : MovingFootsteps
{
    public float objectInterationDistance = 0.2f;
    
    private Transform clickedObject;
    private bool isObjectClicked;

    void Awake()
    {

    }

    protected override void Update()
    {
        base.Update();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject() && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))) // && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
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
            else if (clickedObject.CompareTag("Switch"))
            {
                RoomManager.instance.RotateConnectedRoom();
            }
            else if (clickedObject.CompareTag("Writing"))
            {
                WritingManager.instance.OpenPanelWriting(clickedObject.GetComponent<Message>());
            }
            else if (clickedObject.CompareTag("Npc"))
            {
                WritingManager.instance.OpenPanelWriting(clickedObject.GetComponent<Message>());

                // Message npcMessage = clickedObject.GetComponent<Message>();
                // DialogueManager.instance.DisplayText(npcMessage.FontAsset, npcMessage.FontSize, npcMessage.Text);
            }
        }
    }
}


