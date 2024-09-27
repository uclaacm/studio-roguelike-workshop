using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] float leftRightBiasDegrees = 10;

    const int DIR_FRONT = 0;
    const int DIR_BACK = 1;
    const int DIR_LEFT = 2;
    const int DIR_RIGHT = 3;

    void FixedUpdate(){
        if(rb.velocity == Vector2.zero){
            animator.SetBool("Walking", false);
        }
        else {
            animator.SetBool("Walking", true);

            // math: a dot b = |a||b|cos(theta)
            // so cos(theta) = a dot b / (|a| |b|)
            var angle = Vector2.SignedAngle(Vector2.right, rb.velocity);

            if(angle > -45 - leftRightBiasDegrees && angle < 45 + leftRightBiasDegrees){
                animator.SetInteger("Dir", DIR_RIGHT);
            }
            else if(angle >= 90 - 45 + leftRightBiasDegrees && angle < 90 + 45 - leftRightBiasDegrees){
                animator.SetInteger("Dir", DIR_BACK);
            }
            else if(angle > 180 - 45 - leftRightBiasDegrees && angle < 180 + 45 + leftRightBiasDegrees
                || angle > -180 - 45 - leftRightBiasDegrees && angle < -180 + 45 + leftRightBiasDegrees){
                animator.SetInteger("Dir", DIR_LEFT);
            }
            else if(angle > -90 - 45 + leftRightBiasDegrees && angle < -90 + 45 - leftRightBiasDegrees){
                animator.SetInteger("Dir", DIR_FRONT);
            }
        }
    }
}
