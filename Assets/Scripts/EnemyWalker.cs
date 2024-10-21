using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyWalker : EnemyScript
{
    public Vector2 moveDirection;

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!outOfBounds && (_wrap.viewportPosition.x < 0 || _wrap.viewportPosition.x > 1))
        {
            moveDirection = new Vector2(moveDirection.x, -moveDirection.y);
            _rb.velocity = moveDirection;
        }
    }
}
