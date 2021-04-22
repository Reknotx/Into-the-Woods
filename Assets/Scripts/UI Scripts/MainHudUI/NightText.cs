using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightText : SingletonPattern<NightText>
{
    private GameObject textObj;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        textObj = transform.GetChild(0).gameObject;
    }
    public void On()
    {
        textObj.SetActive(true);
    }
    public void Off()
    {
        textObj.SetActive(false);
    }
}
