using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] public UnityEvent ProjectileShootEvent;
    
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shootingSpeed;
    [SerializeField] float shootingCooldown;
    [SerializeField] float damage;
    [SerializeField] float projectileLifetime;

    float lastShotTime;

    float offset = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // So the player can shoot immediately upon loading in
        lastShotTime = -shootingCooldown - 1;
    }

    // pew pew, called by Enemy or Player Attack scripts
    public void Shoot(Vector2 direction)
    {
        if (Time.time - lastShotTime > shootingCooldown)
        {

            // offset so you don't take damage from your own bullet
            Vector3 position = transform.position + new Vector3(direction.x, direction.y, transform.position.y) * offset;
            
            // Load in a projectile
            GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            ProjectileShootEvent.Invoke();

            // Set projectile lifetime and damage
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.setLifetimeAndDamage(projectileLifetime, damage);

            // give it velocity so it can go pew pew
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x, direction.y, 0) * shootingSpeed;

            // reset the last shot time to the current time
            lastShotTime = Time.time;

        }
        else
        {
            Debug.Log("no pew pew, still on cooldown");
        }

    }

}

