using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///  Author:JT Esmond
///  Date: 2/4/2021
///  <summary>
///  HealthUI class that holds all data and functions for health UI
///  </summary>

public class HealthUI : MonoBehaviour
{

    /// <summary> Singleton instance of Health UI</summary>
    public static HealthUI Instance;

    /// <summary> UI gameobjects </summary>
    public GameObject healthFull1;
    public GameObject healthFull2;
    public GameObject healthFull3;
    public GameObject healthFull4;
    public GameObject healthFull5;
    public GameObject healthFull6;
    public GameObject healthFull7;
    public GameObject healthFull8;
    public GameObject healthFull9;
    public GameObject healthFull10;
    public GameObject healthFull11;
    public GameObject healthFull12;
    public GameObject healthFull13;
    public GameObject healthFull14;
    public GameObject healthFull15;
    public GameObject healthHalf1;
    public GameObject healthHalf2;
    public GameObject healthHalf3;
    public GameObject healthHalf4;
    public GameObject healthHalf5;
    public GameObject healthHalf6;
    public GameObject healthHalf7;
    public GameObject healthHalf8;
    public GameObject healthHalf9;
    public GameObject healthHalf10;
    public GameObject healthHalf11;
    public GameObject healthHalf12;
    public GameObject healthHalf13;
    public GameObject healthHalf14;
    public GameObject healthHalf15;
    public GameObject healthBG1;
    public GameObject healthBG2;
    public GameObject healthBG3;
    public GameObject healthBG4;
    public GameObject healthBG5;
    public GameObject healthBG6;
    public GameObject healthBG7;
    public GameObject healthBG8;
    public GameObject healthBG9;
    public GameObject healthBG10;
    public GameObject healthBG11;
    public GameObject healthBG12;
    public GameObject healthBG13;
    public GameObject healthBG14;
    public GameObject healthBG15;

    //integers used in script
    private int playerHealth;
    public int avacado1Health = 2;
    public int avacado2Health = 2;
    public int avacado3Health = 2;
    public int avacado4Health = 2;
    public int avacado5Health = 2;

    //booleans to check if the player has avocados
    public bool hasAvacado1;
    public bool hasAvacado2;
    public bool hasAvacado3;
    public bool hasAvacado4;
    public bool hasAvacado5;

    public void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 20;
        hasAvacado1 = false;
        hasAvacado2 = false;
        hasAvacado3 = false;
        hasAvacado4 = false;
        hasAvacado5 = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Health: " + playerHealth);

