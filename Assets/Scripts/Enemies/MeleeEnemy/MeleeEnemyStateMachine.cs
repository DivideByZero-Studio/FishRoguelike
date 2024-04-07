using UnityEngine;

public class MeleeEnemyStateMachine : EnemyStateMachine
{
    [SerializeField, Range(0, 1)] private float _approachChance;
    [SerializeField, Range(0, 1)] private float _attackAgainChance;

    [SerializeField] private EnemyAttackRange _attackRange;

    private EnemyAttack _attack;
    private EnemyFlee _flee;
    private MeleeEnemyApproach _approach;
    private EnemyIdle _idle;
    private EnemyWalk _walking;
    private EnemyDeath _death;
    private Health _health;

    private System.Random _random;

    protected override void Awake()
    {
        base.Awake();
        _attack = GetComponent<MeleeEnemyAttack>();
        _flee = GetComponent<EnemyFlee>();
        _approach = GetComponent<MeleeEnemyApproach>();
        _idle = GetComponent<EnemyIdle>();
        _walking = GetComponent<EnemyWalk>();
        _death = GetComponent<EnemyDeath>();
        _health = GetComponent<Health>();

        _random = new System.Random();
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(MeleeEnemyBehaviourFlee)] = new MeleeEnemyBehaviourFlee(_flee, _playerTransform);
        _behavioursMap[typeof(MeleeEnemyBehaviourApproach)] = new MeleeEnemyBehaviourApproach(_approach, _playerTransform);
        _behavioursMap[typeof(MeleeEnemyBehaviourAttack)] = new MeleeEnemyBehaviourAttack(_attack, _playerTransform);
        _behavioursMap[typeof(MeleeEnemyBehaviourIdle)] = new MeleeEnemyBehaviourIdle(_idle);
        _behavioursMap[typeof(MeleeEnemyBehaviourWalk)] = new MeleeEnemyBehaviourWalk(_walking);
        _behavioursMap[typeof(EnemyDeadBehaviour)] = new EnemyDeadBehaviour(_death);
    }

    protected override void SetBehaviourByDefault()
    {
        var behaviour = GetBehaviour<MeleeEnemyBehaviourIdle>();
        SetBehaviour(behaviour);
    }

    protected virtual void SetRandomMoveBehaviour()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;

        Behaviour behaviour;

        double chance = _random.NextDouble();
        if (chance <= _approachChance)
        {
            behaviour = GetBehaviour<MeleeEnemyBehaviourApproach>();
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

    private void SetBehaviourAttack()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;
        var behaviour = GetBehaviour<MeleeEnemyBehaviourAttack>();
        SetBehaviour(behaviour);
    }

    private void TrySetBehaviourApproach()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;
        if (_currentBehaviour != GetBehaviour<MeleeEnemyBehaviourFlee>())
        {
            var behaviour = GetBehaviour<MeleeEnemyBehaviourApproach>();
            SetBehaviour(behaviour);
        }
    }

    private void TrySetBehaviourAttackAgain()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;
        double chance = _random.NextDouble();

        if (chance >= _attackAgainChance)
        {
            var behaviour = GetBehaviour<MeleeEnemyBehaviourFlee>();
            SetBehaviour(behaviour);
        }
    }

    private void SetBehaviourDead()
    {
        if (_currentBehaviour == GetBehaviour<EnemyDeadBehaviour>()) return;
        var behaviour = GetBehaviour<EnemyDeadBehaviour>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        _flee.OnEnded += SetBehaviourIdle;
        _walking.OnEnded += SetBehaviourIdle;

        _idle.OnEnded += SetRandomMoveBehaviour;

        _approach.OnApproached += SetBehaviourAttack;
        _attack.OnLeft += TrySetBehaviourApproach;

        _attack.OnAttack += TrySetBehaviourAttackAgain;

        _attackRange.OnEntered += SetBehaviourAttack;

        _health.OnDead += SetBehaviourDead;
    }

    protected override void Unsubscribe()
    {
        _flee.OnEnded -= SetBehaviourIdle;
        _walking.OnEnded -= SetBehaviourIdle;

        _idle.OnEnded -= SetRandomMoveBehaviour;

        _approach.OnApproached -= SetBehaviourAttack;
        _attack.OnLeft -= TrySetBehaviourApproach;

        _attack.OnAttack -= TrySetBehaviourAttackAgain;

        _attackRange.OnEntered -= SetBehaviourAttack;

        _health.OnDead -= SetBehaviourDead;
    }
}
