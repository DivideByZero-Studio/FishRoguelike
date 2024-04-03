using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Enable();
    }

    public Vector2 GetMovementNormalizedVector()
    {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        return direction;
    }
}
