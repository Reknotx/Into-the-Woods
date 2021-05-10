using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingEscape : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenu.GameIsPaused)
            {
                StartCoroutine(Delay());
                gameObject.SetActive(false);
                Time.timeScale = 1f;

            }
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        PauseMenu.GameIsPaused = false;
    }
}
