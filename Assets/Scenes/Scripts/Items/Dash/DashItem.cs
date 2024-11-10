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

    void Awake(){
        entity = transform.parent.GetComponent<Entity>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value) {
        var dir = value.Get<Vector2>();
        dashEndDir = dir;
        if(dashing) return;
        if(dir == Vector2.zero) return;

        if(Time.time < lastMoveTime + doubleTapDelay && lastMoveDir == dir){
            Dash(dir);
        }
        else{
            lastMoveTime = Time.time;
            lastMoveDir = dir;
        }
    }

    void FixedUpdate(){
        if(dashing){
            if(Time.time > dashStartTime + dashDuration){
                StopDash();
            }
            else {
                rb.velocity = dashDir * entity.stats.MovementSpeed * dashSpeedMult;
            }
        }
    }

    void Dash(Vector2 direction){
        if(Time.time < lastDashTime + dashCooldown) return;
        dashing = true;
        dashDir = direction;
        dashStartTime = Time.time;
    }

    void StopDash(){
        dashing = false;
        rb.velocity = dashEndDir * entity.stats.MovementSpeed;
        lastDashTime = Time.time;
    }
}
