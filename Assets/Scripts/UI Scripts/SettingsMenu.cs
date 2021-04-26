using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 4/24/2021
/// <summary>
/// the class that handles all of the settings menu.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    //lists for the width and the height
    private List<int> width = new List<int>();
    private List<int> height = new List<int>();

    private int defaultWidth;
    private int defaultHeight;
    private int savedWidth;
    private int savedHeight;

    // Start is called before the first frame update
    void Start()
    {

        #region resolution and fullscreen mode stuff;
        //variable for the resolution dropdown, that contains all of the dropdown options
        var resDropdown = transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Dropdown>();
        resDropdown.options.Clear();


        // variable for the screen mode dropdown, that contains all of the screen mode dropdown options
        var fullscreenDropdown = transform.GetChild(3).gameObject.GetComponent<TMPro.TMP_Dropdown>();

        //adds in all of the values for the screen mode dropdown options
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Fullscreen" });
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Windowed Fullscreen" });
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Maximized Windowed" });
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Windowed" });

        //gets all of the possible resolutions the players monitor supports
        Resolution[] resolutions = Screen.resolutions;

        defaultWidth = Screen.currentResolution.width;
        defaultHeight = Screen.currentResolution.height;
        //adds the current resolution to the top of the dropdown menu, and treats it as the default
        resDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = (PlayerPrefs.GetInt("currentWidth", defaultWidth) + "x" + PlayerPrefs.GetInt("currentHeight", defaultHeight)) });
        width.Add(PlayerPrefs.GetInt("currentWidth", defaultWidth));
        height.Add(PlayerPrefs.GetInt("currentHeight", defaultHeight));
        
        //adds all of the possible resolutions to the dropdown menu, along with adding the width and heights for each resolution to their own lists
        foreach (var res in resolutions)
        {
            if (res.width >= 1024 && res.height >= 768)
            {
                resDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = (res.width.ToString() + "x" + res.height.ToString()) });
                width.Add(res.width);
                height.Add(res.height);
            }
            else return;
        }
        //sets the values for both dropdowns to 0 so the player starts at the default

        DropdownSelected(resDropdown, fullscreenDropdown);

        //listener for the resolution and window mode dropdowns
        resDropdown.onValueChanged.AddListener(delegate { DropdownSelected(resDropdown, fullscreenDropdown); });
        fullscreenDropdown.onValueChanged.AddListener(delegate { DropdownSelected(resDropdown, fullscreenDropdown); });
        #endregion
    }

    /// Author: JT Esmond
    /// Date: 4/24/2021
    /// <summary>
    /// function that handles setting the resolution and the screen mode
    /// </summary>
    /// <param name="resDropdown"> reference to the resolution dropdown </param>
    /// <param name="fullscreenDropdown"> reference to the screen mode drop down </param>
    private void DropdownSelected(TMPro.TMP_Dropdown resDropdown, TMPro.TMP_Dropdown fullscreenDropdown)
    {
        //integers to hold the location value in the lists for resolution and screen mode
        int index = resDropdown.value;
        int index2 = fullscreenDropdown.value;

        //checks to see which screen mode the player has selected, then sets the resolution based off of the resolution that is selected
        if (index2 == 0)
        {
            Screen.SetResolution(width[index], height[index], FullScreenMode.ExclusiveFullScreen);
        }
        else if (index2 == 1)
        {
            Screen.SetResolution(width[index], height[index], FullScreenMode.FullScreenWindow);
        }
        else if (index2 == 2)
        {
            Screen.SetResolution(width[index], height[index], FullScreenMode.MaximizedWindow);
        }
        else if (index2 == 3)
        {
            Screen.SetResolution(width[index], height[index], FullScreenMode.Windowed);
        }
    }

}