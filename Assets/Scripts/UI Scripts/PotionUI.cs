using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 2/6/2021
/// <summary>
/// The Class that handles the potion UI
/// </summary>
/// 
public class PotionUI : MonoBehaviour
{
    #region fields
    public static PotionUI Instance;
    #endregion

    #region lists
    /// <summary> lists for both the different potions, and the slots for the UI </summary>
    private List<GameObject> _potionListSlot1 = new List<GameObject>();
    private List<GameObject> _potionListSlot2 = new List<GameObject>();
    private List<GameObject> _potionListSlot3 = new List<GameObject>();
    #endregion

    #region GameObjects
    public GameObject potionSlot1;
    public GameObject potionSlot2;
    public GameObject potionSlot3;
    #endregion

    #region booleans
    [HideInInspector] public bool hasPotion;
    [HideInInspector] public bool potion1Full;
    [HideInInspector] public bool potion2Full;
    [HideInInspector] public bool potion3Full;
    [HideInInspector] public bool healthPotion;
    [HideInInspector] public bool superHealthPotion;
    [HideInInspector] public bool invisibilityPotion;
    [HideInInspector] public bool doubleDamagePotion;
    [HideInInspector] public bool nightWalkerPotion;
    [HideInInspector] public bool frozenHeartPotion;
    #endregion

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
        /// gets the references for all of the potionslots and all of the potions in the PotionSlot UI Holder
        foreach (Transform potion in potionSlot1.transform)
        {
            if(potion.gameObject.CompareTag("Potion"))
            {
                _potionListSlot1.Add(potion.gameObject);
            }
        }
        foreach (Transform potion in potionSlot2.transform)
        {
            if(potion.gameObject.CompareTag("Potion"))
            {
                _potionListSlot2.Add(potion.gameObject);
            }
        }
        foreach (Transform potion in potionSlot3.transform)
        {
            if(potion.gameObject.CompareTag("Potion"))
            {
                _potionListSlot3.Add(potion.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        ///all of these are temporary commands to test the UI
        if (Input.GetKeyDown("l"))
        {
            hasPotion = true;
            frozenHeartPotion = true;
            UpdatePotion();
            frozenHeartPotion = false;
        }
        if (Input.GetKeyDown("k"))
        {
            hasPotion = true;
            nightWalkerPotion = true;
            UpdatePotion();
            nightWalkerPotion = false;
        }
        if (Input.GetKeyDown("j"))
        {
            hasPotion = true;
            doubleDamagePotion = true;
            UpdatePotion();
            doubleDamagePotion = false;
        }
        if (Input.GetKeyDown("h"))
        {
            hasPotion = true;
            invisibilityPotion = true;
            UpdatePotion();
            invisibilityPotion = false;
        }
        if (Input.GetKeyDown("g"))
        {
            hasPotion = true;
            superHealthPotion = true;
            UpdatePotion();
            superHealthPotion = false;
        }
        if (Input.GetKeyDown("f"))
        {
            hasPotion = true;
            healthPotion = true;
            UpdatePotion();
            healthPotion = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i <= 5; i++)
            {
                _potionListSlot1[i].gameObject.SetActive(false);
                potion1Full = false;
            }
            healthPotion = false;
            superHealthPotion = false;
            invisibilityPotion = false;
            doubleDamagePotion = false;
            nightWalkerPotion = false;
            frozenHeartPotion = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i <= 5; i++)
            {
                _potionListSlot2[i].gameObject.SetActive(false);
                potion2Full = false;
            }
            healthPotion = false;
            superHealthPotion = false;
            invisibilityPotion = false;
            doubleDamagePotion = false;
            nightWalkerPotion = false;
            frozenHeartPotion = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i <= 5; i++)
            {
                _potionListSlot3[i].gameObject.SetActive(false);
                potion3Full = false;
            }
            healthPotion = false;
            superHealthPotion = false;
            invisibilityPotion = false;
            doubleDamagePotion = false;
            nightWalkerPotion = false;
            frozenHeartPotion = false;
        }
        */
    }

    #region Potion UI functions

    /// Author: JT Esmond
    /// Date:2/6/2021
    /// <summary>
    /// function that runs the potion slot UI elements
    /// ** Call when the player picks up a potion**
    /// </summary>
    public void UpdatePotion()
    {
        ///checks if the player has a potion
        ///**hasPotion needs to be set to true when the player picks up a potion,
        ///along with the boolean for the type of potion**
        if (hasPotion == true)
        {
            ///a series of conditionals that determine what potion slot to put the potion in
            ///**potion1Full needs to be set to false when the player uses the key bind to drink a potion**
            if (potion1Full == false)
            {
                /// a series of conditionals that deteremines which potion to display on the UI
                if (healthPotion == true)
                {
                    _potionListSlot1[0].gameObject.SetActive(true);
                    for (int i = 1; i <= 5; i++)
                    {
                        _potionListSlot1[i].gameObject.SetActive(false);
                    }
                }
                else if (superHealthPotion == true)
                {
                    _potionListSlot1[0].gameObject.SetActive(false);
                    _potionListSlot1[1].gameObject.SetActive(true);
                    for (int i = 2; i <= 5; i++)
                    {
                        _potionListSlot1[i].gameObject.SetActive(false);
                    }
                }
                else if (invisibilityPotion == true)
                {
                    _potionListSlot1[0].gameObject.SetActive(false);
                    _potionListSlot1[1].gameObject.SetActive(false);
                    _potionListSlot1[2].gameObject.SetActive(true);
                    for (int i = 3; i <= 5; i++)
                    {
                        _potionListSlot1[i].gameObject.SetActive(false);
                    }
                }
                else if (doubleDamagePotion == true)
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        _potionListSlot1[i].gameObject.SetActive(false);
                    }
                    _potionListSlot1[3].gameObject.SetActive(true);
                    _potionListSlot1[4].gameObject.SetActive(false);
                    _potionListSlot1[5].gameObject.SetActive(false);
                }
                else if (nightWalkerPotion == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        _potionListSlot1[i].gameObject.SetActive(false);
                    }
                    _potionListSlot1[4].gameObject.SetActive(true);
                    _potionListSlot1[5].gameObject.SetActive(false);
                }
                else if (frozenHeartPotion == true)
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        _potionListSlot1[i].gameObject.SetActive(false);
                    }
                    _potionListSlot1[5].gameObject.SetActive(true);
                }
                hasPotion = false;
                potion1Full = true;

            }
            ///**potion2Full needs to be set to false when the player uses the key bind to drink a potion**
            else if (potion1Full == true && potion2Full == false)
            {
                if (healthPotion == true)
                {
                    _potionListSlot2[0].gameObject.SetActive(true);
                    for (int i = 1; i <= 5; i++)
                    {
                        _potionListSlot2[i].gameObject.SetActive(false);
                    }
                }
                else if (superHealthPotion == true)
                {
                    _potionListSlot2[0].gameObject.SetActive(false);
                    _potionListSlot2[1].gameObject.SetActive(true);
                    for (int i = 2; i <= 5; i++)
                    {
                        _potionListSlot2[i].gameObject.SetActive(false);
                    }
                }
                else if (invisibilityPotion == true)
                {
                    _potionListSlot2[0].gameObject.SetActive(false);
                    _potionListSlot2[1].gameObject.SetActive(false);
                    _potionListSlot2[2].gameObject.SetActive(true);
                    for (int i = 3; i <= 5; i++)
                    {
                        _potionListSlot2[i].gameObject.SetActive(false);
                    }
                }
                else if (doubleDamagePotion == true)
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        _potionListSlot2[i].gameObject.SetActive(false);
                    }
                    _potionListSlot2[3].gameObject.SetActive(true);
                    _potionListSlot2[4].gameObject.SetActive(false);
                    _potionListSlot2[5].gameObject.SetActive(false);
                }
                else if (nightWalkerPotion == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        _potionListSlot2[i].gameObject.SetActive(false);
                    }
                    _potionListSlot2[4].gameObject.SetActive(true);
                    _potionListSlot2[5].gameObject.SetActive(false);
                }
                else if (frozenHeartPotion == true)
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        _potionListSlot2[i].gameObject.SetActive(false);
                    }
                    _potionListSlot2[5].gameObject.SetActive(true);
                }
                hasPotion = false;
                potion2Full = true;
            }
            ///**potion3Full needs to be set to false when the player uses the key bind to drink a potion**
            else if (potion1Full == true && potion2Full == true && potion3Full == false)
            {

                if (healthPotion == true)
                {
                    _potionListSlot3[0].gameObject.SetActive(true);
                    for (int i = 1; i <= 5; i++)
                    {
                        _potionListSlot3[i].gameObject.SetActive(false);
                    }
                }
                else if (superHealthPotion == true)
                {
                    _potionListSlot3[0].gameObject.SetActive(false);
                    _potionListSlot3[1].gameObject.SetActive(true);
                    for (int i = 2; i <= 5; i++)
                    {
                        _potionListSlot3[i].gameObject.SetActive(false);
                    }
                }
                else if (invisibilityPotion == true)
                {
                    _potionListSlot3[0].gameObject.SetActive(false);
                    _potionListSlot3[1].gameObject.SetActive(false);
                    _potionListSlot3[2].gameObject.SetActive(true);
                    for (int i = 3; i <= 5; i++)
                    {
                        _potionListSlot3[i].gameObject.SetActive(false);
                    }
                }
                else if (doubleDamagePotion == true)
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        _potionListSlot3[i].gameObject.SetActive(false);
                    }
                    _potionListSlot3[3].gameObject.SetActive(true);
                    _potionListSlot3[4].gameObject.SetActive(false);
                    _potionListSlot3[5].gameObject.SetActive(false);
                }
                else if (nightWalkerPotion == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        _potionListSlot3[i].gameObject.SetActive(false);
                    }
                    _potionListSlot3[4].gameObject.SetActive(true);
                    _potionListSlot3[5].gameObject.SetActive(false);
                }
                else if (frozenHeartPotion == true)
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        _potionListSlot3[i].gameObject.SetActive(false);
                    }
                    _potionListSlot3[5].gameObject.SetActive(true);
                }
                hasPotion = false;
                potion3Full = true;
            }
        }
    }

    #endregion
}
