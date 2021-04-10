using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 02/22/2021
/// <summary>
/// Class that handles the UI for the Win Lose states
/// </summary>
public class WinLoseUI : SingletonPattern<WinLoseUI>
{
    private GameObject youWin;
    private GameObject youLose;

    [HideInInspector] public bool won;
    [HideInInspector] public bool lost;
    [HideInInspector] public bool bossDead;

    protected override void Awake()
    {
        base.Awake();

        youWin = transform.GetChild(0).gameObject;
        youLose = transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        bossDead = false;
        Time.timeScale = 1f;
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
