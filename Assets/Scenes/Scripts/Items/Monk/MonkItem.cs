using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This item makes it so every third shot
/// has more damage, but a longer cooldown
/// </summary>
public class MonkItem : MonoBehaviour
{
    [Header("Entity Modifications")]
    [SerializeField] float speedMult = 1.5f;

    [Header("Weapon Modifications")]
    [SerializeField] float lifetimeMult = 0.01f;
    [SerializeField] float shotSpeedMult = 3f;
    [SerializeField] float damageMult = 1.25f;
    [SerializeField] float cooldownMult = 0.33f;

    [Header("Third Shot Modifications")]
    [SerializeField] float thirdShotDamageMult = 2;
    [SerializeField] float thirdShotScaleMult = 2;
    [SerializeField] float thirdShotCooldownMult = 3f;

    Weapon weapon;
    Entity entity;

    int shotNumber = 0;

    void Start()
    {
        // TODO: get the weapon and register an event
        // listener for OnProjectileShoot

        // TODO: modify weapon stats

        // TODO: get entity and modify entity movement speed
    }

    void OnProjectileShoot(Projectile projectile)
    {
        shotNumber++;

        // useful tools: 
        //      to check if we are on the third shot, you
        //      can check if shotNumber % 3 == 0
        //
        //      to check if we are on the shot after the third
        //      shot, check if shotNumber % 3 == 1 && shotNumber > 3

        // TODO: if we have fired 3 shots, make
        // the projectile do more damage,
        // increase its projectile.transform.localScale,
        // and increase the weapon cooldown

        // TODO: if we are on the shot immediately after
        // a third shot, undo the increase of weapon cooldown
    }
}
