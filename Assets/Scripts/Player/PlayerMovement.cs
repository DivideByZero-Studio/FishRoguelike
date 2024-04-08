using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    public Vector2 LastMoveDirection { get; private set; }

    private PlayerController _characterController;
    private Rigidbody2D _rigidbody;

    private Vector2 _moveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterController = GetComponent<PlayerController>();
        enabled = false;
    }

    private void Update()
    {
        _moveDirection = _characterController.GetMovementNormalizedVector();
        if (_moveDirection != Vector2.zero)
            LastMoveDirection = _moveDirection;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var newPosition = _rigidbody.position + _movementSpeed * Time.fixedDeltaTime * _moveDirection;
        _rigidbody.MovePosition(newPosition);
    }
}
