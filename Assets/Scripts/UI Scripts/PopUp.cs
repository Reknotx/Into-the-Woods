using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public ScriptablePopUp popUp;
    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text descriptionText;

    public Image artwork;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = popUp.itemName;
        descriptionText.text = popUp.description;
        artwork.sprite = popUp.art;
    }
}
