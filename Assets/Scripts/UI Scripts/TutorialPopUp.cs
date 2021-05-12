using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUp : MonoBehaviour
{
    private int currentPage = 0;
    private int maxPages = 7; // Add 1 mentally, since we start at 0.

    public GameObject TutorialUI;
    public TMPro.TMP_Text blurb;
    public TMPro.TMP_Text details;
    public TMPro.TMP_Text UIPageNumber;
    public RawImage videoImage;

    public Texture tut1;
    public Texture tut2;
    public Texture tut3;
    public Texture tut4;
    public Texture tut5;
    public Texture tut6;
    public Texture tut7;
    public Texture tut8;


    private void Awake()
    {

        RefreshUIChoice();
    }
    private void Start()
    {
        Time.timeScale = 0f;
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
                videoImage.texture = tut1;
                blurb.text = "Move around the map using WASD.";
                break;
            case 2:
                videoImage.texture = tut2;
                blurb.text = "Use LMB to cast spells.";
                details.text = "Switch spells using the scroll wheel on your mouse or Q and E on your keyboard. Look out for additional spells as you are exploring the forest!";
                break;
            case 3:
                videoImage.texture = tut3;
                blurb.text = "Use Tab to open your inventory.";
                details.text = "If you would like to drop an item, hover over it and press R. Remember! You can only hold 5 items!";
                break;
            case 4:
                videoImage.texture = tut4;
                blurb.text = "Find a cauldron to brew potions!";
                details.text = "You need to collect ingredients in the forest as you explore, but you have one brewed potion to start. Potions will be sent to your hotbar and can be consumed using 1, 2, and 3.";
                break;
            case 5:
                videoImage.texture = tut5;
                blurb.text = "Status effects will show on the right.";
                details.text = "This will show you things like if you have been frozen and cannot cast spells, are taking bleed damage, or have a potion effect active.";
                break;
            case 6:
                videoImage.texture = tut6;
                blurb.text = "It’s fun to explore at night!";
                details.text = "...but be careful! Keep an eye out for doors that might be closed during the day.";
                break;
            case 7:
                videoImage.texture = tut7;
                blurb.text = "Find your master and free him!";
                details.text = "Travel from room to room and defeat enemies along the way.";
                break;
            case 8:
                videoImage.texture = tut8;
                blurb.text = "Time is of the essence!";
                details.text = "You only have three days to find and free your master before he is gone forever... but beware. You’ll have to defeat his captor first!";
                break;
            default:
                print("Default Tutorial case. You shouldn't be seeing this.");
                break;
        }
    }
}
