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

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// function that turns on the different pop ups depending on the item
    /// </summary>
    public void PopUp(Collectable collected)
    {
        Time.timeScale = 0f;

        if(collected is Totem)
        {
            gameObject.transform.Find("TotemPopUp").gameObject.SetActive(true);
        }
        else if (collected is LuckyPenny)
        {
            gameObject.transform.Find("LuckyPennyPopUp").gameObject.SetActive(true);
        }
        else if (collected is AttackCandy)
        {
            gameObject.transform.Find("AttackCandyPopUp").gameObject.SetActive(true);
        }
        else if (collected is BalloonBouquet)
        {
            gameObject.transform.Find("BalloonBouquetPopUp").gameObject.SetActive(true);
        }
        else if (collected is NightOwlToken)
        {
            gameObject.transform.Find("NightOwlTokenPopUp").gameObject.SetActive(true);
        }
        else if (collected is TwoPeas)
        {
            gameObject.transform.Find("TwoPeasInAPodPopUp").gameObject.SetActive(true);
        }
        else if (collected is Compass)
        {
            gameObject.transform.Find("CompassPopUp").gameObject.SetActive(true);
        }
        else if (collected is Avocado)
        {
            gameObject.transform.Find("AvocadoPopUp").gameObject.SetActive(true);
        }
    }

    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// functions for the different resume buttons.
    /// </summary>
    #region Resume Buttons
    public void TotemResume()
    {
        gameObject.transform.Find("TotemPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LuckyPennyResume()
    {
        gameObject.transform.Find("LuckyPennyPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void AttackCandyResume()
    {
        gameObject.transform.Find("AttackCandyPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BalloonBouquetResume()
    {
        gameObject.transform.Find("BalloonBouquetPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void NightOwlResume()
    {
        gameObject.transform.Find("NightOwlTokenPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void TwoPeasResume()
    {
        gameObject.transform.Find("TwoPeasInAPodPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void CompassResume()
    {
        gameObject.transform.Find("CompassPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void AvocadoResume()
    {
        gameObject.transform.Find("AvocadoPopUp").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion
}
