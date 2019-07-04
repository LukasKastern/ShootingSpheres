using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public static List<Projectile> Projectiles = new List<Projectile>();

    private float _velocity => GetComponent<Rigidbody>().velocity.magnitude;

    [SerializeField]
    private DistanceUI _distanceUI;

    private float _spawnedAtTime = 0;
    
    private void Awake()
    {
        _spawnedAtTime = Time.time;
        
        Projectiles.Add(this);
    }

    private void OnDestroy()
    {
        Projectiles.Remove(this);
    }

    private void FixedUpdate()
    {
        if(Time.time - _spawnedAtTime < 0.1f)
            return;
        
        if (_velocity < 0.1f)
        {
            _distanceUI.Disable();
        }
        
        else if (_velocity > 0.1f)
        {
            _distanceUI.Enable();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var collisionData = CollisionData.GetCollisionData(collision);
     
        foreach (var target in collision.transform.GetComponents<ITargetable>())
        {
            target.OnHit(this.gameObject, collisionData);   
        }
    }
}