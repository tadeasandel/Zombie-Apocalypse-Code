﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
  public void ExitGame()
  {
    print("Quiting game");
    Application.Quit();
  }
}
