using Lean.Pool;
using Nocturne.Enums;
using System.Collections.Generic;
using UnityEngine;

//Funciona como el intermediario base para usarse con cualquier tipo de combo o ataque.
//LLama a: cambio de animacion, input, da�o y cambio de estado.
public class MeleeBaseState : ICombatState
{
    //Estos son para que cualquier variable sean afectados por el Update, Fixed Update y Late Update.
    //Sabiendo lo obsesinados que estan los jugadores de lucha con eso de input buffer, input delay, etc, estos seran importantes m�s adelante:
    //TODO: refactorizar para que use OnCollide2D, viendo que ahora activa/desactiva correctamente, aprovechando los nuevos cambios.
    protected float time { get; set; }

    protected float fixedtime { get; set; }
    protected float latetime { get; set; }

    //Duracion del estado antes de cambiar al siguiente.
    public float duration;

    //Animador relacionado.
    protected Animator animator;

    //Determina si el siguiente ataque de la sequencia o combo deberia aparecer o no.
    protected bool shouldCombo;

    //El indice del combo o ataque de la secuencia de ataques.
    protected int attackIndex;

    //Nota: si quieres que haya da�o independiente, modifica este
    protected int damage;

    public CombatManager currentManager;

    //Guarda el collider del ataque con el cual da�a a otros
    protected Collider2D hitcollider;

    //Guarda temporalmente los enemigos golpeados para evitar que haya cruce de da�o o que se golpee al mismo enemigo varias veces.
    private List<Collider2D> collidersDamaged;

    //Efecto de golpe.
    private GameObject hitEffect;

    //Input buffer
    private float attackPressedTimer = 0f;

    public virtual void OnEnterState(CombatManager manager)
    {
        currentManager = manager;
        animator = manager.GetComponent<Animator>();
        collidersDamaged = new List<Collider2D>();
        hitcollider = manager.GetComponent<ComboCharacter>().hitbox;
        //hitEffect = manager.GetComponent<ComboCharacter>().hitEffect;
    }

    public virtual void OnExitState(CombatManager manager)
    {
        return;
    }

    public virtual void OnUpdateState()
    {
        time += Time.deltaTime;
        attackPressedTimer -= Time.deltaTime;

        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            attackPressedTimer = 2f;
        }
        if (attackPressedTimer > 0 && animator.GetFloat("ActiveWindow.Open") > 0f)
        {
            shouldCombo = true;
        }
    }

    //El metodo principal de ataque.
    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitcollider, contactFilter, collidersToDamage);
        for(int i = 0; i < colliderCount; i++)
        {
            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent teamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                //Revisan aqui si tienen algun teamcomponent y da�ador activo.
                if (teamComponent && teamComponent.currentTeamIndex == TeamIndex.Enemy)
                {
                    //Para Spawnear el efecto.
                    //LeanPool.Spawn(hitEffect, collidersToDamage[i].transform);
                    Debug.Log($"Enemy has taken {attackIndex} damage");
                    collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }

    public void OnFixedUpdate()
    {
        fixedtime += Time.deltaTime;
    }

    public void OnLateUpdate()
    {
        latetime += Time.deltaTime;
    }
}