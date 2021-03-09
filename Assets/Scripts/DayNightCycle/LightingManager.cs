using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 03/04/2021
/// <summary>
/// Class that handles the functionality of the Day night cycle
/// </summary>
/// 
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //reference for the light in the scene
    [SerializeField] private Light DirectionalLight;

    //reference for the preset lighting variables
    [SerializeField] private LightingPreset Preset;

    //reference for the TimeOfDay variable;
    [SerializeField] private float TimeOfDay;

    //variables used to check if its night or not
    private float time;
    private bool night;


    private void Update()
    {
        time = transform.rotation.x;
        if (time >= -25)
        {
            night = false;
        }
        else if (TimeOfDay >= 295 && TimeOfDay <= 60)
        {
            night = true;
        }

        if (Preset == null)
            return;


        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 360f;
            UpdateLighting(TimeOfDay / 720f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 360f);
        }

        Debug.Log(night);

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
