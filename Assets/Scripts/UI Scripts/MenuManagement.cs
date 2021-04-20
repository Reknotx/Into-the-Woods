using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{

    public string surveyLink;
    public string bugReportLink;
    private int randomLocation;

    private void Start()
    {
        ///Limits the game's refresh rate to our current monitors refresh rate to 
        ///avoid having the game eating up our system.
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any Play Game button button.
    /// </summary>
    public void PlayGame()
    {
        // SceneManager.LoadScene("ModeSelect");
        SceneManager.LoadScene("ModeSelect_2");
        // Edited to redirect to new mode select menu. -Paul.
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any How to play button. HowToPlay scene needs to be 1 in the build Index
    /// </summary>
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any main menu button. MainMenu scene needs to be 0 in the build Index
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        WinLoseUI.Instance.won = false;
        WinLoseUI.Instance.lost = false;
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any Shop button. Shop scene needs to be 3 in the build Index
    /// </summary>
    public void Shop()
    {
        //Opens shop screen
        //SceneManager.LoadScene("Shop");
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any Exit button.
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any Survey Button
    /// </summary>
    public void Survey()
    {
        Application.OpenURL(surveyLink);
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any Bug Report Button
    /// </summary>
    public void BugReport()
    {
        Application.OpenURL(bugReportLink);
    }
}
