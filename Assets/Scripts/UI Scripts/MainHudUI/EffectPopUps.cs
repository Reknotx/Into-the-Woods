using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectPopUps : SingletonPattern<EffectPopUps>
{
    private ScriptableEffectPopUp temp;

    public List<GameObject> popUpHolders = new List<GameObject>();
    public List<ScriptableEffectPopUp> effectPopUps = new List<ScriptableEffectPopUp>();

    public List<Image> images = new List<Image>();

    private int listLocation = 0;

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

    public void Update()
    {
        Debug.Log(bleedActive);
    }
    public void TurnOn(int x,  bool boolean)
    {
        if (boolean == false)
        {
            popUpHolders[listLocation].gameObject.SetActive(true);
            temp = effectPopUps[x];
            images[listLocation].sprite = temp.Art;
            listLocation++;
        }
        else
        {
            return;
        }
    }

    public void TurnOff(bool boolean)
    {
        listLocation--;
        popUpHolders[listLocation].gameObject.SetActive(false);
    }
}
