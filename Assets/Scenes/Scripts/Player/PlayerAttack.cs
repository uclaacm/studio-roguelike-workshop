using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    public void OnClick()
    {

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        //get world space of object
        Vector3 mouseWorldPos =
            camera.ScreenToWorldPoint(mouseScreenPos + new Vector3(0, 0, camera.nearClipPlane));

        Vector2 dir = new Vector2(mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y);
        dir.Normalize();
        Debug.Log(dir);

        GetComponent<Weapon>().Shoot(dir);

        // float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        // Quaternion bubbleRotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
