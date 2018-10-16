using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum Location {North, East, South, West}

    [SerializeField]
    private bool isLocked;
    [SerializeField]
    private Location doorLocation;
    [SerializeField]
    private GameObject roomConnected;

    public bool IsLocked { get { return isLocked; } }
    public Location DoorLocation { get { return doorLocation; } }
    public GameObject RoomConnected { get { return roomConnected; } }
}
