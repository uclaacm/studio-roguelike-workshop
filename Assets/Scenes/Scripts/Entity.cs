using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [Serializable]
    public struct EntityStats
    {
        // max health
        public float MovementSpeed;
    }

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