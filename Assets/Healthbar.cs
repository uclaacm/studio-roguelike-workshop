using UnityEngine;

public class Healthbar : MonoBehaviour
{
    // Attach to entity at position offset:
    private Entity entity;
    public Vector3 offset;

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
    }
}
