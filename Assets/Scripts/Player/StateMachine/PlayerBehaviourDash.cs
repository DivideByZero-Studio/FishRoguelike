public class PlayerBehaviourDash : Behaviour
{
    private PlayerDash _dash;

    public PlayerBehaviourDash(PlayerDash dash)
    {
        _dash = dash;
    }

    public override void Enter()
    {
        _dash.enabled = true;
        _dash.StartDash();
    }

    public override void Exit()
    {
        _dash.enabled = false;
    }
}
