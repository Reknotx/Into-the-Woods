﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : SingletonPattern<CameraTransition>
{
    [SerializeField]
    float zOffset = 4.53f;

    public float transSpeed = 1.5f;

    public bool Done { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
    }


    public void TransitionToPoint(Vector3 pos)
    {
        Done = false;
        StartCoroutine(Transition(pos));
    }


    IEnumerator Transition(Vector3 pos)
    {
        bool moving = true;

        float timeStart = Time.time;
        Vector3 start = transform.position;
        Vector3 end = new Vector3(pos.x, transform.position.y, pos.z - zOffset);
        Vector3 newPos = Vector3.zero;

        while (moving)
        {
            float u = (Time.time - timeStart) / (1 / transSpeed);


            if (u >= 1f)
            {
                u = 1;
                moving = false;
            }

            u = EaseInOut(u);

            newPos = (1 - u) * start + u * end;

            transform.position = newPos;


            yield return new WaitForFixedUpdate();

        }
        Done = true;

        float Lerp(float start_value, float end_value, float pct)
        {
            return (start_value + (end_value - start_value) * pct);
        }

        float Flip(float x)
        {
            return 1 - x;
        }

        float EaseIn(float t)
        {
            return t * t;
        }

        float EaseOut(float t)
        {
            return Flip(Mathf.Pow( Flip(t), 2) );
        }

        float EaseInOut(float t)
        {
            return Lerp(EaseIn(t), EaseOut(t), t);
        }

    }

}
