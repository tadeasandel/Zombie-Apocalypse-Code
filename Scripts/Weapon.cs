using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  [SerializeField] Camera FPCamera;
  [SerializeField] float range = 100f;
  [SerializeField] float damage = 20f;
  [SerializeField] float headShotDamage = 50f;
  [SerializeField] ParticleSystem muzzleFlash;
  [SerializeField] GameObject hitEffect;
  [SerializeField] Ammo ammoSlot;
  [SerializeField] AmmoType ammoType;
  [SerializeField] float shootTimeDelay = 5f;
  [SerializeField] TextMeshProUGUI ammoText;
  [SerializeField] float knockBack;
  [SerializeField] AudioSource shootSFX;
  [SerializeField] AudioSource reloadSFX = null;
  [SerializeField] AudioSource headShotSFX;
  [SerializeField] LayerMask headMask;
  [SerializeField] LayerMask bodyMask;

  bool canShoot = true;
  public bool strokeHeadShot;
  [SerializeField] bool hasHitHeadShot;
  [SerializeField] bool hasHitBody;
  [SerializeField] bool hasHitAnythingElse;

  private void OnEnable()
  {
    canShoot = true;
  }
  void Update()
  {
    DisplayAmmo();
    if (Input.GetButtonDown("Fire1") && canShoot)
    {
      StartCoroutine(Shoot());
    }
  }
  private void DisplayAmmo()
  {
    int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
    ammoText.text = currentAmmo.ToString();
  }
  IEnumerator Shoot()
  {
    canShoot = false;
    if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
    {
      PlayMuzzleFlash();
      shootSFX.Play();
      ProcessRayCast();
      ammoSlot.ReduceCurrentAmmo(ammoType);
    }
    yield return new WaitForSeconds(shootTimeDelay);
    canShoot = true;
  }

  private void PlayMuzzleFlash()
  {
    muzzleFlash.Play();
  }

  private void ProcessRayCast()
  {
    RaycastHit hit;
    hasHitHeadShot = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, headMask);
    hasHitBody = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, bodyMask);
    hasHitAnythingElse = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range);
    if (hasHitBody || hasHitHeadShot)
    {
      CreateHitImpact(hit);
      if (hasHitBody)
      {
        strokeHeadShot = false;
        print("Hit Body");
      }
      else if (hasHitHeadShot)
      {
        strokeHeadShot = true;
        print("Hit Head");
      }
      if (hit.transform.tag == "Enemy")
      {
        print("Hit Enemy");
        if (hasHitHeadShot)
        {
          EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
          target.TakeDamage(headShotDamage, ammoSlot.gameObject, knockBack, hit);
          headShotSFX.Play();
        }
        else if (hasHitBody)
        {
          EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
          target.TakeDamage(damage, ammoSlot.gameObject, knockBack, hit);
        }
      }
    }
    else if (hasHitAnythingElse)
    {
      CreateHitImpact(hit);
    }

    //   RaycastHit hit;
    //   hasHitHeadShot = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, headMask);
    //   hasHitBody = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, bodyMask);
    //   if (hasHitBody || hasHitHeadShot)
    //   {
    //     CreateHitImpact(hit);
    //     EnemyHealth target = null;
    //     if (hit.transform.tag == "Enemy")
    //     {
    //       print("it's head");
    //       if (hasHitHeadShot)
    //       {
    //         print("stroke Headshot");
    //         strokeHeadShot = true;
    //       }
    //       else if (hasHitBody)
    //       {
    //         print("stroke body");
    //         strokeHeadShot = false;
    //       }
    //       if (target == null)
    //       {
    //         return;
    //       }
    //       target = hit.transform.GetComponent<EnemyHealth>();
    //     }
    //   }
    //   if (strokeHeadShot)
    //   {
    //     target.TakeDamage(headShotDamage, ammoSlot.gameObject, knockBack, hit);
    //     print("headshot");
    //     headShotSFX.Play();
    //   }
    //   else
    //   {
    //     print("no headshot");
    //     target.TakeDamage(damage, ammoSlot.gameObject, knockBack, hit);
    //   }
    // }
  }
  private void CreateHitImpact(RaycastHit hit)
  {
    // if (hit.transform.tag == "Iron")
    // {
    //   GameObject impact = Instantiate(hitEffect[0], hit.point, Quaternion.LookRotation(hit.normal));
    //   Destroy(impact, 5f);
    // }
    // else if (hit.transform.tag == "Grass")
    // {
    //   GameObject impact = Instantiate(hitEffect[2], hit.point, Quaternion.LookRotation(hit.normal));
    //   Destroy(impact, 5f);
    // }
    // else if (hit.transform.tag == "Head" || hit.transform.tag == "Enemy")
    // {
    //   GameObject impact = Instantiate(hitEffect[1], hit.point, Quaternion.LookRotation(hit.normal));
    //   Destroy(impact, 5f);
    // }
    GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    Destroy(impact, 0.1f);
  }
}
