using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerPlane : MonoBehaviour
{
    public static PlayerPlane instance;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] public PlaneInput _input;
    private UnitStats _us;
    private Vector2 moveDirection;
    [SerializeField][Range(1, 10)] private int moveSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _input = new PlaneInput();
        _input.Player.Move.Enable();
        _input.Player.Fire.Enable();

        _rb = GetComponent<Rigidbody>();
        _us = GetComponent<UnitStats>();
    }

    private void OnEnable()
    {
        _input.Player.Move.performed += MoveAction;
        _input.Player.Move.canceled += MoveStop;
        _input.Player.Fire.performed += FireAction;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= MoveAction;
        _input.Player.Move.canceled -= MoveStop;
        _input.Player.Fire.performed -= FireAction;
    }

    public void MoveAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            moveDirection = callbackContext.ReadValue<Vector2>();
        }
    }
    public void MoveStop(InputAction.CallbackContext callbackContext)
    {
        moveDirection = Vector2.zero;

    }

    public void FireAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            _us.ShootProjectile(transform.up);
        }
    }

    public void PauseAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("Paused");
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
