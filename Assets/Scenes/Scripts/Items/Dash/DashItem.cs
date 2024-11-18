using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashItem : MonoBehaviour
{
    [SerializeField] float doubleTapDelay = 0.3f;
    [SerializeField] float dashSpeedMult = 10f;
    [SerializeField] float dashDuration = 0.1f;
    [SerializeField] float dashCooldown = 2f;

    float lastMoveTime = float.NegativeInfinity;
    Vector2 lastMoveDir = Vector2.zero;

    float dashStartTime;
    Vector2 dashDir;
    bool dashing = false;

    Vector2 dashEndDir;
    float lastDashTime = float.NegativeInfinity;

    Entity entity;
    Rigidbody2D rb;

    void Awake()
    {
        entity = transform.parent.GetComponent<Entity>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        var dir = value.Get<Vector2>();
        dashEndDir = dir;
        if (dashing) return;
        if (dir == Vector2.zero) return;

        // only dash if time between previous movement input and new input is less than 
        // doubleTapDelay and if inputs were in same direction 
        if (Time.time < lastMoveTime + doubleTapDelay && lastMoveDir == dir)
        {
            // TODO: call Dash() with current direction

        }
        else
        {
            // TODO: set lastMoveTime to current time
            // TODO: set lastMoveDir to current direction

        }
    }

    void FixedUpdate()
    {
        if (dashing)
        {
            // stop dashing after dash duration is over
            if (Time.time > dashStartTime + dashDuration)
            {
                // TODO: call StopDash()

            }
            else
            {
                // TODO: set rb's velocity to dashDir multiplied by entity.stats.MovementSpeed 
                // multiplied by dashSpeedMult

            }
        }
    }

    void Dash(Vector2 direction)
    {
        if (Time.time < lastDashTime + dashCooldown) return;

        // TODO: set dashing equal to true
        // set dashDir to direction
        // set dashStartTime to current time

    }

    void StopDash()
    {
        // TODO: set dashing to false
        // set rb's velocity to dashEndDir multiplied by entity.stats.MovementSpeed
        // set lastDashTime to current time

    }
}
