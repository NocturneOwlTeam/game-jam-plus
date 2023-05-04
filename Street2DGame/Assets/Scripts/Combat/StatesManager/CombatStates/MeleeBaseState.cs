using Lean.Pool;
using Nocturne.Enums;
using Nocturne.Health;
using System;
using System.Collections.Generic;
using UnityEngine;

//Funciona como el intermediario base para usarse con cualquier tipo de combo o ataque.
//LLama a: cambio de animacion, input, daño y cambio de estado.
public class MeleeBaseState : IState
{
    //Estos son para que cualquier variable sean afectados por el Update, Fixed Update y Late Update.
    //Sabiendo lo obsesinados que estan los jugadores de lucha con eso de input buffer, input delay, etc, estos seran importantes más adelante:
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
    protected bool upperActivated;
    protected bool heavyActivated;

    //El indice del combo o ataque de la secuencia de ataques.
    protected int attackIndex;

    //Nota: si quieres que haya daño independiente, modifica este
    protected int damage;

    public StateMachineManager currentManager;

    //Guarda el collider del ataque con el cual daña a otros
    protected Collider2D hitcollider;

    //Guarda temporalmente los enemigos golpeados para evitar que haya cruce de daño o que se golpee al mismo enemigo varias veces.
    private List<Collider2D> collidersDamaged;

    //Efecto de golpe.
    private GameObject hitEffect;

    //Input buffer
    private float attackPressedTimer = 0f;
    private float heavyAttackPressedTimer = 0f;
    private float upPressed = 0f;
    private float downPressed = 0f;

    public static Action OnAttackLanded;

    public virtual void OnEnterState(StateMachineManager manager)
    {
        currentManager = manager;
        animator = manager.GetComponentInChildren<Animator>();
        collidersDamaged = new List<Collider2D>();
        hitcollider = manager.GetComponent<CharacterManager>().hitbox;
        //hitEffect = manager.GetComponent<ComboCharacter>().hitEffect;
    }

    public virtual void OnExitState(StateMachineManager manager)
    {
        return;
    }

    public virtual void OnUpdateState()
    {
        time += Time.deltaTime;
        attackPressedTimer -= Time.deltaTime;
        upPressed -= Time.deltaTime;
        heavyAttackPressedTimer -= Time.deltaTime;
        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            attackPressedTimer = 2f;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            heavyAttackPressedTimer = 2f;
        }

        if(Input.GetAxis("Vertical")> 0.1f)
        {
            upPressed = 2f;
        }
        if (attackPressedTimer > 0 && animator.GetFloat("ActiveWindow.Open") > 0f)
        {
            shouldCombo = true;
        }

        if (heavyAttackPressedTimer > 0 && animator.GetFloat("ActiveWindow.Open") > 0f)
        {
            heavyActivated = true;
        }

        if (upPressed > 0 && animator.GetFloat("ActiveWindow.Open") > 0f)
        {
            upperActivated = true;
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
                HealthSystem targetHealth = collidersToDamage[i].GetComponentInChildren<HealthSystem>();
                //Revisan aqui si tienen algun teamcomponent y dañador activo.
                if (targetHealth && teamComponent && teamComponent.currentTeamIndex == TeamIndex.Enemy)
                {
                    //Para Spawnear el efecto.
                    //LeanPool.Spawn(hitEffect, collidersToDamage[i].transform);
                    Debug.Log($"Enemy has taken {damage} damage");
                    OnAttackLanded?.Invoke();
                    targetHealth.Damage(damage);
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