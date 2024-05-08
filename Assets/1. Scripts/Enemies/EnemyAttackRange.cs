using System;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    public event Action OnEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerVisuals>(out var player))
        {
            OnEntered?.Invoke();
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
