using UnityEngine;

public class MeleeEnemyBehaviourAttack : Behaviour
{
    private MeleeEnemyAttack _attack;
    private Transform _playerTransform;
    public MeleeEnemyBehaviourAttack(MeleeEnemyAttack attack, Transform playerTransform)
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
