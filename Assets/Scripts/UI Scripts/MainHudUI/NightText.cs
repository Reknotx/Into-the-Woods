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

    private IEnumerator delay()
    {
        textObj.SetActive(true);
        yield return new WaitForSeconds(3f);
        textObj.SetActive(false);
    }
    public void On()
    {
        StartCoroutine(delay());
    }
    public void Off()
    {
        textObj.SetActive(false);
        StopCoroutine(delay());
    }
}
