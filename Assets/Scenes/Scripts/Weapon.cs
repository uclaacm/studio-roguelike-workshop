using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] public UnityEvent<Projectile> ProjectileShootEvent;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] public float ShootingSpeed = 5;
    [SerializeField] public float ShootingCooldown = 1;
    [SerializeField] public float Damage = 1;
    [SerializeField] public float ProjectileLifetime = 5;
    [SerializeField] public float ProjectileScale = 1;

    float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {
        // So the player can shoot immediately upon loading in
        lastShotTime = -ShootingCooldown - 1;
    }

    // pew pew, called by Enemy or Player Attack scripts
    public void Shoot(Vector2 direction)
    {
        // if we are not on cooldown
        if (Time.time - lastShotTime > ShootingCooldown)
        {

            // Load in a projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.localScale *= ProjectileScale;

            // Set projectile lifetime and damage
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.setLifetimeAndDamage(ProjectileLifetime, Damage);
            projectileScript.SourceTag = gameObject.tag;

            // give it velocity so it can go pew pew
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x, direction.y, 0) * ShootingSpeed;

            // reset the last shot time to the current time
            lastShotTime = Time.time;

            ProjectileShootEvent.Invoke(projectileScript);
        }
    }

}
