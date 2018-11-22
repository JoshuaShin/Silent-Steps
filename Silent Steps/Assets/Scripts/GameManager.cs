using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject checkpoint;
    [SerializeField]
    private GameObject panelLoading;
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private Camera mainCamera;

    public static GameManager instance = null;

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

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !panelLoading.activeInHierarchy)
        {
            if (options.activeInHierarchy)
            {
                ResumeGame();
                options.SetActive(false);
            }
            else
            {
                PauseGame();
                options.SetActive(true);
            }
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void MoveCamera(Vector3 position)
    {
        mainCamera.transform.position = position + new Vector3(0, 0, -10);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        SoundManager.instance.PlayEscapeBgm();
        SoundManager.instance.PlayFootstepsSfx(14);

        panelGameOver.SetActive(true);
        panelGameOver.GetComponent<Image>().DOFade(1, 3);
    }

    public void LoadCheckpoint()
    {
        ResumeGame();
        RoomManager.instance.ResetRooms();
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene("Room");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
