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
        Instantiate(sentry, new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z), quaternion.identity);
        yield return new WaitForSeconds(10f);
        specialOnCooldown = false;
    }
}
