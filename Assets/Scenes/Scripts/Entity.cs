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

    private void Start()
    {
        CurrentHealth = stats.MaxHealth;
    }

    public void TakeDamage(float damage)
    {

        CurrentHealth -= damage;
        Debug.Log("Ouch");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("oof you died");
        DeathEvent.Invoke();
        Destroy(gameObject);
    }
}