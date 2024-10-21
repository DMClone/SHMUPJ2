using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    protected Rigidbody _rb;
    public Wrap _wrap;
    public bool outOfBounds = true;
    // Start is called before the first frame update
    public virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _wrap = GetComponent<Wrap>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (_wrap.viewportPosition.y >= 0.7f && outOfBounds)
        {
            _rb.velocity = new Vector2(0, -2);
            Debug.Log("y > 0");
        }
        else if (outOfBounds)
        {
            _rb.velocity = Vector2.zero;
        }
    }
}
