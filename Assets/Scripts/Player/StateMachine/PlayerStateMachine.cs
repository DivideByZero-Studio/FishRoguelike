using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttack), typeof(PlayerDash))]
public class PlayerStateMachine : StateMachine
{
    private PlayerController _characterController;
    private PlayerMovement _movement;
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private Health _health;

    private void Awake()
    {
        _characterController = GetComponent<PlayerController>();
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
        _characterController.OnDash += SetBehaviourDash;
        _dash.OnDashEnd += SetBehaviourActive;
        _health.OnDead += SetBehaviourDead;
    }

    protected override void Unsubscribe()
    {
        _characterController.OnDash -= SetBehaviourDash;
        _dash.OnDashEnd -= SetBehaviourActive;
        _health.OnDead -= SetBehaviourDead;
    }
}
