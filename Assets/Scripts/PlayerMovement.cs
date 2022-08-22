using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float _Speed = 3;



    InputActions _Input;
    Rigidbody2D _Rigidbody;
    SpriteRenderer _SpriteRenderer;
    Animator _Animator;
    Vector2 _Movement;

    private void Awake()
    {
        _Input = new InputActions();
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
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
        _Animator.SetFloat("Horizontal", _Movement.x);
        _Animator.SetFloat("Vertical", _Movement.y);
        _Animator.SetFloat("Speed", _Movement.sqrMagnitude);
    }

    private void Update()
    {
        _Rigidbody.velocity = _Movement * _Speed;
    }
}
