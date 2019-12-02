using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderButton : MonoBehaviour
{
  public void LoadLevel(int levelNumber)
  {
    if (levelNumber < 0)
    {
      return;
    }
    SceneManager.LoadScene(levelNumber);
  }
}
