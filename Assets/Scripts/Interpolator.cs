using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolator : MonoBehaviour
{
    public Vector3 p0 = new Vector3(0f, 0f, 0f);
    public Vector3 p1 = new Vector3(1f, 1f, 1f);

    public float timeDuration = 1f;
    public bool checkToCalculate = false;

    public Vector3 p01;
    public bool moving = false;
    public float timeStart;


    // Update is called once per frame
    void Update()
    {
        if (checkToCalculate)
        {
            checkToCalculate = false;
            moving = true;
            timeStart = Time.time;
        }

        if (moving)
        {
            float u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                moving = false;
            }
            //standard linear interpolation formula
            p01 = (1 - u) * p0 + u * p1;

            //apply the new position
            transform.position = p01;
        }

    }
}
