using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //Este sera el estado principal
    private ICombatState idleState;

    public ICombatState currentState { get; private set; }

    private ICombatState nextState;

    protected Animator combatAnimator;

    void Awake()
    {
        SetNextStateAsMain();
    }

    public void SwitchState(ICombatState newState)
    {
        nextState = null;
        currentState?.OnExitState(this);
        currentState = newState;
        currentState.OnEnterState(this);
    }

    public void SetNextState(ICombatState newNextState) => nextState = newNextState;

    public void SetNextStateAsMain() => nextState = idleState;

    void Update()
    {
        if(nextState != null)
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

    private void OnValidate()
    {
        if(idleState == null)
        {
            idleState = new IdleCombatState();
        }
    }
}
