using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    public bool isPlayerOwned;
    [HideInInspector] public int bulletDamage;
    public float projectileSpeed = 1;
    [HideInInspector] public Vector3 direction;
    public float positionOffset;
    bool isDead;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        transform.up = direction;
        transform.position += transform.up * positionOffset;
    }

    private void FixedUpdate()
    {
        _rb.velocity = direction * 5 * projectileSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<UnitStats>() != null) && other.GetComponent<UnitStats>().isPlayer != isPlayerOwned && !isDead
        && !other.GetComponent<UnitStats>().isDead)
        {
            if ((other.GetComponent<HunterOverride>() == null) || !other.GetComponent<HunterOverride>().dodging)
            {
                other.GetComponent<UnitStats>().TakeDamage(bulletDamage);
                Destroy(gameObject);
                isDead = true;
            }
            else if ((other.GetComponent<HunterOverride>() != null) && other.GetComponent<HunterOverride>().dodging)
            {
                other.GetComponent<HunterOverride>().ProjectileDodge();
            }
        }
    }
}
