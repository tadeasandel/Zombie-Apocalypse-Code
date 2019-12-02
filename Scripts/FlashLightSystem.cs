using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;
    [SerializeField] float intensityMaximum = 6f;
    [SerializeField] float angleMaximum = 70f;

    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }
    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }
    
    public void RestoreLightAngle()
    {
        myLight.spotAngle = angleMaximum;
    }
    public void RestoreLightIntensity(float restoreIntensity)
    {
        if (intensityMaximum <= myLight.intensity)
        {
            myLight.intensity = intensityMaximum;
        }
        else
        {
            myLight.intensity += restoreIntensity;
        }
    }


    private void DecreaseLightIntensity()
    {
        myLight.intensity -= (lightDecay * Time.deltaTime);
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle >= minimumAngle)
        {
            myLight.spotAngle -= (angleDecay * Time.deltaTime);
        }
    }
}
