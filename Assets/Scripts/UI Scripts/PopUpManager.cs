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

    public List<ScriptablePopUp> PopUps = new List<ScriptablePopUp>();

    public List<SpellScroll> SpellScrolls = new List<SpellScroll>();
    public List<PotionRecipe> PotionRecipes = new List<PotionRecipe>();


    private string test;
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

    }

    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// function that turns on the different pop ups depending on the item
    /// </summary>
    public void PopUpOn(Interactable collected)
    {
        Time.timeScale = 0f;
        transform.GetChild(0).gameObject.SetActive(true);
        foreach (ScriptablePopUp popUp in PopUps)
        {
            if(collected is LearnSpell)
            {
                    if (collected.GetType() == popUp.ObjRef.GetComponent<Interactable>().GetType())
                    {
                        PopUp.Instance.UpdateSpellInfo();
                    }
            }
            else if(collected is LearnPotion)
            {
                foreach (PotionRecipe potionRecipe in PotionRecipes)
                {
                    if (collected.GetType() == popUp.ObjRef.GetComponent<Interactable>().GetType())
                    {
                        Debug.Log("Hi");
                    }
                }
            }
            else if(collected.GetType() == popUp.ObjRef.GetComponent<Interactable>().GetType())
            {
                PopUp.Instance.UpdateCollectableInfo(popUp);
            }
        }
    }

    /// Author: JT Esmond
    /// Date: 3/8/2021
    /// <summary>
    /// function that turns off the pop ups
    /// </summary>
    #region Resume Buttons
    public void PopUpResume()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion
}
