using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    //Este sera el estado principal
    private IState idleState;

    public IState currentState { get; private set; }

    private IState nextState;

    private void Awake()
    {
        SetNextStateAsMain();
    }

    public void SwitchState(IState newState)
    {
        nextState = null;
        currentState?.OnExitState(this);
        currentState = newState;
        currentState.OnEnterState(this);
    }

    public void SetNextState(IState newNextState) => nextState = newNextState;

    public void SetNextStateAsMain() => nextState = idleState;

    private void Update()
    {
        if (nextState != null)
        {
            SwitchState(nextState);
        }
        currentState?.OnUpdateState();
    }

    private void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        currentState?.OnLateUpdate();
    }

    //Esto pueede ser importante para otros más adelante.
    public virtual void StartMainState()
    {
        if (idleState == null)
        {
            idleState = new IdleCombatState();
        }
    }

    private void OnValidate()
    {
        StartMainState();
    }
}