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
    private float roomTransitionTime = 1f;

    private GameObject currentRoom;
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

        currentRoom = GameObject.Find("Room"); // TODO: Temporary solution; move initializing the first room to GameManager.
    }

    public GameObject GetCurrentRoom()
    {
        return currentRoom;
    }

    public void ChangeRoom(Door door)
    {
        StartCoroutine(ChangeRoomCoroutine(door));
    }

    IEnumerator ChangeRoomCoroutine(Door door)
    {
        panelBlackout.SetActive(true);

        Destroy(currentRoom);
        currentRoom = Instantiate(door.RoomConnected, new Vector3(0, 0, 0), Quaternion.identity);
        player.transform.position = RoomEnterPosition(door);

        yield return new WaitForSeconds(roomTransitionTime);

        panelBlackout.SetActive(false);
    }

    private Vector3 RoomEnterPosition(Door door)
    {
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
