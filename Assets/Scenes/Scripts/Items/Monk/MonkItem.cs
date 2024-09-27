using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonkItem : MonoBehaviour
{
    [SerializeField] float speedMult = 1.5f;
    [SerializeField] float lifetimeMult = 0.01f;
    [SerializeField] float shotSpeedMult = 3f;
    [SerializeField] float damageMult = 1.25f;
    [SerializeField] float cooldownMult = 0.33f;
    [SerializeField] float thirdShotDamageMult = 2;
    [SerializeField] float thirdShotScaleMult = 2;
    [SerializeField] float thirdShotCooldownMult = 3f;

    Weapon weapon;
    Entity entity;

    int shotNumber = 0;

    void Awake(){
        weapon = transform.parent.GetComponent<Weapon>();
        weapon.ProjectileShootEvent.AddListener(OnProjectileShoot);

        weapon.ShootingSpeed *= speedMult;
        weapon.Damage *= damageMult;
        weapon.ShootingCooldown *= cooldownMult;
        weapon.ProjectileLifetime *= lifetimeMult;

        entity = transform.parent.GetComponent<Entity>();
        entity.stats.MovementSpeed *= speedMult;
    }

    void OnProjectileShoot(Projectile projectile){
        ++shotNumber;
        if(shotNumber == 3){
            shotNumber = 0;

            projectile.Damage *= thirdShotDamageMult;
            projectile.transform.localScale *= thirdShotScaleMult;
            weapon.ShootingCooldown *= thirdShotCooldownMult;
        }
        else if (shotNumber == 1){
            weapon.ShootingCooldown /= thirdShotCooldownMult;
        }
    }
}
