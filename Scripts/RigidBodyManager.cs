using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyManager : MonoBehaviour
{
  [SerializeField] Transform startingPosition;
  Rigidbody rigidBody;
  EnemyAI enemyAI;

  bool currentGravity = true;
  Transform transformObject;

  void Start()
  {
    startingPosition.position = transform.position;
    startingPosition.rotation = transform.rotation;
    rigidBody = GetComponent<Rigidbody>();
    enemyAI = GetComponent<EnemyAI>();
    transformObject = GetComponent<Transform>();
    SetGravity(false);
  }

  void Update()
  {
    if (Mathf.Approximately(transform.position.x, startingPosition.position.x) || Mathf.Approximately(transform.position.y, startingPosition.position.y) || Mathf.Approximately(transform.position.z, startingPosition.position.z))
    {
      print(gameObject.name + " needs to be placed where he was");
      transformObject.position = startingPosition.position;
      transformObject.rotation = startingPosition.rotation;
    }
  }

  public void SetGravity(bool usesGravity)
  {
    if (usesGravity != currentGravity)
    {
      rigidBody.useGravity = usesGravity;
      currentGravity = usesGravity;
    }
  }
}
