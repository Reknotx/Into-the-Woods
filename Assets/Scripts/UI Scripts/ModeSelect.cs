using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the functions for changing the game mode from a main menu.
/// The variables set here are passed into Unity's PlayerPrefs and will be read later.
/// THIS THING NEEDS PROPER UI AND STUFF LIKE BUTTONS.
/// </summary>
public class ModeSelect : MonoBehaviour
{

    public int enterRows;
    public int enterColumns;
    public bool enterUseSeed;
    public int enterSeed;

    public int playerDamageMultiplier;

    public void Toggle5x5MapMenu()
    {
        // Toggle behavior.
        if (enterRows == 5)
        {
            enterRows = 4;
            enterColumns = 4;
        }
        else
        {
            enterRows = 5;
            enterColumns = 5;
        }

        // Save the result to PlayerPrefs!
        PlayerPrefs.SetInt("PWorldRows", enterRows);
        PlayerPrefs.SetInt("PWorldColumns", enterColumns);
        print("World size is now " + enterRows + "x" + enterColumns + "!");

    }

    public void ToggleDoubleDamageMenu()
    {
        // Toggle behavior.
        if (playerDamageMultiplier == 2)
        {
            playerDamageMultiplier = 1;
        }
        else
        {
            playerDamageMultiplier = 2;
        }

        // Save the result to PlayerPrefs!
        //PlayerPrefs.SetInt("playerDamageMultiplier", playerDamageMultiplier);
        PlayerPrefs.SetInt(PrefTags.DmgMulti, playerDamageMultiplier);
        print("Player damage multiplier set to x" + playerDamageMultiplier + "!");
    }

    /// <summary>
    /// Sets the custom seed, if used.
    /// In order for this to work, this would need a function that calls it
    /// after typing into a fill-in field, or something like that.
    /// There's already code in the WorldGen that reads from PlayerPrefs if this
    /// is edited, so you'd just need to call this after editing enterUseSeed and enterSeed.
    /// </summary>
    void SaveSeed()
    {
        if (enterUseSeed)
        {
            PlayerPrefs.SetInt("useSeed", 1);
            PlayerPrefs.SetInt("seed", enterSeed);
            print("Seed set to " + enterSeed);
        }
        else
        {
            PlayerPrefs.SetInt("useSeed", 0);
            print("Seed will be random.");
        }

        
    }

}