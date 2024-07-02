using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles melee enemy movement towards the player
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    
    // melee enemy rigidbody
    [SerializeField] public new Rigidbody2D rigidbody;

    // Used to determine movement speed
    [SerializeField] public float MovementSpeed = 2.0f;

    // Determining monster detection range
    [SerializeField] public float Range = 5.0f;
    
    // player location
    [SerializeField] public Transform Player;

    // enemy distance from player
    float distance;

    // the direction from the enemy -> player
    Vector2 direction;

    // For converting the player transform to a 2d vector
    Vector2 playerPosition;

    // Update is called once per frame
    void Update()
    {
        // Converts player transform to 2d vector
        playerPosition = new Vector2(Player.position.x, Player.position.y);

        // Finds the distance between the player (player.position) and the enemy (rigidbody.position)
        distance = Vector2.Distance(playerPosition, rigidbody.position);

        // If the distance between the player and enemy is less than the detection range (ie. the player is in range)
        if (distance < Range)
        {
            // Finds the direction from the enemy to player (vector math, subtract player - enemy
            // to find a vector pointing from the enemy towards the player
            direction = (playerPosition - rigidbody.position).normalized;

            // changes enemy velocity to point in the right direction scaled by MovementSpeed
            rigidbody.velocity = direction * MovementSpeed;
        } else
        {
            // if the player isn't in range, the enemy stops moving
            rigidbody.velocity = Vector2.zero;
        }
    }
}
