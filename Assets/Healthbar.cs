using UnityEngine;

public class Healthbar : MonoBehaviour
{
    // Attach to entity at position offset:
    private Entity entity;
    public Vector3 offset;
    // Update foreground position/scale:
    [SerializeField] private Transform foreground;

    void Start()
    {
        entity = transform.root.GetComponent<Entity>();
        if (!entity)
        {
            Debug.LogError("Healthbar prefab must be a child of an entity");
            Destroy(this.gameObject);
            return;
        }
        offset = transform.position - entity.transform.position;
    }

    void Update()
    {
        // Attach to entity at position offset:
        transform.position = entity.transform.position + offset;
        transform.rotation = Quaternion.identity;

        // Update foreground localposition/scale:
        // at 100%, foreground is pos (0,0) and scale (8,1)
        // at 0%, foreground is pos (-4,0) and scale (0,1)
        float healthPercent = entity.CurrentHealth / entity.stats.MaxHealth;
        foreground.localPosition = Vector2.Lerp(new Vector2(-4, 0), Vector2.zero, healthPercent);
        foreground.localScale = Vector2.Lerp(new Vector2(0, 1), new Vector2(8, 1), healthPercent);
    }
}
