using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // Aqui esta basicamente el cerebro, el que maneja el CombatManager (que es el State Machine o maquina de estado) para gestionar todo a la vez y se comunique con el usuario.
    //Y el que establece el Idle State o neutro como la base, porque sino vamos a tener problemas manejando el resto.
    //Adicionalmente, sera el intermediario entre el CombatManager y los demas manejadores de estado.

    //Para Erick, aqui gestionas cuando habilitar y desahilitar tu codigo de movimiento.
    [SerializeField]
    private StateMachineManager combatManager;

    public Collider2D hitbox;

    public GameObject hitEffect;

    private void Start()
    {
        if (!combatManager && !TryGetComponent(out combatManager))
        {
            Debug.LogError("Este componente requiere del componente CombatManager o sino no funcionara correctamente");
        }
    }

    private void Update()
    {
        //Verifica si el estado actual es Idle para que se cambie nomas al combate.
        //NOTA: si quieren dar más condiciones (si esta sprinteando, en un roll, etc), conviertan esto asi:
        //- Primero que se detecte la condicion del boton en si (vamos a poner fire1, pero en el input, deben cambiarlo a otro
        //- Despues, usen el switch case para detectar que tipo de estado es.
        if (Input.GetButton("Fire1") && combatManager.currentState.GetType() == typeof(IdleCombatState))
        {
            //Cambia a estado nuevo
            combatManager.SetNextState(new MeleeEntryState());
        }
    }
}