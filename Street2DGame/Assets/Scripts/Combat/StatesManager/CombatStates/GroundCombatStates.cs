using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnterState(StateMachineManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 1;
        damage = 2;
        duration = 0.5f;
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este ataque.
        animator.SetTrigger($"Attack{attackIndex}");
        Debug.Log($"Ataque {attackIndex} activo");
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                //Siguiente combo;
                currentManager.SetNextState(new GroundComboState());
            }
            else
            {
                //Retornar a estado principal o idle o movement.
                currentManager.SetNextStateAsMain();
            }
        }
    }
}

public class GroundComboState : MeleeBaseState
{
    public override void OnEnterState(StateMachineManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 2;
        damage = 2;
        duration = 0.5f;
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este ataque.
        animator.SetTrigger($"Attack{attackIndex}");
        Debug.Log($"Ataque {attackIndex} activo");
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                //Siguiente combo;
                currentManager.SetNextState(new GroundFinisherState());
            }
            else
            {
                //Retornar a estado principal o idle o movement.
                currentManager.SetNextStateAsMain();
            }
        }
    }
}

public class GroundFinisherState : MeleeBaseState
{
    public override void OnEnterState(StateMachineManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 3;
        damage = 3;
        duration = 0.75f;
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este ataque.
        animator.SetTrigger($"Attack{attackIndex}");
        Debug.Log($"Ataque {attackIndex} activo");
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (fixedtime >= duration)
        {
            //Retornar a estado principal o idle o movement o a un nuevo combo, puesto que este es el ultimo.
            currentManager.SetNextStateAsMain();
        }
    }
}

public class SprintAttackState : MeleeBaseState
{
    public override void OnEnterState(StateMachineManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 1;
        damage = 3;
        duration = 0.75f;
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este ataque.
        animator.SetTrigger($"Attack{attackIndex}");
        Debug.Log($"Ataque {attackIndex} activo");
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (fixedtime >= duration)
        {
            //Retornar a estado principal o idle o movement o a un nuevo combo, puesto que este es el ultimo.
            currentManager.SetNextStateAsMain();
        }
    }
}

public class HeavyGroundAttack: MeleeBaseState
{
    public override void OnEnterState(StateMachineManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 1;
        damage = 4;
        duration = 0.75f;
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este ataque.
        animator.SetTrigger($"Attack{attackIndex}");
        Debug.Log($"Ataque {attackIndex} activo");
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (fixedtime >= duration)
        {
            //Retornar a estado principal o idle o movement o a un nuevo combo, puesto que este es el ultimo.
            currentManager.SetNextStateAsMain();
        }
    }
}

public class GrabAttackState : IState
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
        animator.SetTrigger($"Grab");
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