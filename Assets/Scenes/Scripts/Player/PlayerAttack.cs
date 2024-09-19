using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    bool attackHeld = false;
    new Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if(PauseMenuUI.isPaused){
            return;
        }
        if (attackHeld)
        {
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
            //get world space of object
            Vector3 mouseWorldPos =
                camera.ScreenToWorldPoint(mouseScreenPos + new Vector3(0, 0, camera.nearClipPlane));

            Vector2 dir = new Vector2(mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y);
            dir.Normalize();

            GetComponent<Weapon>().Shoot(dir);
        }
    }

    public void OnClick(InputValue value)
    {
        if(PauseMenuUI.isPaused){
            return;
        }
        attackHeld = value.isPressed;
    }
}
