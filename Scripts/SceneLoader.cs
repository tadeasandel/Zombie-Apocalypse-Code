using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField] Canvas gameOverCanvas;
  public void ReloadGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    gameOverCanvas.enabled = false;
    Time.timeScale = 1;
  }
  public void QuitGame()
  {
    Application.Quit();
  }
}
