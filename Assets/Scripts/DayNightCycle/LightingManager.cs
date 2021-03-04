using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 03/04/2021
/// <summary>
/// Class that handles the functionality of the Day night cycle
/// </summary>
public class LightingManager : MonoBehaviour
{

    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    [SerializeField] private float TimeOfDay;

    private void Update()
    {
        if (Preset == null)
            return;
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 360f;
            UpdateLighting(TimeOfDay / 360f);
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
