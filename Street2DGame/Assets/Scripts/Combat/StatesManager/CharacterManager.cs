using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // Aqui esta basicamente el cerebro, el que maneja el CombatManager (que es el State Machine o maquina de estado) para gestionar todo a la vez y se comunique con el usuario.
    //Y el que establece el Idle State o neutro como la base, porque sino vamos a tener problemas manejando el resto.
    //Adicionalmente, sera el intermediario entre el CombatManager y los demas manejadores de estado.

    [SerializeField]
    private StateMachineManager combatManager;

    public Collider2D hitbox;

    public GameObject hitEffect;

    float combatDelay = 0.7f;

    float currentDelay = 0f;

    private void Start()
    {
        if (!combatManager && !TryGetComponent(out combatManager))
        {
            Debug.LogError("Este componente requiere del componente CombatManager o sino no funcionara correctamente");
        }
    }

    private void Update()
    {
        //Esto por alguna razon no funciona en el build: && combatManager.currentState.GetType() == typeof(IdleCombatState)
        //A veces odio Unity.
        if (Input.GetButtonDown("Fire1") && currentDelay <= 0f)
        {
            //Cambia a estado nuevo
            combatManager.SetNextState(new MeleeEntryState());
            currentDelay = combatDelay;
        }

        if(currentDelay > 0f)
        {
            currentDelay -= Time.deltaTime;
        }
    }
}