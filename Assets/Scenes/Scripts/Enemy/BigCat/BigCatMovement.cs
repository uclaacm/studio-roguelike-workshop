using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCatMovement : MonoBehaviour
{
    enum State {
        Idle,
        Dash,
    }

    State state = State.Idle;

    float stateStartTime = 0;

    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        TransitionIdle();
    }

    void Update(){
        var delta = Time.time - stateStartTime;
        if (state == State.Dash && Player.Instance){
            if (delta < 0.2){
                transform.position = Vector3.Lerp(
                    transform.position,
                    Player.Instance.transform.position,
                    Time.deltaTime * 5
                );
            }
            else {
                TransitionIdle();
                transform.position = Vector3.Lerp(
                    transform.position,
                    Player.Instance.transform.position,
                    Time.deltaTime * 5
                );
            }
        }
    }

    void TransitionIdle(){
        state = State.Idle;
        if(Player.Instance) {
            rb.velocity = (transform.position - Player.Instance.transform.position).normalized;
        }
        IEnumerator Coro(){
            yield return new WaitForSeconds(2);
            TransitionRandomAttack();
        }

        StartCoroutine(Coro());
    }

    void TransitionRandomAttack(){
        TransitionDash();
    }

    void TransitionDash() {
        state = State.Dash;
        stateStartTime = Time.time;
    }
}
