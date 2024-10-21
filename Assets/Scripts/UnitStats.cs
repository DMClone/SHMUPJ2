using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public bool isPlayer;
    public GameObject projectile;
    public int health = 1;
    public int damage = 1;
    public int projectileCountPerShot = 1;
    [Range(0, 1)] public float spreadOffset;

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
            Projectile shotProjectile =
            Instantiate(projectile, new Vector2(transform.position.x - spreadOffset * (projectileCountPerShot - 1) + spreadOffset * 2 * i, transform.position.y), quaternion.identity).gameObject.GetComponent<Projectile>();
            shotProjectile.transform.rotation = gameObject.transform.rotation;
            shotProjectile.isPlayerOwned = isPlayer;
            shotProjectile.direction = transform.up;
            shotProjectile.bulletDamage = damage;
        }
    }
}
