using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float intensityRestore = 3f;
    FlashLightSystem myLightSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myLightSystem = FindObjectOfType<FlashLightSystem>();
            //myLightSystem = other.GetComponent<FlashLightSystem>();
            myLightSystem.RestoreLightAngle();
            myLightSystem.RestoreLightIntensity(intensityRestore);
            Destroy(gameObject);
        }
    }
}
