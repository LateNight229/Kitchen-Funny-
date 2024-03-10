using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Reset()
    {
         pauseMenu = GameObject.Find("PausePanel");
    }
    private void Start()
    {
        if(pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PausePanel");
            Debug.LogWarning("Not Reset at PauseManager");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

     public  void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            // Resume game
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            SoundManager.instance.HandleBGMusic( On: true);
        }
        else
        {
            // Pause game
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            SoundManager.instance.HandleBGMusic(On: false);
        }
    }
}
