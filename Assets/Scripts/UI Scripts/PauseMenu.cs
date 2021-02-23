using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 02/22/2021
/// <summary>
/// Pause Menu class that holds the functions for the pause menu, and also runs the pause menu functions
/// </summary>
public class PauseMenu : MonoBehaviour
{
    //static bool for the when the game is paused. Made it static in case it needs to be access in other scripts.
    public static bool GameIsPaused = false;

    //game object reference for the pause menu ui holder
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// Author: JT Esmond
    /// Date: 02/22/2021
    /// <summary>
    /// function that unpauses the game
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// Author: JT Esmond
    /// Date: 02/22/2021
    /// <summary>
    /// function that pauses the game
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
