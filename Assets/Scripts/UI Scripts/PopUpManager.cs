using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 3/8/2021
/// <summary>
/// class that holds the functions for the different pop up descriptions for items
/// </summary>
public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    public List<GameObject> PopUps = new List<GameObject>();

    private int listLocation;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        foreach(Transform popUp in this.transform)
        {
            if(popUp.gameObject.CompareTag("PopUp"))
            {
                PopUps.Add(popUp.gameObject);
            }
        }
    }

    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// function that turns on the different pop ups depending on the item
    /// </summary>
    public void PopUp(Collectable collected)
    {
        Time.timeScale = 0f;

        if (collected is Totem)
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
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void SpellPopUp()
    {

    }
    public void PotionPopUp()
    {
        
    }


    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// function that turns off the pop ups
    /// </summary>
    #region Resume Buttons
    public void PopUpResume()
    {
        for(listLocation = 0; listLocation < PopUps.Count; listLocation++)
        {
            PopUps[listLocation].SetActive(false);
        }
        Time.timeScale = 1f;
    }
    #endregion
}
