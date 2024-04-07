using System;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    public event Action OnEntered;
    public event Action OnExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerStateMachine>(out var player))
        {
            OnEntered?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerStateMachine>(out var player))
        {
            OnExited?.Invoke();
        }
    }
}
