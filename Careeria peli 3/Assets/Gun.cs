using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform FpsCam;
    [SerializeField] private float impactForce = 300f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;

    private float shootRange = 100f;

    private float NextTimeToFire = 0f;


    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out RaycastHit hit, shootRange))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                hit.rigidbody.detectCollisions = true;
            }
            GameObject hitGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitGO, 2f);

            if(hit.transform.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Vihu_1>().TakeDamage(40);
            }
        }
    }

}
