using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAssetSwap : MonoBehaviour
{
    // Different asset themes for this specific object prefab.
    /*
    public GameObject evergreen;
    public GameObject bio;
    public GameObject mushroom;
    public GameObject flower;
    */

    private int envThemeID; // What environment theme will we use?

    private void Awake()
    {
        envThemeID = PlayerPrefs.GetInt(PrefTags.EnvironmentTheme, 0);

        // ErrorCheck();

        AssetSwap();
    }

    private void ErrorCheck()
    {
        if (envThemeID == 1 && transform.GetChild(1).gameObject == null) 
        {
                envThemeID = 0;
                print(this.gameObject + "'s 'bio' asset was not assigned.");
        }
        else
        if (envThemeID == 2 && transform.GetChild(2).gameObject == null)
        {
                envThemeID = 0;
                print(this.gameObject + "'s 'mushroom' asset was not assigned.");
        }
        else
        if (envThemeID == 3 && transform.GetChild(3).gameObject == null)
        {
                envThemeID = 0;
                print(this.gameObject + "'s 'flower' asset was not assigned.");
        }
    }

    private void AssetSwap()
    {
        int childCount = 0;
        // Turn off all children.
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            childCount++;
        }

        // Turn on just the theme ID one.
        if (envThemeID <= childCount)
        {
            transform.GetChild(envThemeID).gameObject.SetActive(true);
        }
        else // Error message and put on default.
        {
            print(this.gameObject + "'s asset swap for theme " + envThemeID + " did not exist.");
            if (transform.GetChild(0).gameObject != null)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
