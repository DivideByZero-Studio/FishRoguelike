public class MeleeEnemyBehaviourWalk : Behaviour
{
    private EnemyWalk _walking;

    public MeleeEnemyBehaviourWalk(EnemyWalk walking)
    {
        _walking = walking;
    }

    public override void Enter()
    {
        _walking.StartWalk();
    }

    public override void Exit()
    {
        _walking.StartWalk();
    }

    public override void Update()
    {
        _walking.Walk();
    }
}
