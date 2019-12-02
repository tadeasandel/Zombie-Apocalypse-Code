using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsHandler : MonoBehaviour
{
  [SerializeField] Canvas pauseMenuCanvas;
  bool isInMenu = false;

  void Start()
  {
    pauseMenuCanvas.enabled = false;
    Time.timeScale = 1;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (!isInMenu)
      {
        isInMenu = true;
        Weapon[] weapons = FindObjectsOfType<Weapon>();
        foreach (Weapon weapon in weapons)
        {
          if (weapons != null)
          {
            weapon.enabled = false;
          }
        }
        pauseMenuCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
      }
      else
      {
        isInMenu = false;
        ResumeGame();
      }
    }
  }
  public void ResumeGame()
  {
    Weapon[] weapons = FindObjectsOfType<Weapon>();
    foreach (Weapon weapon in weapons)
    {
      if (weapons != null)
      {
        weapon.enabled = true;
      }
    }
    pauseMenuCanvas.enabled = false;
    Time.timeScale = 1;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    FindObjectOfType<WeaponSwitcher>().enabled = true;
  }
}
