using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    [SerializeField] float range = 5;
    [SerializeField] float lookAhead = 3;

    Entity entity;
    Rigidbody2D rb;

    Vector2 curMovementDir = Vector2.down;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();
    }

    void Update(){
        if(Player.Instance && Vector3.Distance(transform.position, Player.Instance.transform.position) < range){
            MoveToPlayer();
        }
        else {
            MoveIdle();
        }
    }

    void MoveToPlayer(){
        rb.velocity = entity.stats.MovementSpeed * (Player.Instance.transform.position - transform.position).normalized;
    }

    void MoveIdle(){
        rb.velocity = entity.stats.MovementSpeed * curMovementDir;
        // if we are about to run into something, rotate 90 degrees
        Physics2D.queriesHitTriggers = false;
        Physics2D.queriesStartInColliders = false;
        var hit = Physics2D.Raycast(transform.position, curMovementDir, lookAhead);
        if(hit.collider){
            Debug.Log(hit.collider);
            curMovementDir = Quaternion.Euler(0, 0, 90) * curMovementDir;
        }
    }
}
