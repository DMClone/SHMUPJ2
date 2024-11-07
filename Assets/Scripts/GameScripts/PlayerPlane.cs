using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerPlane : MonoBehaviour
{
    public static PlayerPlane instance;

    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected PlaneInput _input;
    protected UnitStats _us;
    [SerializeField] protected Sprite[] shipSprites;
    protected int shipLevel = 1;
    protected Vector2 moveDirection;
    [SerializeField][Range(1, 10)] protected int moveSpeed;
    protected bool specialOnCooldown;

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _input = new PlaneInput();
        _input.Player.Move.Enable();
        _input.Player.Fire.Enable();
        _input.Player.SpecialFire.Enable();

        _rb = GetComponent<Rigidbody>();
        _us = GetComponent<UnitStats>();
    }

    protected void OnEnable()
    {
        _input.Player.Move.performed += MoveAction;
        _input.Player.Move.canceled += MoveStop;
        _input.Player.Fire.performed += FireAction;
        _input.Player.SpecialFire.performed += SpecialAction;
    }

    protected void OnDisable()
    {
        _input.Player.Move.performed -= MoveAction;
        _input.Player.Move.canceled -= MoveStop;
        _input.Player.Fire.performed -= FireAction;
        _input.Player.SpecialFire.performed -= SpecialAction;
    }

    public virtual void MoveAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            moveDirection = callbackContext.ReadValue<Vector2>();
        }
    }

    public virtual void FixedUpdate()
    {
        _rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public virtual void MoveStop(InputAction.CallbackContext callbackContext)
    {
        moveDirection = Vector2.zero;
    }

    public void FireAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            _us.ShootProjectile(transform.up, 1);
        }
    }

    public void PauseAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("Paused");
        }
    }

    public void SpecialAction(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Special");
        if (callbackContext.performed && !specialOnCooldown)
        {
            StartCoroutine(Special());
        }
    }

   public virtual IEnumerator Special()
    {
        specialOnCooldown = true;
        _us.ShootProjectile(transform.up, 2);
        yield return new WaitForSeconds(10f);
        specialOnCooldown = false;
    }

    public void Regenerate()
    {
        if (_us.health < _us.maxHealth)
        {
            _us.health++;
            LivesCanvas.instance.UpdateHealthBar(_us.health); // Set health UI to current health
        }
    }

    public void UpgradeShip()
    {
        if (shipLevel < shipSprites.Length)
        {
            _us.projectileCountPerShot++;
            shipLevel++;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shipSprites[shipLevel - 1];
        }
    }
}
