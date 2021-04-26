using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 02/22/2021
/// <summary>
/// Class that handles the UI for the Win Lose states
/// </summary>
public class WinLoseUI : SingletonPattern<WinLoseUI>
{

    [HideInInspector] public bool won;
    [HideInInspector] public bool lost;
    [HideInInspector] public bool bossDead;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        bossDead = false;
    }

    /// Author: JT Esmond
    /// Date: 02/22/2021
    /// <summary>
    /// Call when the player defeats the boss
    /// </summary>
    public void YouWin()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
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
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        Time.timeScale = 0f;
        lost = true;
    }
}
