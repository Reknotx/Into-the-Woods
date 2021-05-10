using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOn : MonoBehaviour
{
    public List<GameObject> children = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.GameIsPaused)
            {
                StartCoroutine(Delay());
                foreach (GameObject child in children)
                {
                    gameObject.SetActive(false);
                }
                Time.timeScale = 1f;

            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        PauseMenu.GameIsPaused = false;
    }
    public void off()
    {
        if (PauseMenu.GameIsPaused)
        {
            foreach (GameObject child in children)
            {
                gameObject.SetActive(false);
            }
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;
        }
    }

    public void On()
    {
        if (PauseMenu.GameIsPaused)
        {
            foreach (GameObject child in children)
            {
                gameObject.SetActive(true);
            }
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = true;
        }
    }
}
