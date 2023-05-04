using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAndDodge : IState
{
    public StateMachineManager currentManager;
    public float dodgeDuration;
    public virtual void OnEnterState(StateMachineManager manager)
    {
        return;
    }

    public virtual void OnExitState(StateMachineManager manager)
    {
        return;
    }

    public virtual void OnFixedUpdate()
    {
        return;
    }

    public virtual void OnLateUpdate()
    {
        return;
    }

    public void OnUpdateState()
    {

    }
}

public class BlockState
{

}

public class DodgeState
{

}

public class EvadeState : IState
{
    protected float fixedtime { get; set; }

    public float duration;

    //Animador relacionado.
    protected Animator animator;
    public StateMachineManager currentManager;
    public void OnEnterState(StateMachineManager manager)
    {
        currentManager = manager;
        duration = 1f;
        animator = manager.GetComponent<Animator>();
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este estado.
        animator.SetTrigger($"Evade");
    }

    public void OnExitState(StateMachineManager manager)
    {
        return;
    }

    public void OnFixedUpdate()
    {
        fixedtime += Time.deltaTime;
        
    }

    public void OnLateUpdate()
    {
        return;
    }

    public void OnUpdateState()
    {
        if (fixedtime >= duration)
        {
            //Retornar a estado principal o idle o movement o a un nuevo combo, puesto que este es el ultimo.
            currentManager.SetNextStateAsMain();
        }
    }
}
