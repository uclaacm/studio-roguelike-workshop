using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] float shootingSpeed = 15.0f;

    [SerializeField] float playerAttackDelay = 0.5f;
    new Camera camera;
    float lastShootTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (Time.time - lastShootTime > playerAttackDelay)
        {
            lastShootTime = Time.time;
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
            //get world space of object
            Vector3 mouseWorldPos =
                camera.ScreenToWorldPoint(mouseScreenPos + new Vector3(0, 0, camera.nearClipPlane));

            Vector2 dir = new Vector2(mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y);
            dir.Normalize();
            Debug.Log(dir);
            // float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            // Quaternion bubbleRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            bubble.GetComponent<Rigidbody2D>().velocity = new Vector3(dir.x, dir.y, 0) * shootingSpeed;
        }
    }
}
