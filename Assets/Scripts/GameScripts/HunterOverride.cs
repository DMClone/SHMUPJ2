using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterOverride : PlayerPlane
{
    public bool dodging;
    private Animator _an;
    private int dodgeStacks; // amount of bullets dodged when 
    private bool dodged;


    public override void Awake()
    {
        base.Awake();
        _an = GetComponent<Animator>();
    }

    public override void MoveAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && !dodging)
        {
            moveDirection = callbackContext.ReadValue<Vector2>();
        }
    }

    public override void MoveStop(InputAction.CallbackContext callbackContext)
    {
        if (!dodging)
        {
            moveDirection = Vector2.zero;
        }
    }

    public override IEnumerator Special()
    {
        specialOnCooldown = true;
        StartCoroutine(Dodge());
        yield return new WaitForSeconds(10f);
        specialOnCooldown = false;
    }


    IEnumerator Dodge()
    {
        Debug.Log("Dodged");
        dodging = true;
        dodged = false;
        _an.SetTrigger("Dodge");
        yield return new WaitForSeconds(1);
        dodging = false;
    }

    public void ProjectileDodge()
    {
        if (!dodged && dodgeStacks < 5)
        {
            dodgeStacks++;
        }
        else
        {
            Regenerate();
        }
    }
}
