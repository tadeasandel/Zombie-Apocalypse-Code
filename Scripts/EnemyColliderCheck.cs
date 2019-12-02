using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderCheck : MonoBehaviour, IShotable
{
  [SerializeField] EnemyHealth health;
  [SerializeField] bool isHeadShot;

  EnemyHealth IShotable.GetEnemyHealthComponent()
  {
    return health;
  }
  public bool IsHeadShot()
  {
    return isHeadShot;
  }
}