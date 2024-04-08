using System;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public event Action OnAttack;
    public event Action OnLeft;

    [SerializeField] protected float _attackRange;

    public abstract void Attack();

    public abstract void StartAttack(Transform _playerTransform);

    public virtual void StopAttack()
    {

    }
    protected void InvokeOnAttack()
    {
        OnAttack?.Invoke();
    }

    protected void InvokeOnLeft()
    {
        OnLeft?.Invoke();
    }

    public float GetAttackRange()
    {
        return _attackRange;
    }
}
