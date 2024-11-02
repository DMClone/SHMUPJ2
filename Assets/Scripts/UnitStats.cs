using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [SerializeField] private GameObject deathParticles;
    public bool isPlayer;
    [HideInInspector] public bool isDead;
    public GameObject projectile;
    public int health = 1;
    public int maxHealth;
    public int damage = 1;
    public int projectileCountPerShot = 1;
    [Range(0, 1)] public float spreadOffset;

    private void Awake()
    {
        maxHealth = health;
    }

    public void TakeDamage(int damage)
    {
        if (gameObject != null)
        {
            health -= damage;
            if (health <= 0)
            {
                isDead = true;
                Destroy(gameObject);
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                if (!isPlayer)
                {
                    GM.instance.enemiesAlive--;
                    GM.instance.enemiesKilled++;
                    GM.instance.PowerupSpawnCheck(transform.position);
                }
            }

            if (isPlayer)
            {
                GM.instance.RefreshLiveCanvas(); // Set health UI to current health
            }
        }
    }

    
    public void ShootProjectile(Vector3 projectileDirection)
    {
        for (int i = 0; i < projectileCountPerShot; i++)
        {
            Projectile shotProjectile =
            Instantiate(projectile, new Vector2(transform.position.x - spreadOffset * (projectileCountPerShot - 1) + spreadOffset * 2 * i, transform.position.y), quaternion.identity).gameObject.GetComponent<Projectile>();
            shotProjectile.isPlayerOwned = isPlayer;
            shotProjectile.bulletDamage = damage;
            if (projectileDirection == null)
            {
                Debug.Log("Insert a rotation to the bullet");
            }
            else
            {
                shotProjectile.direction = projectileDirection.normalized;
            }
        }
    }
}
