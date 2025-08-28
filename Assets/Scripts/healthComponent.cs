using System;
using UnityEngine;

public class healthComponent : MonoBehaviour, IIDamageable
{
    public event Action<float, float, float> onDamaged;

    public event Action<MonoBehaviour> onDead;

    [Header("health variables")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    //apply damage to player
    public void applyDamage(float damage, MonoBehaviour causer) 
    {
        float change = Mathf.Min(currentHealth, damage);
        currentHealth -= change;
        
        //invoke the on damaged event with the players current health and max health
        onDamaged?.Invoke(currentHealth, maxHealth, change);
        if (currentHealth <= 0.0f)
        {
            onDead?.Invoke(causer);
        }
    }
}

