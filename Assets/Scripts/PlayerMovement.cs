using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Vector2 moveDirection;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = GameInput.Instance.GetMoveVectorNormilized();
        transform.Translate(movementSpeed * Time.deltaTime * moveDirection);
    }
}
