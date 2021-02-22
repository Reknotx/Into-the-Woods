using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///  Author: JT Esmond
///  Date: 2/21/2021
///  <summary>
/// The UI class that handles the physical representation of the day night cycle
///  </summary>

public class ClockUI : MonoBehaviour
{
    //floats that are used in handling the Clock UI
    public float timerMinutes;
    private const float realSecondsPerInGameDay = 5f;
    private float day;

    //transform for the clockhand
    private Transform clockHandTransform;

    //text reference for the clock UI text
    private Text dayText;

    private void Awake()
    {
        //setting the reference for the clock hand and the reference for the text element
        clockHandTransform = transform.Find("clockHand");
        dayText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        //sets the day float variable
        day += Time.deltaTime / realSecondsPerInGameDay;

        //normalizes the day float
        float dayNormalized = day % 1f;

        //float for the amount of rotation per day
        float rotationDegreesPerDay = 360f;

        //rotates the clock hand the correct amount based off of the normalized day value
        clockHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);




        //*** counter for the actual day counter, remove coments when day night cycle implemented
        //string dayString = (Mathf.FloorToInt(day)).ToString("");
        //dayText.text = ("Day: " + dayString);

        //*** temporary timer info that can be deleted when the day night cycle is implemented
        string minutesString = Mathf.Floor(timerMinutes - Mathf.Floor(day)).ToString("0");
        dayText.text = minutesString + " Minutes";
    }
}