        //temporary code to test that the UI works
        if (Input.GetKeyDown("a"))
        {
            if (hasAvacado1 == true && hasAvacado2 == false && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
            {
                avacado1Health--;
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
            {
                avacado2Health--;
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == false && hasAvacado5 == false)
            {
                avacado3Health--;
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == true && hasAvacado5 == false)
            {
                avacado4Health--;
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == true && hasAvacado5 == true)
            {
                avacado5Health--;
            }
            else if (hasAvacado1 == false && hasAvacado2 == false && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
            {
                playerHealth--;
            }
        }

        //temporary code to add avocados and test the UI
        if (Input.GetKeyDown("d"))
        {
            if (hasAvacado1 == false && hasAvacado2 == false && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
            {
                avacado1Health = 2;
                hasAvacado1 = true;
                Debug.Log("Picked up avacado");
            }
            else if (hasAvacado1 == true && hasAvacado2 == false && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
            {
                avacado2Health = 2;
                hasAvacado2 = true;
                Debug.Log("Picked up avacado");
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
            {
                avacado3Health = 2;
                hasAvacado3 = true;
                Debug.Log("Picked up avacado");
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == false && hasAvacado5 == false)
            {
                avacado4Health = 2;
                hasAvacado4 = true;
                Debug.Log("Picked up avacado");
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == true && hasAvacado5 == false)
            {
                avacado5Health = 2;
                hasAvacado5 = true;
                Debug.Log("Picked up avacado");
            }
            else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == true && hasAvacado5 == true)
            {
                Debug.Log("Inventory Full");
            }
        }

        healthCheck();
    }

    ///  Author: JT Esmond
    ///  Date: 2/4/2021
    ///  <summary>
    ///  Function that runs all aspects of the health UI
    ///  </summary>
    public void healthCheck()
    {
        //playerHealth = Player.Instance.health;

        //outer conditionals check if the player has any avacados in their inventory
        if (hasAvacado1 == false && hasAvacado2 == false && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
        {
            //this set of conditionals check to see how much health the player has, determines what hearts to display on the players health bar
            if (playerHealth == 20)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(true);
                healthFull8.SetActive(true);
                healthFull9.SetActive(true);
                healthFull10.SetActive(true);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 19)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(true);
                healthFull8.SetActive(true);
                healthFull9.SetActive(true);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(true);
            }
            else if (playerHealth == 18)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(true);
                healthFull8.SetActive(true);
                healthFull9.SetActive(true);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 17)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(true);
                healthFull8.SetActive(true);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(true);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 16)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(true);
                healthFull8.SetActive(true);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 15)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(true);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(true);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 14)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(true);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 13)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(true);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 12)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(true);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 11)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(true);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 10)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(true);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 9)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(true);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 8)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(true);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 7)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(true);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 6)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(true);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 5)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(false);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(true);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 4)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(true);
                healthFull3.SetActive(false);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 3)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(false);
                healthFull3.SetActive(false);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(true);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 2)
            {
                healthFull1.SetActive(true);
                healthFull2.SetActive(false);
                healthFull3.SetActive(false);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 1)
            {
                healthFull1.SetActive(false);
                healthFull2.SetActive(false);
                healthFull3.SetActive(false);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(true);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
            }
            else if (playerHealth == 0)
            {
                healthFull1.SetActive(false);
                healthFull2.SetActive(false);
                healthFull3.SetActive(false);
                healthFull4.SetActive(false);
                healthFull5.SetActive(false);
                healthFull6.SetActive(false);
                healthFull7.SetActive(false);
                healthFull8.SetActive(false);
                healthFull9.SetActive(false);
                healthFull10.SetActive(false);
                healthHalf1.SetActive(false);
                healthHalf2.SetActive(false);
                healthHalf3.SetActive(false);
                healthHalf4.SetActive(false);
                healthHalf5.SetActive(false);
                healthHalf6.SetActive(false);
                healthHalf7.SetActive(false);
                healthHalf8.SetActive(false);
                healthHalf9.SetActive(false);
                healthHalf10.SetActive(false);
                Debug.Log("Game Over");
                //gameOver
            }
        }
        else if (hasAvacado1 == true && hasAvacado2 == false && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
        {
            /*after the outer conditional determines that the player has 1 avocado in their inventory, then checks how much health the avocado has and displays the correct amount of health
            * after the avocado loses all of its health the heart disapears all together
            */
            if (avacado1Health == 2)
            {
                healthBG11.SetActive(true);
                healthFull11.SetActive(true);
                healthHalf11.SetActive(false);
            }
            else if (avacado1Health == 1)
            {
                healthBG11.SetActive(true);
                healthFull11.SetActive(false);
                healthHalf11.SetActive(true);
            }
            else if (avacado1Health == 0)
            {
                healthBG11.SetActive(false);
                healthFull11.SetActive(false);
                healthHalf11.SetActive(false);
                hasAvacado1 = false;
            }
        }
        else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == false && hasAvacado4 == false && hasAvacado5 == false)
        {
            /*after the outer conditional determines that the player has 2 avocado in their inventory, then checks how much health the avocado has and displays the correct amount of health
            * after the avocado loses all of its health the heart disapears all together
            */
            if (avacado2Health == 2)
            {
            healthBG12.SetActive(true);
            healthFull12.SetActive(true);
            healthHalf12.SetActive(false);
            }
            else if (avacado2Health == 1)
            {
            healthBG12.SetActive(true);
            healthFull12.SetActive(false);
            healthHalf12.SetActive(true);
            }
            else if (avacado2Health == 0)
            {
            healthBG12.SetActive(false);
            healthFull12.SetActive(false);
            healthHalf12.SetActive(false);
            hasAvacado2 = false;
            }
        }
        else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == false && hasAvacado5 == false)
        {
            /*after the outer conditional determines that the player has 3 avocado in their inventory, then checks how much health the avocado has and displays the correct amount of health
            * after the avocado loses all of its health the heart disapears all together
            */
            if (avacado3Health == 2)
            {
                healthBG13.SetActive(true);
                healthFull13.SetActive(true);
                healthHalf13.SetActive(false);
            }
            else if (avacado3Health == 1)
            {
                healthBG13.SetActive(true);
                healthFull13.SetActive(false);
                healthHalf13.SetActive(true);
            }
            else if (avacado3Health == 0)
            {
                healthBG13.SetActive(false);
                healthFull13.SetActive(false);
                healthHalf13.SetActive(false);
                hasAvacado3 = false;
            }
        }
        else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == true && hasAvacado5 == false)
        {
            /*after the outer conditional determines that the player has 4 avocado in their inventory, then checks how much health the avocado has and displays the correct amount of health
            * after the avocado loses all of its health the heart disapears all together
            */
            if (avacado4Health == 2)
            {
                healthBG14.SetActive(true);
                healthFull14.SetActive(true);
                healthHalf14.SetActive(false);
            }
            else if (avacado4Health == 1)
            {
                healthBG14.SetActive(true);
                healthFull14.SetActive(false);
                healthHalf14.SetActive(true);
            }
            else if (avacado4Health == 0)
            {
                healthBG14.SetActive(false);
                healthFull14.SetActive(false);
                healthHalf14.SetActive(false);
                hasAvacado4 = false;
            }
        }
        else if (hasAvacado1 == true && hasAvacado2 == true && hasAvacado3 == true && hasAvacado4 == true && hasAvacado5 == true)
        {
            /*after the outer conditional determines that the player has 5 avocado in their inventory, then checks how much health the avocado has and displays the correct amount of health
            * after the avocado loses all of its health the heart disapears all together
            */
            if (avacado5Health == 2)
            {
                healthBG15.SetActive(true);
                healthFull15.SetActive(true);
                healthHalf15.SetActive(false);
            }
            else if (avacado5Health == 1)
            {
                healthBG15.SetActive(true);
                healthFull15.SetActive(false);
                healthHalf15.SetActive(true);
            }
            else if (avacado5Health == 0)
            {
                healthBG15.SetActive(false);
                healthFull15.SetActive(false);
                healthHalf15.SetActive(false);
                hasAvacado5 = false;
            }
        }
    }
}

