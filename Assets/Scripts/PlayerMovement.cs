using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float _Speed = 3;

    InputActions _Input;
    Rigidbody2D _Rigidbody;
    Vector2 _Movement;

    private void Awake()
    {
        _Input = new InputActions();
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _Input.Enable();

        _Input.Player.Move.performed += OnMovement;
        _Input.Player.Move.canceled += OnMovement;
    }

    private void OnDisable()
    {
        _Input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext ctx)
    {
        _Movement = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        _Rigidbody.velocity = _Movement * _Speed;
    }
}
