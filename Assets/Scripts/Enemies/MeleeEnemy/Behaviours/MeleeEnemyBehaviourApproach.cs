using UnityEngine;

public class MeleeEnemyBehaviourApproach : Behaviour
{
    private MeleeEnemyApproach _approach;
    private Transform _playerTransform;

    public MeleeEnemyBehaviourApproach(MeleeEnemyApproach approach, Transform playerTransform)
    {
        _approach = approach;
        _playerTransform = playerTransform;
    }

    public override void Enter()
    {
        _approach.StartApproach(_playerTransform);
    }

    public override void Exit()
    {
        _approach.StopApproach();
    }

    public override void Update()
    {
        _approach.Approach();
    }
}
