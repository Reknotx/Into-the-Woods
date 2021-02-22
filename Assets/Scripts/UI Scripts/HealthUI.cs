using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///  Author:JT Esmond
///  Date: 2/4/2021
///  <summary>
///  HealthUI class that holds all data and functions for health UI
///  </summary>

public class HealthUI : SingletonPattern<HealthUI>
{
    #region GameObjects
    /// <summary> The gameobject parent of all the normal hearts. </summary>
    public GameObject healthUIObj;

    /// <summary> The game object parent of all the bonus hearts. </summary>
    public GameObject bonusHealthUIObj;
    #endregion

    #region lists
    /// <summary> A list of all Heart Objs in the hierarchy. </summary>
    private List<GameObject> heartList = new List<GameObject>();

    /// <summary> A list of all the bonus heart objs in the hierarchy. </summary>
    private List<GameObject> bonusHeartList = new List<GameObject>();
    #endregion

    #region integers
    private int _playerHealth = 20;
    private int _bonusHealth = 0;

    private int PlayerHealth
    {
        get => _playerHealth;

        set { _playerHealth = Mathf.Clamp(value, 0, 20); }
    }

    private int BonusHealth
    {
        get => _bonusHealth;

        set { _bonusHealth = Mathf.Clamp(value, 0, 10); }
    }
    #endregion

    protected override void Awake()
    {
        ///Uses the singleton pattern class now instead
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        ///Edit: Chase O'Connor
        /// Gets a reference to all of the hearts that are a child of the UI
        /// to avoid having to manually set it in the inspector
        foreach (Transform heart in healthUIObj.transform)
        {
            if (heart.gameObject.CompareTag("Heart"))
            {
                heartList.Add(heart.gameObject);
            }
        }

        foreach (Transform bonusHeart in bonusHealthUIObj.transform)
        {
            if (bonusHeart.gameObject.CompareTag("Heart"))
            {
                bonusHeartList.Add(bonusHeart.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    #region Health UI Functions
    /// Author: Chase O'Connor
    /// Date: 2/5/2021
    /// <summary>
    /// Updates the health UI to match the Player's health.
    /// </summary>
    /// This uses a for loop to iterate over the health UI list
    /// to turn on and off hearts as needed. Incorporating JT's health
    /// UI set up as he has it in Unity.
    /// **Call when the player Gains or loses health and doesn't have an avocado**
    public void UpdateHealth()
    {
        ///This is the player's actual health which is used to test against the temp
        ///to see what hearts need to be turned on or off.
        int health = Player.Instance.Health;

        //Debug.Log("Player health = " + health.ToString());

        if (health == 20)
        {
            foreach (GameObject heart in heartList)
            {
                heart.transform.GetChild(0).gameObject.SetActive(true);
                heart.transform.GetChild(1).gameObject.SetActive(false);
            }

            return;
        }

        ///the int i starts at the end of the list and goes through it backwards
        for (int i = heartList.Count - 1; i >= 0; i--)
        {
            ///This starts checking a value of 20 to see what the player's
            ///health matches up to.
            int tempHealth = (i * 2) + 2;

            //Debug.Log("Temp health = " + tempHealth.ToString());

            GameObject heartObj = heartList[i];

            GameObject fullHeart = heartObj.transform.GetChild(0).gameObject;
            GameObject halfHeart = heartObj.transform.GetChild(1).gameObject;

            if (tempHealth > health)
            {
                ///The value of temp health is greater than our actual health.
                ///such as temp = 20 where actual is 19
                if (tempHealth - 1 == health)
                {
                    ///Example: tempHealth is at 20 but player health is 19
                    ///this means we want to turn off the full but turn on
                    ///the half.
                    fullHeart.SetActive(false);
                    halfHeart.SetActive(true);
                }
                else
                {
                    fullHeart.SetActive(false);
                    halfHeart.SetActive(false);
                }
            }
            else if (tempHealth <= health)
            {
                if (tempHealth + 1 == health)
                {
                    fullHeart.SetActive(true);
                }
                else
                {
                    fullHeart.SetActive(true);
                    halfHeart.SetActive(false);
                }
            }
        }
    }


    /// Author: JT Esmond
    /// Date: 2/5/2021
    /// <summary> After Chase assisted me in condensing my code for the Health UI
    /// and making it more manageable, I utilized his code in setting up the UI
    /// for the bonus health.
    /// </summary>
    public void UpdateBonusHealth()
    {
        int bonusHealth = Player.Instance.BonusHealth;
        //int bonusHealth = BonusHealth;
        //Debug.Log("Bonus health = " + bonusHealth.ToString());
        
        if (BonusHealth == 10)
        {
            foreach (GameObject bonusHeart in bonusHeartList)
            {
                bonusHeart.transform.GetChild(0).gameObject.SetActive(true);
                bonusHeart.transform.GetChild(1).gameObject.SetActive(false);
                bonusHeart.transform.GetChild(2).gameObject.SetActive(true);
            }

            return; /// Added to leave the script early
        }

        for (int i = bonusHeartList.Count - 1; i >= 0; i--)
        {
            int tempBonusHealth = (i * 2) + 2;

            GameObject bonusHeartObj = bonusHeartList[i];

            GameObject bonusFullHeart = bonusHeartObj.transform.GetChild(0).gameObject;
            GameObject bonusHalfHeart = bonusHeartObj.transform.GetChild(1).gameObject;
            GameObject bonusBGHeart = bonusHeartObj.transform.GetChild(2).gameObject;

            if (tempBonusHealth > bonusHealth)
            {
                if (tempBonusHealth - 1 == bonusHealth)
                {
                    bonusFullHeart.SetActive(false);
                    bonusHalfHeart.SetActive(true);
                    bonusBGHeart.SetActive(true);
                }
                else
                {
                    //Debug.Log("Happening");
                    bonusFullHeart.SetActive(false);
                    bonusHalfHeart.SetActive(false);
                    bonusBGHeart.SetActive(false);
                }
            }
            else if (tempBonusHealth <= bonusHealth)
            {
                if (tempBonusHealth + 1 == bonusHealth)
                {
                    bonusFullHeart.SetActive(true);
                    bonusHalfHeart.SetActive(false);
                    bonusBGHeart.SetActive(true);
                }
                else
                {
                    bonusFullHeart.SetActive(true);
                    bonusHalfHeart.SetActive(false);
                    bonusBGHeart.SetActive(true);
                }
            }
        }
    }
    #endregion 
}

