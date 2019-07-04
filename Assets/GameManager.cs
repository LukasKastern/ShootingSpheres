using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void ClearProjectiles()
    {
        foreach (var spawnedObject in Projectile.Projectiles)
        {
            GameObject.Destroy(spawnedObject.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearProjectiles();
        }
    }
}