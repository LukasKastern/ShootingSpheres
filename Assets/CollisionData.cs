using UnityEngine;

public struct CollisionData
{
    public static CollisionData GetCollisionData(Collision collisionToGetTheDataOf)
    {
        return new CollisionData
        {
            RelativeVelocity = collisionToGetTheDataOf.relativeVelocity.magnitude,
            Normal = collisionToGetTheDataOf.GetContact(0).normal,
        };
    }
    
    public float RelativeVelocity;
    public Vector3 Normal;
}