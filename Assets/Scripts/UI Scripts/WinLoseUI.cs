using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 02/22/2021
/// <summary>
/// Class that handles the UI for the Win Lose states
/// </summary>
public class WinLoseUI : MonoBehaviour
{
    public static WinLoseUI Instance;

    private GameObject youWin;
    private GameObject youLose;

    [HideInInspector] public bool won;
    [HideInInspector] public bool lost;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        youWin = transform.GetChild(0).gameObject;
        youLose = transform.GetChild(1).gameObject;
    }

    /// Author: JT Esmond
    /// Date: 02/22/2021
    /// <summary>
    /// Call when the player defeats the boss
    /// </summary>
    public void YouWin()
    {
        youWin.SetActive(true);
        Time.timeScale = 0f;
        won = true;
    }

    /// Author: JT Esmond
    /// Date: 02/22/2021
    /// <summary>
    /// Call whenever the players health runs out, or the time runs out.
    /// </summary>
    public void YouLose()
    {
        youLose.SetActive(true);
        Time.timeScale = 0f;
        lost = true;
    }
}
