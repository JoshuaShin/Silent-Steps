using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject rooms;
    [SerializeField]
    private GameObject rotationSwitch;

    [SerializeField]
    private GameObject bunchOfFootsteps;
    [SerializeField]
    private GameObject rightBehindYou;
    [SerializeField]
    private GameObject surroundedByFootsteps;
    [SerializeField]
    private GameObject fullOfFootsteps;
    [SerializeField]
    private GameObject spawnDoors;

    public static EventManager instance = null;

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

    public void SpawnRotationSwitches()
    {
        foreach (Transform child in rooms.transform)
        {
            Instantiate(rotationSwitch, child);
        }
    }

    public void PlayBunchOfFootsteps()
    {
        Instantiate(bunchOfFootsteps, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void PlayRightBehindYou()
    {
        RoomManager.instance.UnlockAllDoors();
        SoundManager.instance.PlayScreamSfx();
        Instantiate(rightBehindYou, GameManager.instance.GetPlayer().transform.position, GameManager.instance.GetPlayer().transform.rotation);
    }

    public void PlaySurroundedByFootsteps()
    {
        SoundManager.instance.PlayEvilSfx();
        Instantiate(surroundedByFootsteps, GameManager.instance.GetPlayer().transform.position, GameManager.instance.GetPlayer().transform.rotation);
    }

    public void PlayFullOfFootsteps()
    {
        RoomManager.instance.UnlockAllDoors();
        SoundManager.instance.PlayEvilSfx();
        Instantiate(fullOfFootsteps, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void PlaySpawnDoors()
    {
        SoundManager.instance.PlayEvilMachineSfx();
        Instantiate(spawnDoors, RoomManager.instance.GetCurrentRoom().transform);
    }
}
