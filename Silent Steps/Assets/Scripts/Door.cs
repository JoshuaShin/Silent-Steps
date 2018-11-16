using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum Location {North, West, South, East}

    [SerializeField]
    private bool isLocked;
    [SerializeField]
    private Location doorLocation;
    [SerializeField]
    private GameObject roomConnected;

    public bool IsLocked { get { return isLocked; } set { isLocked = value; } }
    public Location DoorLocation { get { return doorLocation; } set { doorLocation = value; } }
    public GameObject RoomConnected { get { return roomConnected; } }
}
