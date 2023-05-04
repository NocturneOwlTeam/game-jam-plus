//Aqui ponen los estados genericos de combate para cualquier estilo o tipo de combate
//Como este estado, que tiene como proposito verificar si es posible atacar o no.
public class MeleeEntryState : IState
{
    public void OnEnterState(StateMachineManager manager)
    {
        //NOTA: aqui se pone la verificacion de si el jugador esta en el aire, en el suelo, o cualquier otra variable para el combo.
        IState nextState = new HeavyGroundAttack();
        manager.SetNextState(nextState);
    }

    public void OnExitState(StateMachineManager manager)
    {
        return;
    }

    public void OnFixedUpdate()
    {
        return;
    }

    public void OnLateUpdate()
    {
        return;
    }

    public void OnUpdateState()
    {
        return;
    }
}