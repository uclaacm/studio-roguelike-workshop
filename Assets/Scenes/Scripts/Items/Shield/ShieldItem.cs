using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This item spawns a collider
/// and if any projectiles enter the collider
/// whose target tag is the player's tag,
/// it destroys them and then starts a cooldown
/// </summary>
public class ShieldItem : MonoBehaviour
{
    [SerializeField] float cooldown = 1.0f;

    string entityTag;
    SpriteRenderer spriteRenderer;

    bool onCooldown = false;
    float cooldownEndTime = float.NegativeInfinity;

    void Awake()
    {
        entityTag = transform.parent.tag;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // TODO: reset onCooldown if the time
        // has passed cooldownEndTime
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (onCooldown) return;

        // TODO: check if the other collider has a Projectile script
        // if it does, destroy the projectile GameObject if its 
        // SourceTag is not the same as entityTag
        // Then, hide the shield sprite and start the cooldown
    }
}
