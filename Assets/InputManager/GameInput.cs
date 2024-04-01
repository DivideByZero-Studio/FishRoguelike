using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInput _playerInput;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
    }

    public Vector2 GetMoveVectorNormilized() => _playerInput.Player.Move.ReadValue<Vector2>();
}
