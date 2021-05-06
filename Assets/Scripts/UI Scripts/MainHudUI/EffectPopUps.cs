using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date; 4/11/2021
/// <summary>
/// Class that handles the effect popUps
/// </summary>
public class EffectPopUps : SingletonPattern<EffectPopUps>
{
    //temp value for the scriptable objects
    private ScriptableEffectPopUp temp;

    //list for the holder game objects
    public List<GameObject> popUpHolders = new List<GameObject>();
    //list for the scriptable object pop ups
    public List<ScriptableEffectPopUp> effectPopUps = new List<ScriptableEffectPopUp>();
    //list for the image game objects
    public List<Image> images = new List<Image>();
    //list for the text game objects
    public List<TMPro.TMP_Text> text = new List<TMPro.TMP_Text>();
    // int used to determine which holder to turn on
    private int listLocation = 0;

    // set of bools used to stop the effects from popping up more then once on screen
    [HideInInspector] public bool bleedActive;
    [HideInInspector] public bool doubleDamageActive;
    [HideInInspector] public bool frozenHeartActive;
    [HideInInspector] public bool frozenActive;
    [HideInInspector] public bool invisibilityActive;
    [HideInInspector] public bool nightwalkerActive;

    protected override void Awake()
    {
        base.Awake();
    }

    /// Author: JT Esmond
    /// Date: 4/11/2021
    /// <summary>
    /// Function used to turn on the different pop ups
    /// </summary>
    public void TurnOn(int x,  bool boolean)
    {
        //checks if the specified pop up is already active, if it isn't turns it on
        if (boolean == false)
        {
            popUpHolders[listLocation].gameObject.SetActive(true);
            temp = effectPopUps[x];
            images[listLocation].sprite = temp.Art;
            text[listLocation].text = temp.text;
            listLocation++;
        }
        else
        {
            return;
        }
    }

    /// Author: JT Esmond
    /// Date: 4/11/2021
    /// <summary>
    /// function that turns off the specified pop up
    /// </summary>
    public void TurnOff(bool boolean)
    {
        if (listLocation > 0)
        {
            listLocation--;
        }
        popUpHolders[listLocation].gameObject.SetActive(false);
    }
}
