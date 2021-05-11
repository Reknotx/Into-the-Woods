using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableRotation : MonoBehaviour
{
    /// <summary>
    /// Author: Paul Hernandez
    /// Date: 4/19/2021
    /// This is some logic for object rotation.
    /// Primarily for Collectable, but also for the "Learn" Interactables.
    /// </summary>

    public GameObject rotateObject;
    protected float rotationSpeed = 1f;
    protected float bobHeight;
    protected float bobSpeed;
    protected float originalY;
    protected float timeOffset;
    public GameObject UItoOffset;

    void Awake()
    {
        bobHeight = 0.1f;
        this.originalY = this.transform.position.y + bobHeight;
        timeOffset = Time.time;
        if (rotateObject == null)
        {
            rotateObject = this.gameObject;
        }

    }

    void FixedUpdate()
    {
        if (rotateObject.activeSelf)
        {
            rotateObject.transform.Rotate(0f, rotationSpeed, 0f, Space.World);

            if (rotateObject == this.gameObject) // If the root gameObject is also the model, then offset the rotation of the popup.
            {
                //this.gameObject.transform.GetChild(0).transform.Rotate(0f, -rotationSpeed, 0f, Space.World);
                UItoOffset.transform.Rotate(0f, -rotationSpeed, 0f, Space.World);
            }

            transform.position = new Vector3(transform.position.x, originalY + ((float)Math.Sin(Time.time - timeOffset) * bobHeight), transform.position.z);
        }
        
    }
}
