using System;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public event Action OnAttacked;
    public event Action OnLeft;

    public static readonly float AttackRange = 1.5f;

    public abstract void Attack();

    public abstract void StartAttack(Transform _playerTransform);

    protected void InvokeOnAttacked()
    {
        OnAttacked?.Invoke();
    }

    protected void InvokeOnLeft()
    {
        OnLeft?.Invoke();
    }
}
