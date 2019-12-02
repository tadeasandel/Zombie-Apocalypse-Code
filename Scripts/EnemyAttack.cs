using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  PlayerHealth target;
  [SerializeField] float damage = 40f;
  [SerializeField] AudioSource attackSFX;

  void Start()
  {
    target = FindObjectOfType<PlayerHealth>();
  }
  public void AttackHitEvent()
  {
    attackSFX.Play();
    if (target == null)
    {
      return;
    }
    target.ProcessDamage(damage);
    target.GetComponent<DisplayDamage>().ShowDamageImpact();
  }
}
