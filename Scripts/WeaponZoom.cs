using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
  [SerializeField] Camera playerCamera;
  MouseLook mouseLook;
  [SerializeField] float zoom = 30f, nonzoom = 60f;
  [SerializeField] bool isZoomed = false;
  [SerializeField] float mouseSensitivityZoomed = 70f, mouseSensitivityUnzoomed = 100f;
  private void OnDisable()
  {
    ZoomOut();
  }

  private void Start()
  {
    mouseLook = playerCamera.GetComponent<MouseLook>();
  }
  private void Update()
  {
    if (Input.GetButtonDown("Fire2"))
    {
      if (!isZoomed)
      {
        ZoomIn();
      }
    }
    if (Input.GetButtonUp("Fire2"))
    {
      if (isZoomed)
        ZoomOut();
    }
  }


  private void ZoomIn()
  {
    mouseLook.mouseSensitivity = mouseSensitivityZoomed;
    isZoomed = true;
    playerCamera.fieldOfView = zoom;
  }
  private void ZoomOut()
  {
    mouseLook.mouseSensitivity = mouseSensitivityUnzoomed;
    isZoomed = false;
    playerCamera.fieldOfView = nonzoom;
  }
}
