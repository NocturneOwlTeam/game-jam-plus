public interface IState
{
    public void OnEnterState(StateMachine manager);

    public void OnExitState(StateMachine manager);

    public void OnUpdateState();

    public void OnFixedUpdate();

    public void OnLateUpdate();
}