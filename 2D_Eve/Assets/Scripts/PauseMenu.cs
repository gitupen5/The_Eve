using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool IsPaused = false;

    /*public void pauseGame()
    {
        if (IsPaused)
        {
            Time.timeScale = 1;
            IsPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            IsPaused = true;
        }
    }*/

    //public GameObject pauseMenu;

    /*void Start()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
            Debug.LogError("Game Pause");
           // IsPaused = !IsPaused;
        }
    }*/
    

    void pauseGame()
    {
        if (IsPaused)
        {
            Time.timeScale = 1;
            IsPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            IsPaused = true;
        }
        

    }
}
