using UnityEngine;
public class TripleShotItem : MonoBehaviour
{
    [SerializeField] float spread = 30.0f;

    Weapon weapon;

    void Start()
    {
        weapon = transform.parent.GetComponent<Weapon>();
        weapon.ProjectileShootEvent.AddListener(OnProjectileFire);
    }

    void OnProjectileFire(Projectile projectile)
    {
        // TODO: copy projectile GameObject and rotate it by spread degrees
        // in either direction
        //
        // tools:
        //      use Instantiate(gameObjectToCopy) to copy a GameObject
        //      you can rotate an object by doing
        //          transform.eulerAngle += Vector3.forward * angle;
    }
}
