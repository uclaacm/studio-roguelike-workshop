using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadShotItem : MonoBehaviour
{
    Weapon weapon;

    void Awake(){
        weapon = transform.parent.GetComponent<Weapon>();
        weapon.ProjectileShootEvent.AddListener(OnProjectileShoot);
    }

    void OnProjectileShoot(Projectile projectile) {
        // copy the projectile twice, one rotated 30 degrees clockwise
        // one rotated 30 degrees counterclockwise
        var cw = Instantiate(projectile.gameObject);
        var ccw = Instantiate(projectile.gameObject);
        var behind = Instantiate(projectile.gameObject);

        var projectileRB = projectile.GetComponent<Rigidbody2D>();
        var cwRB = cw.GetComponent<Rigidbody2D>();
        var ccwRB = ccw.GetComponent<Rigidbody2D>();
        var behindRB = behind.GetComponent<Rigidbody2D>();

        cwRB.velocity = Quaternion.Euler(0, 0, -90) * projectileRB.velocity;
        ccwRB.velocity = Quaternion.Euler(0, 0, 90) * projectileRB.velocity;
        behindRB.velocity = Quaternion.Euler(0, 0, 180) * projectileRB.velocity;
    }
}
