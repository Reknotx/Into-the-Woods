using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 3/8/2021
/// <summary>
/// class that holds the functions for the different pop up descriptions for items
/// </summary>
public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    // Booleans for that are used to determine wether or not the different items have been picked up before
    private bool totem;
    private bool luckyPenny;
    private bool attackCandy;
    private bool balloonBouquet;
    private bool nightOwl;
    private bool twoPeas;
    private bool compass;
    private bool avocado;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            TotemPopUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            LuckyPennyPopUp();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AttackCandyPopUp();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            BalloonBouquetPopUp();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            NightOwlPopUp();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            TwoPeasPopUp();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            CompassPopUp();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            AvocadoPopUp();
        }
    }

    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// these functions control the different item pop ups. They pause the game time, turn on the specific pop up for the item
    /// and sets the specific boolean to true so that pop up wont turn back on
    /// </summary>
    #region PopUp Functions
    public void TotemPopUp()
    {
        if(!totem)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            totem = true;
        }
    }
    public void LuckyPennyPopUp()
    {
        if (!luckyPenny)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            luckyPenny = true;
        }
    }
    public void AttackCandyPopUp()
    {
        if (!attackCandy)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            attackCandy = true;
        }
    }
    public void BalloonBouquetPopUp()
    {
        if (!balloonBouquet)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            balloonBouquet = true;
        }
    }
    public void NightOwlPopUp()
    {
        if (!nightOwl)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            nightOwl = true;
        }
    }
    public void TwoPeasPopUp()
    {
        if (!twoPeas)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
            twoPeas = true;
        }
    }
    public void CompassPopUp()
    {
        if (!compass)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(6).gameObject.SetActive(true);
            compass = true;
        }
    }
    public void AvocadoPopUp()
    {
        if (!avocado)
        {
            Time.timeScale = 0f;
            gameObject.transform.GetChild(7).gameObject.SetActive(true);
            avocado = true;
        }
    }
    #endregion

    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// functions for the different resume buttons.
    /// </summary>
    #region Resume Buttons
    public void TotemResume()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LuckyPennyResume()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void AttackCandyResume()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BalloonBouquetResume()
    {
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void NightOwlResume()
    {
        gameObject.transform.GetChild(4).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void TwoPeasResume()
    {
        gameObject.transform.GetChild(5).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void CompassResume()
    {
        gameObject.transform.GetChild(6).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void AvocadoResume()
    {
        gameObject.transform.GetChild(7).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion
}
