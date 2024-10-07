using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    public bool isPlayerOwned;
    public int bulletDamage;
    public Vector3 direction;

    void Start()
    {
        Debug.Log("Start");
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _rb.velocity = direction * 4;
    }
}
