public class MeleeEnemyBehaviourIdle : Behaviour
{
    private EnemyIdle _enemyIdle;

    public MeleeEnemyBehaviourIdle(EnemyIdle enemyIdle)
    {
        _enemyIdle = enemyIdle;
    }

    public override void Enter()
    {
        _enemyIdle.StartIdle();
    }

    public override void Exit()
    {
        _enemyIdle.StopIdle();
    }
}
