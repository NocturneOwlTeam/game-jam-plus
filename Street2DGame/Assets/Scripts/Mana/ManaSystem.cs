using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManaSystem : MonoBehaviour, IManaSystem
{
    [SerializeField]
    private float maximumMana;

    [SerializeField]
    private float startingMana;

    private float maxMana;

    private float currentMana;

    public event Action OnManaChanged;
    public event Action OnDrain;
    public event Action OnRecover;
    public event Action OnMaxManaChanged;
    
    private void Awake()
    {
        maxMana = maximumMana;
        if(startingMana > 0)
        {
            SetMana(startingMana);
        }
        else
        {
            currentMana = maximumMana;
        }
    }

    public bool AtFullMana() => currentMana == maxMana;

    public void Drain(float drainage)
    {
        if (drainage <= 0 || drainage > currentMana) return;
        currentMana = Mathf.Clamp(currentMana - drainage, 0, maxMana);
        OnManaChanged?.Invoke();
        OnDrain?.Invoke();
    }

    public float GetMana() => currentMana;

    public float GetMaxMana() => maximumMana;

    public void InstantDrain()
    {
        currentMana = 0;
        OnManaChanged?.Invoke();
        OnDrain?.Invoke();
    }

    public void InstantRecover()
    {
        currentMana = maxMana;
        OnManaChanged?.Invoke();
        OnRecover?.Invoke();
    }

    public bool IsManaDrained() => currentMana <= 0;

    public void Recover(float recovering)
    {
        if(recovering <= 0) return;
        currentMana = Mathf.Clamp(currentMana + recovering, 0, maxMana);
        OnManaChanged?.Invoke();
        OnRecover?.Invoke();
    }

    public void SetMana(float mana)
    {
        if(mana <= 0) return;
        currentMana = mana;
        OnManaChanged?.Invoke();
    }

    public void SetMaxMana(float max)
    {
        float temp = maxMana;
        maxMana = max;
        OnMaxManaChanged?.Invoke();
        if(temp <= currentMana || currentMana >= maxMana)
        {
            currentMana = maxMana;
            OnManaChanged?.Invoke();
        }
    }

}
