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
    public int projectileCountPerShot;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (isPlayer)
        {
            GM.instance.Start();
        }
    }

    public void ShootProjectile()
    {
        for (int i = 0; i < projectileCountPerShot; i++)
        {
        Projectile shotProjectile = Instantiate(projectile, transform.position, quaternion.identity).gameObject.GetComponent<Projectile>();
        shotProjectile.transform.rotation = gameObject.transform.rotation;
        shotProjectile.isPlayerOwned = isPlayer;
        shotProjectile.direction = transform.up;
        }
    }
}
