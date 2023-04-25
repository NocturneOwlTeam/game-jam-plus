using System.Collections.Generic;
using UnityEngine;

public class AirEntryState : MeleeBaseState
{

    public override void OnEnterState(CombatManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 1;
        duration = 0.5f;
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este ataque.
        animator.SetTrigger($"Attack{attackIndex}");
        Debug.Log($"Ataque aereo {attackIndex} activo");
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                //Siguiente combo;
                currentManager.SetNextState(new AirComboState());
            }
            else
            {
                //Retornar a estado principal o idle o movement.
                currentManager.SetNextStateAsMain();
            }
        }
    }
}

public class AirComboState : MeleeBaseState
{
    public override void OnEnterState(CombatManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 2;
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
                currentManager.SetNextState(new AirFinisherState());
            }
            else
            {
                //Retornar a estado principal o idle o movement.
                currentManager.SetNextStateAsMain();
            }
        }
    }
}

public class AirFinisherState : MeleeBaseState
{
    //Opcional: como es combate aereo, recomiendo agregar un delay para este tipo de combo para que el jugador no abuse de este.
    public override void OnEnterState(CombatManager manager)
    {
        base.OnEnterState(manager);

        //Ataque:
        attackIndex = 3;
        duration = 0.75f;
        //NOTA: reemplazalo por el nombre de la animacion relacionada a este ataque.
        animator.SetTrigger($"Attack{attackIndex}");
        Debug.Log($"Ataque aereo {attackIndex} activo");
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