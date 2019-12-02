using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  [SerializeField] float chaseRange = 5f;
  [SerializeField] float turnSpeed = 5f;
  [SerializeField] AudioSource aggroedSFX;
  [SerializeField] float aggroRadius = 5f;

  Transform target;
  NavMeshAgent navMeshAgent;
  float distanceToTarget = Mathf.Infinity;
  public bool isProvoked;
  EnemyHealth health;

  void Start()
  {
    navMeshAgent = GetComponent<NavMeshAgent>();
    health = GetComponent<EnemyHealth>();
    target = FindObjectOfType<PlayerHealth>().transform;
  }

  void Update()
  {
    if (health.IsDead() == true)
    {
      enabled = false;
      navMeshAgent.enabled = false;
    }
    distanceToTarget = Vector3.Distance(target.position, transform.position);
    if (isProvoked)
    {
      EngageTarget();
    }
    else if (distanceToTarget <= chaseRange || isProvoked)
    {
      isProvoked = true;
      aggroedSFX.Play();
    }
  }

  public void OnDamageTaken()
  {
    isProvoked = true;
    RaycastHit[] hits = Physics.SphereCastAll(transform.position, aggroRadius, Vector3.up, 0);
    foreach (RaycastHit hit in hits)
    {
      EnemyAI enemyAI = hit.transform.GetComponent<EnemyAI>();
      if (enemyAI == null) continue;
      enemyAI.isProvoked = true;
    }
  }

  public bool IsProvoked()
  {
    return isProvoked;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, chaseRange);
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, aggroRadius);
  }
  private void EngageTarget()
  {
    if (health.IsDead()) { return; }
    FaceTarget();
    if (distanceToTarget >= navMeshAgent.stoppingDistance)
    {
      ChaseTarget();
    }
    if (distanceToTarget <= navMeshAgent.stoppingDistance)
    {
      AttackTarget();
    }
  }
  private void ChaseTarget()
  {
    if (!health.IsDead())
    {
      GetComponent<Animator>().SetBool("Attack", false);
      GetComponent<Animator>().SetTrigger("Move");
      navMeshAgent.SetDestination(target.position);
    }
  }
  private void AttackTarget()
  {

    GetComponent<Animator>().SetBool("Attack", true);
  }

  private void FaceTarget()
  {
    Vector3 direction = (target.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
  }
}

