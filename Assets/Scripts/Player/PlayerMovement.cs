using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private CharacterController _characterController;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
        enabled = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveDirection = _characterController.GetMovementNormalizedVector();
        Vector3 moveVector = new Vector3(_transform.position.x + moveDirection.x, _transform.position.y + moveDirection.y, _transform.position.z);
        _transform.position = Vector3.MoveTowards(_transform.position, moveVector, _movementSpeed * Time.deltaTime);
    }
}
