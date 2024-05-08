public class PlayerBehaviourActive : Behaviour
{
    private PlayerMovement _movement;
    private PlayerAttack _attack;

    public PlayerBehaviourActive(PlayerMovement movement, PlayerAttack attack)
    {
        _movement = movement;
        _attack = attack;
    }

    public override void Enter()
    {
        _movement.enabled = true;
        _attack.enabled = true;
    }

    public override void Exit()
    {
        _movement.enabled = false;
        _attack.enabled = false;
    }
}
