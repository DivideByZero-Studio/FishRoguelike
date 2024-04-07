public class MeleeEnemyBehaviourAttack : Behaviour
{
    private MeleeEnemyAttack _attack;

    public MeleeEnemyBehaviourAttack(MeleeEnemyAttack attack)
    {
        _attack = attack;
    }

    public override void Update()
    {
        _attack.Attack();
    }
}
