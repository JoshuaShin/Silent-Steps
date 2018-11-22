using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private GameObject northRoom;
    [SerializeField]
    private GameObject westRoom;
    [SerializeField]
    private GameObject southRoom;
    [SerializeField]
    private GameObject eastRoom;
    [SerializeField]
    private GameObject connectedRoom;
    private int rotation;

    public GameObject NorthRoom { get { return northRoom; } }
    public GameObject WestRoom { get { return westRoom; } }
    public GameObject SouthRoom { get { return southRoom; } }
    public GameObject EastRoom { get { return eastRoom; } }
    public GameObject ConnectedRoom { get { return connectedRoom; } }

    public void RotateRoomCounterclockwise()
    {
        if (rotation == 270)
        {
            rotation = 0;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            rotation += 90;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

        foreach (Transform child in transform)
        {
            if (child.tag == "Door")
            {
                Door door = child.GetComponent<Door>();
                if (door.DoorLocation == Door.Location.East)
                {
                    door.DoorLocation = Door.Location.North;
                }
                else
                {
                    door.DoorLocation++;
                }
            }
        }
    }

    public void ResetRoom()
    {
        while (rotation != 0)
        {
            RotateRoomCounterclockwise();
        }
    }
}
