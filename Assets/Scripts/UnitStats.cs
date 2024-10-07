using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public bool isPlayer;
    public GameObject projectile;
    public int health;
    public int damage;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ShootProjectile()
    {
        Projectile shotProjectile = Instantiate(projectile, transform.position, quaternion.identity).gameObject.GetComponent<Projectile>();
        shotProjectile.transform.rotation = gameObject.transform.rotation;
        shotProjectile.isPlayerOwned = isPlayer;
        shotProjectile.direction = transform.up;
    }
}
