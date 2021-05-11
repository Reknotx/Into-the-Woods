using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Good_End_Scene");
        won = true;
    }

    /// Author: JT Esmond
    /// Date: 02/22/2021
    /// <summary>
    /// Call whenever the players health runs out, or the time runs out.
    /// </summary>
    public void YouLose()
    {
        SceneManager.LoadScene("Bad_End_Scene");
        lost = true;
    }
}
