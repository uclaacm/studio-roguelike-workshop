using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5.0f;

    Vector2 lastMovementDir = Vector2.zero;

    /// <summary>
    /// Get's sent by PlayerInput when
    /// the Move action is triggered
    /// (Defaults to WASD)
    /// </summary>
    /// <param name="inputValue">
    /// Contains the data about the input,
    /// notably inputValue.Get<Vector2>() is the
    /// direction of movement
    /// </param>
    void OnMove(InputValue inputValue){
        lastMovementDir = inputValue.Get<Vector2>();
    }


    /// <summary>
    /// Gets called every physics frame.
    /// We apply movement here so that
    /// we keep moving right even
    /// after we hit a wall
    /// </summary>
    void FixedUpdate(){
        rb.velocity = lastMovementDir * speed;
    }
}
