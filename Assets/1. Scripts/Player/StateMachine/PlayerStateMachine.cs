using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttack), typeof(PlayerDash))]
public class PlayerStateMachine : StateMachine
{
    private PlayerMovement _movement;
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private Health _health;

    [Inject] private GameInput gameInput;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _attack = GetComponent<PlayerAttack>();
        _dash = GetComponent<PlayerDash>();
        _health = GetComponent<Health>();
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(PlayerBehaviourActive)] = new PlayerBehaviourActive(_movement, _attack);
        _behavioursMap[typeof(PlayerBehaviourDash)] = new PlayerBehaviourDash(_dash);
        _behavioursMap[typeof(PlayerBehaviourDead)] = new PlayerBehaviourDead(gameObject);
    }

    protected override void SetBehaviourByDefault()
    {
        var behaviour = GetBehaviour<PlayerBehaviourActive>();
        SetBehaviour(behaviour);
    }

    private void SetBehaviourDash()
    {
        if (_currentBehaviour == GetBehaviour<PlayerBehaviourDead>()) return;
        var behaviour = GetBehaviour<PlayerBehaviourDash>();
        SetBehaviour(behaviour);
    }

    private void SetBehaviourActive()
    {
        if (_currentBehaviour == GetBehaviour<PlayerBehaviourDead>()) return;
        var behaviour = GetBehaviour<PlayerBehaviourActive>();
        SetBehaviour(behaviour);
    }

    private void SetBehaviourDead()
    {
        if (_currentBehaviour == GetBehaviour<PlayerBehaviourDead>()) return;
        var behaviour = GetBehaviour<PlayerBehaviourDead>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        gameInput.OnDash += SetBehaviourDash;
        _dash.OnDashEnd += SetBehaviourActive;
        _health.OnDead += SetBehaviourDead;
    }

    protected override void Unsubscribe()
    {
        gameInput.OnDash -= SetBehaviourDash;
        _dash.OnDashEnd -= SetBehaviourActive;
        _health.OnDead -= SetBehaviourDead;
    }
}
