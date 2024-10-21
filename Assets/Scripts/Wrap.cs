using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    public Vector3 viewportPosition;

    public enum onBoundsExitEvents
    {
        Nothing,
        Wrap,
        DestroySelf,
    }

    public onBoundsExitEvents onBoundsExit;

    private void Awake()
    {
        viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
    }

    private void FixedUpdate()
    {
        viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 moveAdjust = Vector3.zero;

        if (onBoundsExit == onBoundsExitEvents.DestroySelf && (viewportPosition.x <= 0
        || viewportPosition.x >= 1 || viewportPosition.y <= 0 || viewportPosition.y >= 1))
        {
            Destroy(gameObject);
        }
        else if (viewportPosition.x < 0)
        {
            if (onBoundsExit == onBoundsExitEvents.Wrap)
            {
                moveAdjust.x += 1;
            }
        }
        else if (viewportPosition.x > 1)
        {
            if (onBoundsExit == onBoundsExitEvents.Wrap)
            {
                moveAdjust.x -= 1;
            }
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjust);
    }
}
