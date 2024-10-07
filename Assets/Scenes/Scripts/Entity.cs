using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{

    [Serializable]
    public struct EntityStats
    {
        public float MaxHealth;
        public float MovementSpeed;
    }

    public Rigidbody2D rb;
    public EntityStats stats;

    public UnityEvent DeathEvent;

    [NonSerialized]
    public float CurrentHealth;

    // this is used to prevent "multiple deaths" in one frame
    // which can happen if multiple projectiles
    // hit in one frame
    bool dead = false;

    private void Start()
    {
        CurrentHealth = stats.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(dead) return;

        CurrentHealth -= Mathf.Ceil(damage);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DeathEvent.Invoke();
        dead = true;
        Destroy(gameObject);
    }
}