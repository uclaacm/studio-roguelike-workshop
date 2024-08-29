using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] float lifeTime = 5.0f;
    [SerializeField] float damage = 1f;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > lifeTime)
            Destroy(gameObject);
    }

    // this function is called when another object enters the projectile's collider 
    // and the projectile's collider is marked as a trigger
    // The parameter (other) contains information about what collided into the projectile
    // Unity documentation: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the projectile collides with an enemy
        if (other.tag == "Enemy")
        {
            // other.GetComponent gets a component on the same game object as other (which is the collider and not the game object itself)
            // In this case, it gets the Entity component, which is a script (a type of component)
            // After getting that script, we can call TakeDamage(damage) on that script to deal damage
            // Unity documentation: https://docs.unity3d.com/ScriptReference/Component.GetComponent.html
            Entity enemy = other.GetComponent<Entity>();

            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
