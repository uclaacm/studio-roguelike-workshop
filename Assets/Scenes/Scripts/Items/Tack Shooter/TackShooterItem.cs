using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackShooterItem : MonoBehaviour
{
    [SerializeField] int nShots = 8;

    Weapon weapon;

    void Awake(){
        weapon = transform.parent.GetComponent<Weapon>();
        weapon.ProjectileShootEvent.AddListener(OnProjectileShoot);
    }

    void OnProjectileShoot(Projectile projectile) {
        var projectileRB = projectile.GetComponent<Rigidbody2D>();
        for(int i = 1; i < nShots; ++i){
            var newProjectile = Instantiate(projectile.gameObject);
            var rb = newProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = Quaternion.Euler(0, 0, 360 / nShots * i) * projectileRB.velocity;
        }
    }
}
