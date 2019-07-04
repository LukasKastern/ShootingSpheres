using UnityEngine;

public class DestroyOnHitTarget : MonoBehaviour, ITargetable
{
    public void OnHit(GameObject projectile, CollisionData collisionData)
    {
        GameObject.Destroy(this.gameObject);
    }
}