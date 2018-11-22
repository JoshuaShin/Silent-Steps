using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelBlackout;
    [SerializeField]
    private GameObject currentRoom;
    [SerializeField]
    private GameObject rooms;
    [SerializeField]
    private float roomTransitionTime = 0.65f;

    private bool isRotationSwitchSpawned = false;
    private bool isRotating;

    public static RoomManager instance = null;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //EventManager.instance.SpawnRotationSwitches();
    }

    public GameObject GetCurrentRoom()
    {
        return currentRoom;
    }

    public void UnlockAllDoors()
    {
        currentRoom.transform.Find("Door South").GetComponent<Door>().IsLocked = false;
        currentRoom.transform.Find("Door West").GetComponent<Door>().IsLocked = false;
        currentRoom.transform.Find("Door North").GetComponent<Door>().IsLocked = false;
        currentRoom.transform.Find("Door East").GetComponent<Door>().IsLocked = false;
    }

    public void LockAllDoors()
    {
        currentRoom.transform.Find("Door South").GetComponent<Door>().IsLocked = true;
        currentRoom.transform.Find("Door West").GetComponent<Door>().IsLocked = true;
        currentRoom.transform.Find("Door North").GetComponent<Door>().IsLocked = true;
        currentRoom.transform.Find("Door East").GetComponent<Door>().IsLocked = true;
    }

    public void ChangeRoom(Door door)
    {
        if (door.IsLocked)
        {
            SoundManager.instance.PlayDoorLockedSfx();
            return;
        }
        else
        {
            StartCoroutine(ChangeRoomCoroutineDemoTwo(door));
        }
    }

    IEnumerator ChangeRoomCoroutineDemoOne(Door door)
    {
        SoundManager.instance.PlayDoorOpenSfx();
        panelBlackout.SetActive(true);

        yield return new WaitForSeconds(roomTransitionTime);

        Destroy(currentRoom);
        currentRoom = Instantiate(door.RoomConnected, new Vector3(0, 0, 0), Quaternion.identity);
        GameManager.instance.GetPlayer().transform.position = RoomEnterPositionDemoOne(door);

        panelBlackout.SetActive(false);
    }

    IEnumerator ChangeRoomCoroutineDemoTwo(Door door)
    {
        if (door.IsExit) // Check if exit door and conditions for exits
        {
            if (door.DoorLocation == Door.Location.East) // Room alignment: play exit
            {
                SoundManager.instance.PlayDoorOpenSfx();
                GameManager.instance.GameOver();
                yield break;
            }
            else if (door.DoorLocation == Door.Location.South) // Room alignment: play trigger
            {
                if (!isRotationSwitchSpawned)
                {
                    isRotationSwitchSpawned = true;
                    SoundManager.instance.PlayPartTwoBgm();
                    EventManager.instance.SpawnRotationSwitches();
                }

                SoundManager.instance.PlayDoorLockedSfx();
                yield break;
            }
        }

        Room currentRoomScript = currentRoom.GetComponent<Room>();
        GameObject newRoom;

        if (door.DoorLocation == Door.Location.North)
        {
            newRoom = currentRoomScript.NorthRoom;
        }
        else if (door.DoorLocation == Door.Location.West)
        {
            newRoom = currentRoomScript.WestRoom;
        }
        else if (door.DoorLocation == Door.Location.South)
        {
            newRoom = currentRoomScript.SouthRoom;
        }
        else //(door.DoorLocation == Door.Location.East)
        {
            newRoom = currentRoomScript.EastRoom;
        }

        if (newRoom == null || !CheckDoorExists(newRoom, OppositeLocation(door.DoorLocation)))
        {
            SoundManager.instance.PlayDoorLockedSfx();
            yield return null;
        }
        else
        {
            SoundManager.instance.PlayDoorOpenSfx();
            panelBlackout.SetActive(true);

            GameManager.instance.GetPlayer().transform.position = RoomEnterPositionDemoTwo(newRoom, OppositeLocation(door.DoorLocation));
            GameManager.instance.MoveCamera(newRoom.transform.position);
            currentRoom = newRoom;

            yield return new WaitForSeconds(roomTransitionTime);
            panelBlackout.SetActive(false);
        }

        // TODO: ONLY FOR DEMO PURPOSES
        if (currentRoom.name == "Room (3)")
        {
            SoundManager.instance.FadeMuteBgm();
        }
    }

    private Vector3 RoomEnterPositionDemoOne(Door door)
    {
        // TODO: ONLY FOR DEMO PURPOSES
        if (currentRoom.name == "Room 7(Clone)")
        {
            return new Vector3(0, 0, 0);
        }

        if (door.DoorLocation == Door.Location.North)
        {
            return door.RoomConnected.transform.Find("Door South").transform.position;
        }
        else if (door.DoorLocation == Door.Location.West)
        {
            return door.RoomConnected.transform.Find("Door East").transform.position;
        }
        else if (door.DoorLocation == Door.Location.South)
        {
            return door.RoomConnected.transform.Find("Door North").transform.position;
        }
        else //(door.DoorLocation == Door.Location.East)
        {
            return door.RoomConnected.transform.Find("Door West").transform.position;
        }
    }

    private Vector3 RoomEnterPositionDemoTwo(GameObject room, Door.Location doorLocation)
    {
        foreach (Transform child in room.transform)
        {
            if (child.tag == "Door")
            {
                Door door = child.GetComponent<Door>();
                if (door.DoorLocation == doorLocation)
                {
                    return child.position;
                }
            }
        }

        return room.transform.position;
    }

    public void RotateCurrentRoom()
    {
        currentRoom.GetComponent<Room>().RotateRoomCounterclockwise();
    }

    public void RotateConnectedRoom()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateConnectedRoomCoroutine());
        }
    }

    public void ResetRooms()
    {
        foreach (Transform child in rooms.transform)
        {
            child.GetComponent<Room>().ResetRoom();
        }
    }

    IEnumerator RotateConnectedRoomCoroutine()
    {
        isRotating = true;
        SoundManager.instance.PlayRotationSfx(5 / Vector3.Distance(GameManager.instance.GetPlayer().transform.position, currentRoom.GetComponent<Room>().ConnectedRoom.transform.position));
        currentRoom.GetComponent<Room>().ConnectedRoom.GetComponent<Room>().RotateRoomCounterclockwise();
        yield return new WaitForSeconds(3f);
        isRotating = false;
    }

    private bool CheckDoorExists(GameObject room, Door.Location doorLocation)
    {
        foreach (Transform child in room.transform)
        {
            if (child.tag == "Door")
            {
                Door door = child.GetComponent<Door>();
                if (door.DoorLocation == doorLocation)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private Door.Location OppositeLocation(Door.Location location)
    {
        if (location == Door.Location.North)
        {
            return Door.Location.South;
        }
        else if (location == Door.Location.West)
        {
            return Door.Location.East;
        }
        else if (location == Door.Location.South)
        {
            return Door.Location.North;
        }
        else //(location == Door.Location.East)
        {
            return Door.Location.West;
        }
    }
}
