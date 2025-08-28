using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private PlayerControls controls;

    private Vector3 inputMove;
    private Rigidbody2D rb;
    private float speed = 5f;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Input.Move.performed += Handle_MovePerformed;
        controls.Input.Move.canceled += Handle_MoveCancelled;
    }


    private void OnDisable()
    {
        controls.Disable();

        controls.Input.Move.performed -= Handle_MovePerformed;
        controls.Input.Move.canceled -= Handle_MoveCancelled;
    }


    private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();    
    }
    private void Handle_MoveCancelled(InputAction.CallbackContext obj)
    {
        inputMove = Vector2.zero;
    }
    
    private void Update()
    {
        rb.linearVelocity = inputMove * speed;
    }
}
