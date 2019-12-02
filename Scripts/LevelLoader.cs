using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
  [SerializeField] int maxSceneIndex = 3;

  int currentSceneIndex;
  void Start()
  {
    currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      LoadNextScene();
    }
  }

  private void LoadNextScene()
  {
    if (currentSceneIndex < 0)
    {
      return;
    }
    if (currentSceneIndex + 1 >= maxSceneIndex)
    {
      SceneManager.LoadScene(0);
    }
    SceneManager.LoadScene(currentSceneIndex + 1);
  }
}
