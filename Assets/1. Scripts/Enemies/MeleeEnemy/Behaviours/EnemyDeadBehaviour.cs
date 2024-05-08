public class EnemyDeadBehaviour : Behaviour
{
    private EnemyDeath _death;
    
    public EnemyDeadBehaviour(EnemyDeath death)
    {
        _death = death;
    }

    public override void Enter()
    {
        _death.Dead();
    }
}
