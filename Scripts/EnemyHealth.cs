using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour, IShotable
{
  [SerializeField] float hitPoints = 100f;
  [SerializeField] float maxHealth = 100f;
  [SerializeField] AudioSource deathSFX;
  [SerializeField] float knockBackDuration = 0.5f;
  [SerializeField] RectTransform healthTransform;
  [SerializeField] Canvas healthBarCanvas;
  [SerializeField] BoxCollider headCollider;
  [SerializeField] BoxCollider bodyCollider;

  [SerializeField] LayerMask groundMask;
  [SerializeField] float groundDistance;

  bool isDead = false;
  [SerializeField] bool isGrounded;
  GameObject player;
  [SerializeField] Transform groundCheck;
  NavMeshAgent navMeshAgent;
  Rigidbody rigidBody;
  RigidBodyManager gravityManager;

  public bool IsDead()
  {
    return isDead;
  }

  private void Awake()
  {
    navMeshAgent = GetComponent<NavMeshAgent>();
    rigidBody = GetComponent<Rigidbody>();
    gravityManager = GetComponent<RigidBodyManager>();
  }

  public void TakeDamage(float damage, GameObject player, float knockBack, RaycastHit hit)
  {
    this.player = player;
    hitPoints = hitPoints - damage;
    if (knockBack > 0)
    {
      StartCoroutine(KnockBack(player.transform, knockBack, hit));
    }
    GetComponent<EnemyAI>().OnDamageTaken();
    if (hitPoints <= 0)
    {
      Die();
    }
  }

  public float GetHealth()
  {
    return hitPoints;
  }

  public float GetHealthPercentage()
  {
    return 100 * GetHealth() / GetMaxHealth();
  }

  public float GetMaxHealth()
  {
    return maxHealth;
  }

  private void Update()
  {
    DisplayHealth();
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
  }

  private void DisplayHealth()
  {
    float healthPercentage = GetHealthPercentage();
    if (healthPercentage >= 100)
    {
      healthBarCanvas.enabled = false;
    }
    else if (healthPercentage < 100 && healthPercentage > 0)
    {
      healthBarCanvas.enabled = true;
      healthTransform.localScale = new Vector3(healthPercentage / 100, 1, 1);
    }
    else
    {
      healthBarCanvas.enabled = false;
    }
  }

  private void Die()
  {
    if (isDead)
    {
      return;
    }
    isDead = true;
    GetComponent<Animator>().SetTrigger("Die");
    deathSFX.Play();
    bodyCollider.enabled = false;
    headCollider.enabled = false;
  }

  IEnumerator KnockBack(Transform player, float knockBack, RaycastHit hit)
  {
    print(knockBack);
    gravityManager.SetGravity(true);
    Vector3 direction = transform.position - player.position;
    navMeshAgent.enabled = false;
    yield return null;
    rigidBody.AddForce(direction * knockBack);
    while (!isGrounded)
    {
      print("Is not grounded");
      yield return new WaitForSeconds(Time.deltaTime * knockBackDuration);
    }
    navMeshAgent.enabled = true;
    gravityManager.SetGravity(false);
  }

  public EnemyHealth GetEnemyHealthComponent()
  {
    return this;
  }
  public bool HeadShot()
  {
    print("no Headshot");
    return false;
  }
}
