using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOn : MonoBehaviour
{
    public List<GameObject> children = new List<GameObject>();

    public static bool GameIsPaused;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsPaused)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !PauseMenu.GameIsPaused && !BrewingEscape.GameIsPaused && !PopUpManager.GameIsPaused)
            {
                GameIsPaused = false;
                foreach (GameObject child in children)
                {
                    gameObject.SetActive(false);
                }
                Time.timeScale = 1f;

            }
            else return;
        }
    }

    public void off()
    {
        if (GameIsPaused)
        {
            GameIsPaused = false;
            PauseMenu.GameIsPaused = true;
            foreach (GameObject child in children)
            {
                gameObject.SetActive(false);
            }
            Time.timeScale = 1f;
            
        }
    }

    public void On()
    {
        if (!GameIsPaused)
        {
            Time.timeScale = 0f;
            GameIsPaused = true;
            foreach (GameObject child in children)
            {
                gameObject.SetActive(true);
            }

            
        }
    }
}
