using System;
using UnityEngine;

public class MeleeEnemyAttack : EnemyAttack
{
    public event Action OnAttacked;

    [SerializeField] private float _cooldown;

    private Timer _attackTimer;

    private void Awake()
    {
        _attackTimer = new Timer(_cooldown);
    }

    private void Update()
    {
        _attackTimer.DecreaseTime();
    }

    public override void Attack()
    {
        if (_attackTimer.IsReady)
        {
            Debug.Log("Attack");
            OnAttacked?.Invoke();
            _attackTimer.Reset();
        }
    }
}
