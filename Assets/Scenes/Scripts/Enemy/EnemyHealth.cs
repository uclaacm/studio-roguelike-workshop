using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] public float health = 1f;
    public void TakeDamage (float damage)
    {
 
        health -= damage;
        Debug.Log("Ouch");

        if (health <=0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("oof you died");
        Destroy(gameObject);
    }
}
