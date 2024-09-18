using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    [SerializeField] float cooldown = 3;

    string entityTag;
    SpriteRenderer spriteRenderer;

    bool onCooldown = false;
    float cooldownStartTime;

    void Awake(){
        entityTag = transform.parent.tag;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(onCooldown && Time.time > cooldownStartTime + cooldown){
            EndCooldown();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(onCooldown) return;

        var projectile = other.gameObject.GetComponent<Projectile>();
        // if other is a projectile
        if(projectile){
            // spawning from an enemy
            if(projectile.SourceTag != entityTag){
                Destroy(projectile.gameObject);
                StartCooldown();
            }
        }
    }

    void StartCooldown(){
        onCooldown = true;
        cooldownStartTime = Time.time;
        spriteRenderer.enabled = false;
    }

    void EndCooldown(){
        onCooldown = false;
        spriteRenderer.enabled = true;
    }
}
