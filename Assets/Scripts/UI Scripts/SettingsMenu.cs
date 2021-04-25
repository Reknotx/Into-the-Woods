using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private List<int> width = new List<int>();
    private List<int> height = new List<int>();

    // Start is called before the first frame update
    void Start()
    {

        #region resolution and fullscreen mode stuff;
        var resDropdown = transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Dropdown>();
        resDropdown.options.Clear();

        var fullscreenDropdown = transform.GetChild(3).gameObject.GetComponent<TMPro.TMP_Dropdown>();
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Fullscreen" });
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Windowed Fullscreen" });
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Maximized Windowed" });
        fullscreenDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = "Windowed" });


        Resolution[] resolutions = Screen.resolutions;

        resDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = (Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString()) });
        width.Add(Screen.currentResolution.width);
        height.Add(Screen.currentResolution.height);
        foreach (var res in resolutions)
        {
            resDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = (res.width.ToString() + "x" + res.height.ToString()) });
            width.Add(res.width);
            height.Add(res.height);
        }
        resDropdown.value = 0;
        fullscreenDropdown.value = 0;
        DropdownSelected(resDropdown, fullscreenDropdown);

        resDropdown.onValueChanged.AddListener(delegate { DropdownSelected(resDropdown, fullscreenDropdown); });
        fullscreenDropdown.onValueChanged.AddListener(delegate { DropdownSelected(resDropdown, fullscreenDropdown); });
        #endregion
    }

    private void DropdownSelected(TMPro.TMP_Dropdown resDropdown, TMPro.TMP_Dropdown fullscreenDropdown)
    {
        int index = resDropdown.value;
        int index2 = fullscreenDropdown.value;

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