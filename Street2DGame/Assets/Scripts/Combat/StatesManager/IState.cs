public interface IState
{
    public void OnEnterState(StateMachineManager manager);

    public void OnExitState(StateMachineManager manager);

    public void OnUpdateState();

    public void OnFixedUpdate();

    public void OnLateUpdate();
}