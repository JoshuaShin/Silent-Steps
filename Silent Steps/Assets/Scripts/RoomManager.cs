using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelBlackout;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject currentRoom;
    [SerializeField]
    private float roomTransitionTime = 1f;

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
            SoundManager.instance.PlayDoorOpenSfx();
            StartCoroutine(ChangeRoomCoroutine(door));
        }
    }

    IEnumerator ChangeRoomCoroutine(Door door)
    {
        panelBlackout.SetActive(true);

        yield return new WaitForSeconds(roomTransitionTime);

        Destroy(currentRoom);
        currentRoom = Instantiate(door.RoomConnected, new Vector3(0, 0, 0), Quaternion.identity);
        player.transform.position = RoomEnterPosition(door);

        panelBlackout.SetActive(false);
    }

    private Vector3 RoomEnterPosition(Door door)
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
        else if (door.DoorLocation == Door.Location.East)
        {
            return door.RoomConnected.transform.Find("Door West").transform.position;
        }
        else if (door.DoorLocation == Door.Location.South)
        {
            return door.RoomConnected.transform.Find("Door North").transform.position;
        }
        else //(door.DoorLocation == Door.Location.West)
        {
            return door.RoomConnected.transform.Find("Door East").transform.position;
        }
    }
}
