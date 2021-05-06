using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is just a quick edit of EnvironmentalAssetSwap, specifically for materials only.
/// </summary>
public class EnvironmentalMaterialSwap : MonoBehaviour
{
    private int envThemeID; // What environment theme will we use?

    public List<Material> gmats;

    private void Awake()
    {
        envThemeID = PlayerPrefs.GetInt(PrefTags.EnvironmentTheme, 0);
        AssetSwap();
    }

    private void AssetSwap()
    {
        if (envThemeID <= gmats.Count) // Turn on just the theme ID one.
        {
            this.gameObject.GetComponent<MeshRenderer>().material = gmats[envThemeID];
        }
        else // Error message and put on default.
        {
            print(this.gameObject + "'s material swap for theme " + envThemeID + " did not exist.");
            if (transform.GetChild(0).gameObject != null)
            {
                // this.gameObject.GetComponent<MeshRenderer>().material = groundThemeMaterials[0];
                // Doing nothing is probably sufficient.
            }
        }
    }
}