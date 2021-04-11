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

    public SpellScroll spellScrollValues;
    public List<ScriptablePopUp> PopUps = new List<ScriptablePopUp>();

    public List<SpellScroll> SpellScrolls = new List<SpellScroll>();
    public List<PotionRecipe> PotionRecipes = new List<PotionRecipe>();

    private int listLocation;
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
    /// function that turns on the different pop ups for the items
    /// </summary>
    public void PopUpOn(Interactable collected)
    {
        if (collected is AttackCandy || collected is Avocado || collected is BalloonBouquet || collected is Compass || collected is LuckyPenny || collected is NightOwlToken || collected is Totem || collected is TwoPeas)
        {
            Time.timeScale = 0f;
            transform.GetChild(0).gameObject.SetActive(true);

            foreach (ScriptablePopUp popUp in PopUps)
            {
                if (collected is AttackCandy || collected is Avocado || collected is BalloonBouquet || collected is Compass || collected is LuckyPenny || collected is NightOwlToken || collected is Totem || collected is TwoPeas)
                {
                    if (collected.GetType() == popUp.ObjRef.GetComponent<Interactable>().GetType())
                    {
                        PopUp.Instance.UpdateCollectableInfo(popUp);
                    }
                }
            }
        }         
        else
        {
            return;
        }
    }

    /// Author: JT Esmond
    /// Date: 4/10/2021
    /// <summary>
    /// function that turns on the different pop ups for the spell scrolls
    /// </summary>
    public void SpellPopUp(Interactable collected, SpellScroll spell)
    {
        Time.timeScale = 0f;
        transform.GetChild(0).gameObject.SetActive(true);
        foreach (ScriptablePopUp popUp in PopUps)
        {
            if (collected.GetType() == popUp.ObjRef.GetComponent<Interactable>().GetType())
            {
                //checks which spell is being picked up then displays the corresponding pop up
                if (spell == SpellScrolls[0])
                {
                    listLocation = 10;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
                else if (spell == SpellScrolls[1])
                {
                    listLocation = 11;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
                else if (spell == SpellScrolls[2])
                {
                    listLocation = 12;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
                else if (spell == SpellScrolls[3])
                {
                    listLocation = 13;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
            }
        }
    }

    /// Author: JT Esmond
    /// 4/10/2021
    /// <summary>
    /// Function that turns on the pop ups for the potion recipes
    /// </summary>
    public void PotionPopUp(Interactable collected, PotionRecipe potion)
    {
        Time.timeScale = 0f;
        transform.GetChild(0).gameObject.SetActive(true);
        foreach (ScriptablePopUp popUp in PopUps)
        {
            if(collected.GetType() == popUp.ObjRef.GetComponent<Interactable>().GetType())
            {
                //checks which potion recipe is being picked up then displays the corresponding pop up
                if(potion == PotionRecipes[0])
                {
                    listLocation = 5;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
                else if (potion == PotionRecipes[1])
                {
                    PopUp.Instance.UpdateInteractableInfo(5);
                }
                else if (potion == PotionRecipes[2])
                {
                    listLocation = 8;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
                else if (potion == PotionRecipes[3])
                {
                    listLocation = 6;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
                else if (potion == PotionRecipes[4])
                {
                    listLocation = 7;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
                else if (potion == PotionRecipes[5])
                {
                    listLocation = 9;
                    PopUp.Instance.UpdateInteractableInfo(listLocation);
                }
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
