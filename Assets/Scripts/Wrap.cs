using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 moveAdjust = Vector3.zero;
        if (viewportPosition.x < 0)
        {
            moveAdjust.x += 1;
        }
        else if (viewportPosition.x > 1)
        {
            moveAdjust.x -= 1;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjust);
    }
}
