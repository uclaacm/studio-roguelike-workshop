using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float initialAttackDelay = 2;

    Weapon weapon;

    float startTime;

    void Start(){
        weapon = GetComponent<Weapon>();
        startTime = Time.time;
    }

    void Update(){
        // if player is dead, do nothing
        if(Player.IsDead) return;
        if(Time.time < startTime + initialAttackDelay) return;
        var playerPos = Player.Instance.transform.position;
        var displacement = playerPos - transform.position;
        var distance = displacement.magnitude;

        if(distance < range){
            weapon.Shoot(displacement / distance);
        }
    }
}
