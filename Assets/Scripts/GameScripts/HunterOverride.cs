using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterOverride : PlayerPlane
{
    [SerializeField] public bool dodging;
    private Animator _an;
    private Vector2 dodgeVelocity;
    [SerializeField] private int dodgeStacks; // amount of bullets dodged when 
    [SerializeField] private bool dodged;


    public override void Awake()
    {
        base.Awake();
        _an = GetComponent<Animator>();
    }

    public override void MoveAction(InputAction.CallbackContext callbackContext)
    {
        moveDirection = callbackContext.ReadValue<Vector2>();
    }

    public override void FixedUpdate()
    {
        if (!dodging)
        {
            _rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
        else
        {
            _rb.velocity = new Vector2(dodgeVelocity.x * moveSpeed, dodgeVelocity.y * moveSpeed);
        }
    }

    public override void MoveStop(InputAction.CallbackContext callbackContext)
    {
        moveDirection = Vector2.zero;
    }

    public override IEnumerator Special()
    {
        specialOnCooldown = true;
        StartCoroutine(Dodge());
        yield return new WaitForSeconds(5f);
        specialOnCooldown = false;
    }

    IEnumerator Dodge()
    {
        Debug.Log("Dodged");
        dodgeVelocity = moveDirection;
        dodging = true;
        dodged = false;
        _an.SetTrigger("Dodge");
        yield return new WaitForSeconds(1);
        dodgeStacks = 0;
        dodging = false;
    }

    public void ProjectileDodge()
    {
        if (!dodged && dodgeStacks < 5)
        {
            Debug.Log("Gained dodge stack");
            dodgeStacks++;
        }
        else
        {
            Regenerate();
            dodged = true;
        }
    }
}
