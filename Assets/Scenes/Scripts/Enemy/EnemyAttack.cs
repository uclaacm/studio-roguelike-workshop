using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float range;

    Weapon weapon;

    void Start(){
        weapon = GetComponent<Weapon>();
    }

    void Update(){
        var playerPos = Player.Instance.transform.position;
        var displacement = playerPos - transform.position;
        var distance = displacement.magnitude;

        if(distance < range){
            weapon.Shoot(displacement / distance);
        }
    }
}
