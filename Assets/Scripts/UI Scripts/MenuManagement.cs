using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManagement : MonoBehaviour
{

    public string surveyLink;
    public string bugReportLink;
    private int randomLocation;
    public AudioMixer mixer;
    public bool end;

    private bool hasSet;

    private int defaultwidth;
    private int defaultheight;
    private int currentWidth;
    private int currentHeight;
    private int currentScreenMode;

    private void Start()
    {

        ///Limits the game's refresh rate to our current monitors refresh rate to 
        ///avoid having the game eating up our system.
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        if(defaultwidth == 0 && defaultheight == 0)
        {
            defaultwidth = Screen.currentResolution.width;
            defaultheight = Screen.currentResolution.height;
            
        }
        currentWidth = PlayerPrefs.GetInt("currentWidth", defaultwidth);
        currentHeight = PlayerPrefs.GetInt("currentHeight", defaultheight);
        currentScreenMode = PlayerPrefs.GetInt("ScreenMode", 0);

        if(currentScreenMode == 0)
        {
            return;
        }
        else if (currentScreenMode == 1)
        {
            Screen.SetResolution(currentWidth, currentHeight, FullScreenMode.FullScreenWindow);
            hasSet = false;
        }
        else if (currentScreenMode == 2)
        {
            Screen.SetResolution(currentWidth, currentHeight, FullScreenMode.MaximizedWindow);
            hasSet = false;
        }
        else if (currentScreenMode == 3)
        {
            Screen.SetResolution(currentWidth, currentHeight, FullScreenMode.Windowed);
            hasSet = false;
        }
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
        if (end)
        {
            WinLoseUI.Instance.won = false;
            WinLoseUI.Instance.lost = false;
        }
    }

    /// Author: JT Esmond
    /// Date: 2/21/2021
    /// <summary>
    /// function for any Settings Button
    /// </summary>
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
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
