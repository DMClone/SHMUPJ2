using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyWalker : EnemyScript
{
    [Tooltip("X is speed")]
    public Vector2 moveDirection;

    public override void Start()
    {
        base.Start();
        moveDirection.x = moveDirection.x * 1 + (GM.instance.currentWave / 4);

        event_EnterBounds.AddListener(StartMovement);
        if (Random.Range(0, 2) == 1)
        {
            moveDirection = new Vector2(-moveDirection.x, moveDirection.y);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!outOfBounds && (_wrap.viewportPosition.x <= 0 && moveDirection.x <= 0 || _wrap.viewportPosition.x >= 1 && moveDirection.x >= 0))
        {
            moveDirection = new Vector2(-moveDirection.x, moveDirection.y);
            _rb.velocity = moveDirection;
        }
    }

    void StartMovement()
    {
        moveDirection = new Vector2(moveDirection.x, moveDirection.y);
        _rb.velocity = moveDirection;
    }
}
