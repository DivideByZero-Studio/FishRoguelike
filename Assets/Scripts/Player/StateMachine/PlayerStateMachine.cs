using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttack), typeof(PlayerDash))]
public class PlayerStateMachine : StateMachine
{
    private CharacterController _characterController;
    private PlayerMovement _movement;
    private PlayerAttack _attack;
    private PlayerDash _dash;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _movement = GetComponent<PlayerMovement>();
        _attack = GetComponent<PlayerAttack>();
        _dash = GetComponent<PlayerDash>();
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(PlayerBehaviourActive)] = new PlayerBehaviourActive(_movement, _attack);
        _behavioursMap[typeof(PlayerBehaviourDash)] = new PlayerBehaviourDash(_dash);
    }

    protected override void SetBehaviourByDefault()
    {
        var behaviour = GetBehaviour<PlayerBehaviourActive>();
        SetBehaviour(behaviour);
    }

    private void SetBehaviourDash()
    {
        var behaviour = GetBehaviour<PlayerBehaviourDash>();
        SetBehaviour(behaviour);
    }

    private void SetBehaviourActive()
    {
        var behaviour = GetBehaviour<PlayerBehaviourActive>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        _characterController.OnDash += SetBehaviourDash;
        _dash.OnDashEnd += SetBehaviourActive;
    }

    protected override void Unsubscribe()
    {
        _characterController.OnDash -= SetBehaviourDash;
        _dash.OnDashEnd -= SetBehaviourActive;
    }
}
