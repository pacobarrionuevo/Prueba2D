using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField, Min(1f)] protected float maxHealth = 100f;

    public float MaxHealth => maxHealth;
    public float CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    protected void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float amount) 
    {
        if (IsDead || amount <= 0f) return;

        CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);
        //                        0       100       -   150

        Debug.Log(CurrentHealth);

        OnHealthChanged.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            IsDead = true;

            OnDeath.Invoke();

            Die();
        }
    }

    protected abstract void Die();
}
