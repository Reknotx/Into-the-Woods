using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : SingletonPattern<PopUp>
{
    public ScriptablePopUp tempPopUp;
    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text descriptionText;
    public Image artwork;


    public List<GameObject> SpellList = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    /// Author: JT Esmond
    /// Date: 4/8/2021
    /// <summary>
    /// Function that updates the values of the pop up variables for the Collectibles
    /// </summary>
    public void UpdateCollectableInfo(ScriptablePopUp popUp)
    {
        tempPopUp = popUp;
        nameText.text = popUp.itemName;
        descriptionText.text = popUp.description;
        artwork.sprite = popUp.art;
    }

    /// Author: JT Esmond
    /// Date: 4/10/2021
    /// <summary>
    /// function that updates the values of the pop up variables for the interactables
    /// </summary>
    public void UpdateInteractableInfo(int x)
    {
        tempPopUp = PopUpManager.Instance.PopUps[x];
        nameText.text = tempPopUp.itemName;
        descriptionText.text = tempPopUp.description;
        artwork.sprite = tempPopUp.art;
    }

}
