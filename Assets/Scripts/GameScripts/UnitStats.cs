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
    public float invincibilityTime;
    private bool invincible;

    private void Awake()
    {
        maxHealth = health;
    }

    public void TakeDamage(int damage)
    {
        if (gameObject != null && !invincible)
        {
            health -= damage;
            if (health <= 0)
            {
                isDead = true;
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                if (!isPlayer)
                {
                    GM.instance.AddScore(100);
                    GM.instance.enemiesAlive--;
                    GM.instance.enemiesKilled++;
                    GM.instance.PowerupSpawnCheck(transform.position);
                    Destroy(gameObject);
                }
                else if (GetComponent<Sentry>() != null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    GM.instance.GameEnd();
                }
            }

            if (isPlayer && GetComponent<Sentry>() == null)
            {
                LivesCanvas.instance.UpdateHealthBar(health); ; // Set health UI to current health
                StartCoroutine(Invincibility());
            }
        }
    }

    public IEnumerator Invincibility()
    {
        invincible = true;
        SpriteRenderer _SR = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _SR.color = new Color(_SR.color.r, _SR.color.g, _SR.color.b, 0.5f);
        Debug.Log(_SR.color);
        yield return new WaitForSeconds(invincibilityTime);
        _SR.color = new Color(_SR.color.r, _SR.color.g, _SR.color.b, 1);
        invincible = false;
    }

    public void ShootProjectile(Vector3 projectileDirection, int projectileMultiplier)
    {
        projectileCountPerShot *= projectileMultiplier;
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
            if (GetComponent<Sentry>() != null)
            {
                shotProjectile.positionOffset = 0.55f;
            }
        }
        projectileCountPerShot /= projectileMultiplier;
    }
}
