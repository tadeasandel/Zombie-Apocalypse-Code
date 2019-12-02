using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{

  [SerializeField] GameObject leftDoor;
  [SerializeField] GameObject rightDoor;
  [SerializeField] bool isDoorClosed = false;
  [SerializeField] float doorSpeed = 1.5f;

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player" && !isDoorClosed)
    {
      StartCoroutine(CloseDoors(leftDoor.transform, rightDoor.transform, doorSpeed));
      isDoorClosed = true;
    }
  }
  IEnumerator CloseDoors(Transform doorTransformLeft, Transform doorTransformRight, float doorSpeed)
  {
    for (int i = 0; i < 13; i++)
    {
      doorTransformLeft.position = new Vector3(doorTransformLeft.position.x - 0.1f, doorTransformLeft.position.y, doorTransformLeft.position.z);
      doorTransformRight.position = new Vector3(doorTransformRight.position.x + 0.1f, doorTransformRight.position.y, doorTransformRight.position.z);
      yield return new WaitForSeconds(doorSpeed * Time.deltaTime);
    }
  }
}
