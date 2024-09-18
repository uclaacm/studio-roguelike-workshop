using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This movement pattern tries to back off whenever
/// it gets too close to the player.
/// </summary>
public class SniperEnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float backOffRange = 8;

    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        // if player is dead, do nothing
        if(Player.IsDead) return;
        var playerPos = Player.Instance.transform.position;
        var displacement = playerPos - transform.position;
        var distance = displacement.magnitude;

        if(distance < backOffRange){
            rb.velocity = -displacement / distance * speed;
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }
}