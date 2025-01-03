using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : EnemyScript
{
    private UnitStats _us;

    public enum shootTo
    {
        downwards,
        toPlayer
    }

    private shootTo shootDirection;
    [Tooltip("In seconds")] public float fireRate;

    public override void Start()
    {
        base.Start();
        event_EnterBounds.AddListener(StartShooting);
        _us = GetComponent<UnitStats>();
        _us.projectileCountPerShot = 1 + Mathf.RoundToInt(GM.instance.currentWave / 2);
        fireRate = fireRate * 1 + 0.05f - (0.05f * GM.instance.currentWave);
        if (Random.Range(0, 2) == 1)
        {
            shootDirection = shootTo.toPlayer;
        }

    }

    public void StartShooting()
    {
        StartCoroutine(UnitShooting());
    }

   public IEnumerator UnitShooting()
    {
        Vector3 shootProjectileTowards;
        if (shootDirection == shootTo.downwards && GetComponent<EnemyWalker>() == null)
        {
            shootProjectileTowards = -transform.up;
        }
        else
        {
            shootProjectileTowards = PlayerPlane.instance.transform.position - transform.position;
        }
        _us.ShootProjectile(shootProjectileTowards, 1);
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(UnitShooting());
    }

    public override void FixedUpdate()
    {
        if (GetComponent<EnemyWalker>() == null)
        {
            base.FixedUpdate();
        }
    }
}
