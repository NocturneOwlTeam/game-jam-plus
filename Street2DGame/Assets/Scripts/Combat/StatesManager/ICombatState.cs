public interface ICombatState
{
    public void OnEnterState(CombatManager manager);

    public void OnExitState(CombatManager manager);

    public void OnUpdateState();

    public void OnFixedUpdate();

    public void OnLateUpdate();
}