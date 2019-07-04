using UnityEngine;

public interface ITargetable 
{ 
    void OnHit(GameObject projectile, CollisionData collisionData);
}