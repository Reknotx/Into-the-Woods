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

    public void UpdateCollectableInfo(ScriptablePopUp popUp)
    {
        tempPopUp = popUp;
        nameText.text = popUp.itemName;
        descriptionText.text = popUp.description;
        artwork.sprite = popUp.art;
    }

    public void UpdateSpellInfo()
    {
        if(SpellList[0].gameObject.GetComponent<SpellScroll>().spellToLearn)
        {
            tempPopUp = PopUpManager.Instance.PopUps[10];
            nameText.text = tempPopUp.itemName;
            descriptionText.text = tempPopUp.description;
            artwork.sprite = tempPopUp.art;
        }
        else if(SpellList[1].gameObject.GetComponent<SpellScroll>().spellToLearn)
        {
            tempPopUp = PopUpManager.Instance.PopUps[11];
            nameText.text = tempPopUp.itemName;
            descriptionText.text = tempPopUp.description;
            artwork.sprite = tempPopUp.art;
        }
        else if(SpellList[2].gameObject.GetComponent<SpellScroll>().spellToLearn)
        {
            tempPopUp = PopUpManager.Instance.PopUps[12];
            nameText.text = tempPopUp.itemName;
            descriptionText.text = tempPopUp.description;
            artwork.sprite = tempPopUp.art;
        }
        else if(SpellList[3].gameObject.GetComponent<SpellScroll>().spellToLearn)
        {
            tempPopUp = PopUpManager.Instance.PopUps[13];
            nameText.text = tempPopUp.itemName;
            descriptionText.text = tempPopUp.description;
            artwork.sprite = tempPopUp.art;
        }
    }
}
