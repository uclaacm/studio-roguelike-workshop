using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostItem : MonoBehaviour
{
    [SerializeField] float damageMult = 1.2f;

    void Start(){
        var weapon = transform.parent.GetComponent<Weapon>();
        if(weapon){
            weapon.Damage *= damageMult;
        }
    }
}
