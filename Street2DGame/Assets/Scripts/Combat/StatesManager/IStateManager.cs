using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateManager
{
    public void SwitchState(ICombatState state);
}
