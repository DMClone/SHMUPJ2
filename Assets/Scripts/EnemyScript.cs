using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    protected Rigidbody _rb;
    public Wrap _wrap;
    public bool outOfBounds;
    [Tooltip("At which Y point to stop going downwards")]
    [Range(0.5f, 1f)] public float viewportYStop;

    protected UnityEvent event_EnterBounds;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _wrap = GetComponent<Wrap>();

        if (event_EnterBounds == null)
        {
            event_EnterBounds = new UnityEvent();
        }
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (_wrap.viewportPosition.y >= 0.85f && outOfBounds)
        {
            _rb.velocity = new Vector2(0, -2);
        }
        else if (outOfBounds)
        {
            _rb.velocity = Vector2.zero;
            outOfBounds = false;
            event_EnterBounds.Invoke();
        }
    }
}
