using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainBehaviourAttack : Behaviour
{
    private EnemyAttack _attack;
    private Transform _playerTransform;

    public CaptainBehaviourAttack(EnemyAttack attack, Transform playerTransform)
    {
        _attack = attack;
        _playerTransform = playerTransform;
    }

    public override void Enter()
    {
        _attack.StartAttack(_playerTransform);;
    }

    public override void Update()
    {
        _attack.Attack();
    }
}
