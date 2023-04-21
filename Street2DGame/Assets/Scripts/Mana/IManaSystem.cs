using System;

public interface IManaSystem
{
    public event Action OnManaChanged;

    public event Action OnDrain;

    public event Action OnRecover;

    public event Action OnMaxManaChanged;

    public void Drain(float drainage);

    public void Recover(float recovering);

    public void SetMana(float mana);

    public float GetMana();

    public void SetMaxMana(float max);

    public float GetMaxMana();

    public bool AtFullMana();

    public bool IsManaDrained();

    public void InstantRecover();

    public void InstantDrain();
}
