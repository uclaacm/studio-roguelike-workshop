using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles melee enemy movement towards the player
/// </summary>
public class MeleeEnemyMovement : MonoBehaviour
{

    // melee enemy rigidbody
    [SerializeField] public new Rigidbody2D rigidbody;

    // Determining monster detection range
    [SerializeField] public float Range = 5.0f;

    // enemy distance from player
    float distance;

    // the direction from the enemy -> player
    Vector2 direction;

    Entity player;
    Entity entity;

    void Start(){
        player = Player.Instance;
        entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player is dead, do nothing
        if(!player) return;
        // Converts player transform to 2d vector
        var playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        // Finds the distance between the player (player.position) and the enemy (rigidbody.position)
        distance = Vector2.Distance(playerPosition, rigidbody.position);

        // If the distance between the player and enemy is less than the detection range (ie. the player is in range)
        if (distance < Range)
        {
            // Finds the direction from the enemy to player (vector math, subtract player - enemy
            // to find a vector pointing from the enemy towards the player
            direction = (playerPosition - rigidbody.position).normalized;

            // changes enemy velocity to point in the right direction scaled by MovementSpeed
            rigidbody.velocity = direction * entity.stats.MovementSpeed;
        } else
        {
            // if the player isn't in range, the enemy stops moving
            rigidbody.velocity = Vector2.zero;
        }
    }
}
