using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainStateMachine : EnemyStateMachine
{
    [SerializeField, Range(0, 1)] private float _attackChance;

    private EnemyAttack _attack;
    private EnemyIdle _idle;
    private EnemyWalk _walking;
    private EnemyDeath _death;
    private Health _health;

    private System.Random _random;

    protected override void Awake()
    {
        base.Awake();
        
        _random = new System.Random();

        _attack = GetComponent<EnemyAttack>();
        _idle = GetComponent<EnemyIdle>();
        _walking = GetComponent<EnemyWalk>();
        _death = GetComponent<EnemyDeath>();
        _health = GetComponent<Health>();
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(MeleeEnemyBehaviourIdle)] = new MeleeEnemyBehaviourIdle(_idle);
        _behavioursMap[typeof(CaptainBehaviourAttack)] = new CaptainBehaviourAttack(_attack, _playerTransform);
        _behavioursMap[typeof(MeleeEnemyBehaviourWalk)] = new MeleeEnemyBehaviourWalk(_walking);
        _behavioursMap[typeof(EnemyDeadBehaviour)] = new EnemyDeadBehaviour(_death);
    }

    protected override void SetBehaviourByDefault()
    {
        SetBehaviourIdle();
    }

    private void TrySetBehaviourAttack()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;

        Behaviour behaviour;
        if (_random.NextDouble() <= _attackChance)
        {
            behaviour = GetBehaviour<CaptainBehaviourAttack>();
        }
        else
        {
            behaviour = GetBehaviour<MeleeEnemyBehaviourWalk>();
        }
        SetBehaviour(behaviour);
    }

    private void SetBehaviourIdle()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;

        var behaviour = GetBehaviour<MeleeEnemyBehaviourIdle>();
        SetBehaviour(behaviour);
    }

    private void SetBehaviourDead()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;

        var behaviour = GetBehaviour<EnemyDeadBehaviour>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        _idle.OnEnded += TrySetBehaviourAttack;
        _attack.OnAttack += SetBehaviourIdle;
        _walking.OnEnded += SetBehaviourIdle;
        _health.OnDead += SetBehaviourDead;
    }

    protected override void Unsubscribe()
    {
        _idle.OnEnded -= TrySetBehaviourAttack;
        _attack.OnAttack -= SetBehaviourIdle;
        _walking.OnEnded -= SetBehaviourIdle;
        _health.OnDead -= SetBehaviourDead;
    }
}
