using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

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
        ReadMoveVector();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadMoveVector()
    {
        _moveDirection = _characterController.GetMovementNormalizedVector();
    }

    private void Move()
    {
        var newPosition = _rigidbody.position + _movementSpeed * Time.fixedDeltaTime * _moveDirection;
        _rigidbody.MovePosition(newPosition);
    }
}
