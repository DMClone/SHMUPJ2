using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuilderOverride : PlayerPlane
{
    [SerializeField] private GameObject sentry;

    public override IEnumerator Special()
    {
        specialOnCooldown = true;
        Instantiate(sentry, transform.position, quaternion.identity);
        yield return new WaitForSeconds(10f);
        specialOnCooldown = false;
    }
}
