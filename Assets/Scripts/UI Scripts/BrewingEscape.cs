using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingEscape : MonoBehaviour
{

    public static bool GameIsPaused;
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused &&  !PauseMenu.GameIsPaused && !TurnOn.GameIsPaused && !PopUpManager.GameIsPaused)
            {
                GameIsPaused = false;
                gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            else return;
        }
    }
}
