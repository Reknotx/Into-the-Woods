using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUp : MonoBehaviour
{
    private int currentPage = 0;
    private int maxPages = 5; // Add 1 mentally, since we start at 0.

    public GameObject TutorialUI;
    public TMPro.TMP_Text blurb;
    public TMPro.TMP_Text details;
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
        blurb.text = "";
        details.text = "";

        switch (currentPage + 1)
        {
            case 1:
                blurb.text = "Move around the map using WASD.";
                break;
            case 2:
                blurb.text = "Use LMB to cast spells.";
                details.text = "Switch spells using the scroll wheel on your mouse or Q and E on your keyboard. Look out for additional spells as you are exploring the forest!";
                break;
            case 3:
                blurb.text = "Find a cauldron to brew potions!";
                details.text = "You will need to collect ingredients in the forest as you explore, but you have one on you when you start. Potions will be sent to your hotbar and can be consumed using 1, 2, and 3.";
                break;
            case 4:
                blurb.text = "It’s fun to explore at night!";
                details.text = "...but be careful! Keep an eye out for doors that might be closed during the day.";
                break;
            case 5:
                blurb.text = "Find your master and free him!";
                details.text = "Travel from room to room and defeat enemies along the way.";
                break;
            case 6:
                blurb.text = "Time is of the essence!";
                details.text = "You only have three days to find and free your master before he is gone forever... but beware. You’ll have to defeat his captor first!";
                break;
            default:
                print("Default Tutorial case. You shouldn't be seeing this.");
                break;
        }
    }
}
