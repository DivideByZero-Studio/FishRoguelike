using UnityEngine;

public class MeleeEnemyBehaviourFlee: Behaviour
{
    private EnemyFlee _flee;
    private Transform _playerTransform;
    
    public MeleeEnemyBehaviourFlee(EnemyFlee flee, Transform playerTransform)
    {
        _flee = flee;
        _playerTransform = playerTransform;
    }

    public override void Enter()
    {
        _flee.StartFlee(_playerTransform);
    }
    public override void Exit()
    {
        _flee.StopFlee();
    }

    public override void Update()
    {
        _flee.Flee();
    }

}
