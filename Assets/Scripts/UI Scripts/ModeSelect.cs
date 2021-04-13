using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class contains the functions for changing the game mode from a main menu.
/// The variables set here are passed into Unity's PlayerPrefs and will be read later.
/// THIS THING NEEDS PROPER UI AND STUFF LIKE BUTTONS.
/// </summary>
public class ModeSelect : MonoBehaviour
{
    // Variables to input before saving.
    public int enterRows;
    public int enterColumns;
    public bool enterUseSeed;
    public int enterSeed;

    public int playerDamageMultiplier;

    // Objects to toggle on/off for UI.
    public GameObject mapCheckmark;
    public GameObject dmgCheckmark;

    private void Awake()
    {
        // Awake will ensure everything is properly enabled/disabled correctly.
        if (enterRows == 5)
        {
            mapCheckmark.SetActive(true);
        }
        else
        {
            mapCheckmark.SetActive(false);
            enterRows = 4;
            enterColumns = 4;
        }
        print("World size is " + enterRows + "x" + enterColumns + ".");
        PlayerPrefs.SetInt(PrefTags.PWorldRows, enterRows);
        PlayerPrefs.SetInt(PrefTags.PWorldColumns, enterColumns);

        if (playerDamageMultiplier == 2)
        {
            dmgCheckmark.SetActive(true);
        }
        else
        {
            playerDamageMultiplier = 1;
            dmgCheckmark.SetActive(false);
        }
        print("Player damage multiplier is x" + playerDamageMultiplier + ".");
        PlayerPrefs.SetInt(PrefTags.DmgMulti, playerDamageMultiplier);

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame");
    }

    public void ToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Toggle5x5MapMenu()
    {
        // Toggle behavior.
        if (enterRows == 5)
        {
            enterRows = 4;
            enterColumns = 4;
            mapCheckmark.SetActive(false);
        }
        else
        {
            enterRows = 5;
            enterColumns = 5;
            mapCheckmark.SetActive(true);
        }

        // Save the result to PlayerPrefs!
        PlayerPrefs.SetInt(PrefTags.PWorldRows, enterRows);
        PlayerPrefs.SetInt(PrefTags.PWorldColumns, enterColumns);
        print("World size is now " + enterRows + "x" + enterColumns + "!");

    }

    public void ToggleDoubleDamageMenu()
    {
        // Toggle behavior.
        if (playerDamageMultiplier == 2)
        {
            playerDamageMultiplier = 1;
            dmgCheckmark.SetActive(false);
        }
        else
        {
            playerDamageMultiplier = 2;
            dmgCheckmark.SetActive(true);
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
            PlayerPrefs.SetInt(PrefTags.UseSeed, 1);
            PlayerPrefs.SetInt(PrefTags.Seed, enterSeed);
            print("Seed set to " + enterSeed);
        }
        else
        {
            PlayerPrefs.SetInt(PrefTags.UseSeed, 0);
            print("Seed will be random.");
        }

        
    }

}