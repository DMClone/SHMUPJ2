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
    bool isDead;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        transform.up = direction;
    }

    private void FixedUpdate()
    {
        _rb.velocity = direction * 5 * projectileSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<UnitStats>() != null) && other.GetComponent<UnitStats>().isPlayer != isPlayerOwned && !isDead && !other.GetComponent<UnitStats>().isDead)
        {
            other.GetComponent<UnitStats>().TakeDamage(bulletDamage);
            Destroy(gameObject);
            isDead = true;
        }
    }
}
