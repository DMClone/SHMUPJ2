using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerPlane : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] public PlaneInput _input;
    private UnitStats _us;
    private InputAction move;
    private InputAction fire;
    private Vector2 moveDirection;
    [SerializeField] [Range(1, 10)] private int moveSpeed;


    private void Awake()
    {
        _input = new PlaneInput();
        _rb = GetComponent<Rigidbody>();
        _us = GetComponent<UnitStats>();
    }


    private void OnEnable()
    {
        _input.Player.Move.performed += MoveAction;
        _input.Player.Fire.performed += FireAction;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= MoveAction;
        _input.Player.Fire.performed -= FireAction;
    }

    public void MoveAction(InputAction.CallbackContext callbackContext)
    {
        moveDirection = callbackContext.ReadValue<Vector2>();
    }

    public void FireAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            _us.ShootProjectile();
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
