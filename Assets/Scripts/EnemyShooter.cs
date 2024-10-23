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
        if (GetComponent<EnemyWalker>() == null)
        {
            base.Start();
        }
        event_EnterBounds.AddListener(StartShooting);
        _us = GetComponent<UnitStats>();
        _us.projectileCountPerShot = 1 + Mathf.RoundToInt(GM.instance.currentWave / 2);
        if (Random.Range(0, 2) == 1)
        {
            shootDirection = shootTo.toPlayer;
        }

    }

    private void StartShooting()
    {
        StartCoroutine(UnitShooting());
    }

    IEnumerator UnitShooting()
    {
        Vector3 shootProjectileTowards;
        if (shootDirection == shootTo.downwards)
        {
            shootProjectileTowards = -transform.up;
        }
        else
        {
            shootProjectileTowards = PlayerPlane.instance.transform.position - transform.position;
        }
        _us.ShootProjectile(shootProjectileTowards);
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(UnitShooting());
    }
}
