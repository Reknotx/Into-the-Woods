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
    //reference for the light in the scene
    [SerializeField] private Light DirectionalLight;

    //reference for the preset lighting variables
    [SerializeField] private LightingPreset Preset;

    //reference for the TimeOfDay variable;
    [HideInInspector] public float TimeOfDay;

    //variables used to check if its night or not
    private float time;
    [HideInInspector] public bool night;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //sets the time of day at start at noon
        TimeOfDay += 180;
    }

    private void Update()
    {

        //sets a boolean depending on the time of day
        if(TimeOfDay >= 90 && TimeOfDay <=270)
        {
            night = false;
        }
        else
        {
            night = true;
        }


        if (Preset == null)
            return;

        //runs the day night cycle
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 360f;
            UpdateLighting(TimeOfDay / 360f);
        }

        //temporary testing for setting time of day
        if(Input.GetKeyDown(KeyCode.A))
        {
            TimeOfDay = 180;
        }
    }

    /// Author: JT Esmond
    /// Date: 03/04/2021
    /// <summary>
    /// Function that updates the lighting, and rotates the light in the scene
    /// </summary>
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent)
;

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    //turns on the Sun in the game view
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                    return;
            }
        }
    }
}
