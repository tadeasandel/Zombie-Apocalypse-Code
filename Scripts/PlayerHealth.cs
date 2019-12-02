using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] float playerHealth = 100f;
  [SerializeField] TextMeshProUGUI healthUI;

  public void ProcessDamage(float damage)
  {
    playerHealth = playerHealth - damage;
    if (playerHealth <= 0)
    {
      GetComponent<DeathHandler>().HandleDeath();
    }
  }
  public float GetHealth()
  {
    return playerHealth;
  }

  private void Update()
  {
    healthUI.text = GetHealth().ToString();
  }
}
