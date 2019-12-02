using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PickupTextSpawner : MonoBehaviour
{
  [SerializeField] GameObject spawnPrefab;

  GameObject textPrefab = null;

  public void SpawnTextPrefab(Transform pickupLocation, float textValue)
  {
    textPrefab = Instantiate(spawnPrefab, pickupLocation.position, Quaternion.identity);
    textPrefab.GetComponentInChildren<Text>().text = String.Format("+ " + textValue.ToString());
    Destroy(textPrefab, 6f);
  }
  private void Update()
  {
    if (textPrefab != null)
    {
      textPrefab.transform.forward = Camera.main.transform.forward;
    }
  }
}
