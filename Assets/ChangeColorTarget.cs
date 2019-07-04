using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeColorTarget : MonoBehaviour, ITargetable
{
    public void OnHit(GameObject projectile, CollisionData collisionData)
    {
        this.GetComponent<Renderer>().material.color = GetRandomColor();
    }

    private static Color GetRandomColor()
    {
        return new Color(Utility.RandomNumber, Utility.RandomNumber, Utility.RandomNumber, 1f);
    }
}

public static class Utility
{
    //Returns a random number in range of 0 .. 1
    public static float RandomNumber => Random.Range(0f, 1f);
    
}