using UnityEngine;

public class MeleeEnemyBehaviourAttack : Behaviour
{
    private EnemyAttack _attack;
    private Transform _playerTransform;
    public MeleeEnemyBehaviourAttack(EnemyAttack attack, Transform playerTransform)
    {
        _attack = attack;
        _playerTransform = playerTransform;
    }

    public override void Enter()
    {
        _attack.StartAttack(_playerTransform);
    }

    public override void Update()
    {
        _attack.Attack();
    }
}
