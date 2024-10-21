using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class EntityStats
{
    // max health
    public float MovementSpeed;
}

public class Entity : MonoBehaviour
{

    public EntityStats stats;

    // Non-serialized stuff goes here 

    private void Start()
    {
        // Initialize CurrentHealth here
    }

    public void TakeDamage(float damage)
    {
        // Change CurrentHealth based on damage
        // Invoke dying if necessary 
    }

    private void Die()
    {
        // Die
    }
}
