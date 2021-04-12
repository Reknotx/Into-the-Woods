using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUp : MonoBehaviour
{
    private int currentPage = 0;
    private int maxPages = 2;

    public GameObject TutorialUI;
    public TMPro.TMP_Text description;
    public TMPro.TMP_Text UIPageNumber;
    public RawImage videoImage;

    private void Awake()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        RefreshUIChoice();
    }

    #region ButtonFunctions
    public void ScrollRight()
    {
        currentPage++;
        if (currentPage > maxPages)
        {
            currentPage = 0;
        }
        RefreshUIChoice();
    } 

    public void ScrollLeft()
    {
        currentPage--;
        if (currentPage < 0)
        {
            currentPage = maxPages;
        }
        RefreshUIChoice();
    }

    public void CloseTutorial()
    {
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        TutorialUI.SetActive(false);
    }
    #endregion

    private void RefreshUIChoice()
    {
        UIPageNumber.text = ((currentPage + 1) + "/" + (maxPages + 1));

        switch (currentPage)
        {
            case 0:
                description.text = "This is the first page!";
                break;
            case 1:
                description.text = "This is the second page!";
                break;
            case 2:
                description.text = "This is the third page!";
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                print("Default Tutorial case. You shouldn't be seeing this.");
                break;
        }
    }
}
