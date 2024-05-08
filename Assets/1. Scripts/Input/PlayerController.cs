using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    public event Action OnPrimaryAttack;
    public event Action OnSecondaryAttack;
    public event Action OnDash;

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

    private void Primary_attack_performed(InputAction.CallbackContext context)
    {
        OnPrimaryAttack?.Invoke();
    }

    private void Secondary_attack_performed(InputAction.CallbackContext context)
    {
        OnSecondaryAttack?.Invoke();
    }

    private void Dash_performed(InputAction.CallbackContext context)
    {
        OnDash?.Invoke();
    }

    private void OnEnable()
    {
        _input.Player.PrimaryAttack.performed += Primary_attack_performed;
        _input.Player.SecondaryAttack.performed += Secondary_attack_performed;
        _input.Player.Dash.performed += Dash_performed;
    }

    private void OnDisable()
    {
        _input.Player.PrimaryAttack.performed -= Primary_attack_performed;
        _input.Player.SecondaryAttack.performed -= Secondary_attack_performed;
        _input.Player.Dash.performed -= Dash_performed;
    }
}
