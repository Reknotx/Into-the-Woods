using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///  Author: JT Esmond
///  Date: 2/21/2021
///  <summary>
/// The UI class that handles the physical representation of the day night cycle
///  </summary>

public class ClockUI : SingletonPattern<ClockUI>
{
    //floats that are used in handling the Clock UI
    private const float realSecondsPerInGameDay = 300f;


    private float day;
    private float dayCounter;

    //transform for the clockhand
    private Transform clockHandTransform;

    //text reference for the clock UI text
    private Text dayText;

    protected override void Awake()
    {
        base.Awake();
        //setting the reference for the clock hand and the reference for the text element
        clockHandTransform = transform.Find("clockHand");
        dayText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }

    private void Start()
    {
        dayCounter = 1;
        StartCoroutine("timerTracker");
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

        //
        string dayString = (Mathf.FloorToInt(dayCounter)).ToString("");
        dayText.text = ("Day" + "\n" + dayString);

        if(Input.GetKeyDown(KeyCode.L))
        {
            SetTime();
        }
        if (dayCounter > 3)
        {
            WinLoseUI.Instance.YouLose();
        }
    }

    /// Author: JT Esmond
    /// Date: 02/22/2021
    /// <summary>
    /// tracks the temporary timer that tracks the time passed.
    /// </summary>
    private IEnumerator timerTracker()
    {

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(300f);
            dayCounter++;
        }
    }

    /// Author: JT Esmond
    /// Date: 3/29/2021
    /// <summary>
    /// function that sets the time of day for the clock
    /// </summary>
    public void SetTime()
    {
        day = 0;
        LightingManager.Instance.SetTime();
        dayCounter++;
    }
}
