public interface IStateMachine
{
    public void SwitchState(IState state);

    public void SetNextStateAsMain();

    public void SetNextState(IState newNextState);

    public void StartMainState();
}