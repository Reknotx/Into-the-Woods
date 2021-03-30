using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 03/04/2021
/// <summary>
/// Class that handles the functionality of the Day night cycle
/// </summary>
/// 
public class LightingManager : SingletonPattern<LightingManager>
{

    #region booleans

    //boolean that represents the time of day
    [HideInInspector] public bool night;

    //boolean that tracks wether the value of t is incrementing or decrementing to determine time of day
    private bool isIncrementing;
    #endregion

    #region float
    // half the value of the length of the day
    private float _duration = 150f;
    private float _time;
    private float _customTime;
    #endregion

    #region Light
    //reference for the main light
    private Light _mainLight;
    #endregion

    #region colors
    //color variables for day and night
    private Color _perfectDay;
    private Color _perfectNight;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //assign refrence for main light
        _mainLight = GetComponent<Light>();

        //assign the values of the day and night color variables
        _perfectDay = new Color(152f, 149f, 98f);
        _perfectNight = new Color(38f, 37f, 67f);
    }

    private void Update()
    {
        _customTime += Time.deltaTime;
        UpdateLighting();
    }

    /// Author: JT Esmond
    /// Date: 03/04/2021
    /// <summary>
    /// Function that updates the lighting by lerping between two color variables over a period of time determined by the value of _duration
    /// </summary>
    private void UpdateLighting()
    {
        //ping pongs the value of the of _duration based of of time, then divides by itself to make the value between 0 and 1
        _time = Mathf.PingPong(_customTime, _duration) / _duration;

        //lerps the color of the main light between day and night based off of _time
       _mainLight.color = Color.Lerp(_perfectDay, _perfectNight, _time);

        // Tracker for time of day
        if(_time <= .01f)
        {
            isIncrementing = true;
        }
        else if(_time >= .99f)
        {
            isIncrementing = false;
        }
        if(isIncrementing)
        {
            night = false;
        }
        else
        {
            night = true;
        }
    }

    /// Author: JT Esmond
    /// Date: 3/29/2021
    /// <summary>
    /// Sets the time of day for the for the day night cycle
    /// </summary>
    public void SetTime()
    {
        _mainLight.color = _perfectDay;
        _customTime = 0;
    }
}
