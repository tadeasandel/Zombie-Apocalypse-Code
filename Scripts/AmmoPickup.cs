using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
  [SerializeField] int ammoAmount = 5;
  [SerializeField] AmmoType ammoType;
  [SerializeField] float rotationSpeed = 10f;
  [SerializeField] Transform pickupTransform;

  [Range(0, 1)] [SerializeField] float movementFactor;
  [SerializeField] float period = 1.5f;
  [SerializeField] Vector3 movementVector;

  Vector3 startingPosition;
  float cycle;
  const float tau = Mathf.PI * 2f;
  float rawSineWave;
  Vector3 offset;
  [SerializeField] PickupTextSpawner pickupTextSpawner = null;

  private void OnTriggerEnter(Collider other)
  {
    if (ammoAmount == 0)
    {
      return;
    }
    if (other.gameObject.tag == "Player")
    {
      GivePlayerAmmo(other);
      print(other.name);
      if (pickupTextSpawner != null)
      {
        pickupTextSpawner.SpawnTextPrefab(transform, ammoAmount);
      }
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    startingPosition = pickupTransform.position;
  }

  private void Update()
  {
    if (GetComponent<EnemyHealth>() != null)
    {
      return;
    }
    if (period <= Mathf.Epsilon)
    {
      return;
    }
    cycle = Time.time / period;
    float rawSineWave = Mathf.Sin(cycle * tau);
    movementFactor = rawSineWave / 2f + 0.5f;

    offset = movementFactor * movementVector;
    pickupTransform.position = offset + startingPosition;

    pickupTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
  }

  private void GivePlayerAmmo(Collider other)
  {
    other.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
  }
}
