using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 3;

    private InputActions _input;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _movement;

    private void Awake()
    {
        _input = new InputActions();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Move.performed += OnMovement;
        _input.Player.Move.canceled += OnMovement;
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>();
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private void Update()
    {
        _rigidbody.velocity = _movement * _speed;
    }
}
