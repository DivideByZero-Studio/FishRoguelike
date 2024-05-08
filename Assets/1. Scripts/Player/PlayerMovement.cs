using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    public Vector2 LastMoveDirection { get; private set; }
    private Vector2 _moveDirection;

    private Rigidbody2D _rigidbody;

    [Inject] private GameInput gameInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void Update()
    {
        _moveDirection = gameInput.GetMovementNormalizedVector();
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
