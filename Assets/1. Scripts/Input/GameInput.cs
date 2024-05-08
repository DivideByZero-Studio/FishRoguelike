using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameInput : IInitializable, IDisposable
{
    private PlayerInput playerInput;
    public event Action OnPrimaryAttack;
    public event Action OnSecondaryAttack;
    public event Action OnDash;

    public void Initialize()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        Subscribe();
    }

    public void Dispose()
    {
        Unsubscribe();
    }

    public Vector2 GetMovementNormalizedVector()
    {
        Vector2 direction = playerInput.Player.Move.ReadValue<Vector2>();
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

    private void Subscribe()
    {
        playerInput.Player.PrimaryAttack.performed += Primary_attack_performed;
        playerInput.Player.SecondaryAttack.performed += Secondary_attack_performed;
        playerInput.Player.Dash.performed += Dash_performed;
    }

    private void Unsubscribe()
    {
        playerInput.Player.PrimaryAttack.performed -= Primary_attack_performed;
        playerInput.Player.SecondaryAttack.performed -= Secondary_attack_performed;
        playerInput.Player.Dash.performed -= Dash_performed;
    }
}
