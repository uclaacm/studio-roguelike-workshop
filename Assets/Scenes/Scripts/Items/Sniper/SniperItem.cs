using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperItem : MonoBehaviour
{
    [SerializeField] float velocityMult = 3;
    [SerializeField] float damageMult = 3;
    [SerializeField] float shotCooldownMult = 3f;

    void Start(){
        var weapon = transform.parent.GetComponent<Weapon>();
        weapon.ShootingSpeed *= velocityMult;
        weapon.ShootingCooldown *= shotCooldownMult;
        weapon.Damage *= damageMult;
    }
}
