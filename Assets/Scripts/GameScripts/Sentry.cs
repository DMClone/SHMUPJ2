using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    private UnitStats _us;
    [SerializeField] private GameObject gunPivot;
    private Vector3 nearestTargetDirection;
    public List<Vector3> enemyLocations;

    private void Awake()
    {
        _us = GetComponent<UnitStats>();
        StartCoroutine(SentryShoot());
    }

    private void FixedUpdate()
    {
        getNearestTarget();
    }

    private void getNearestTarget()
    {
        enemyLocations = new List<Vector3>();
        Collider[] Locations = Physics.OverlapSphere(transform.position, 10f);
        for (int i = 0; i < Locations.Length; i++)
        {
            if ((Locations[i].GetComponent<UnitStats>() != null) && !Locations[i].GetComponent<UnitStats>().isPlayer)
            {
                enemyLocations.Add(Locations[i].transform.position);
            }
        }

        Vector3 nearestTarget;
        nearestTarget = new Vector3(100, 100, 100);

        for (int i = 0; i < enemyLocations.Count; i++)
        {
            if (Vector3.Distance(enemyLocations[i], transform.position) < Vector3.Distance(nearestTarget, transform.position))
            {
                nearestTarget = enemyLocations[i];
            }
        }

        nearestTargetDirection = nearestTarget - transform.position;
        gunPivot.transform.up = nearestTargetDirection;

    }

    private IEnumerator SentryShoot()
    {
        yield return new WaitForSeconds(1f);
        _us.ShootProjectile(nearestTargetDirection, 1);
        StartCoroutine(SentryShoot());
    }
}

