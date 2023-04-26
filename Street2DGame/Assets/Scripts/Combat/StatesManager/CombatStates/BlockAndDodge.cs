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
        //Nota: reemplazalo con otro boton
        if (Input.GetButton("Fire2"))
        {

        }
        else
        {

        }
    }
}

public class BlockState
{

}

public class DodgeState
{

}
