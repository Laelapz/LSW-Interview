using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 3;
    [SerializeField] private CheckSurroundings _checkSurroundings;

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
        _input.Player.Interact.performed += OnInteracting;
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

    private void OnInteracting(InputAction.CallbackContext ctx)
    {
        var collision = _checkSurroundings.SearchForInteractable();
        var interactable = collision.rigidbody.GetComponent<IInteractable>();

        if (interactable == null) return;
        interactable.Interact();

    }

    private void Update()
    {
        _rigidbody.velocity = _movement * _speed;
    }
}
