using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class SphereSpawner : MonoBehaviour
{
    public static Action OnProjectileSpawned;
    
    [SerializeField] 
    private Rigidbody _projectile;
    
    [FormerlySerializedAs("sphereSpawnUI")] [SerializeField] 
    private SphereSpawnUI _sphereSpawnUI;
    
    [SerializeField]
    private Transform _spawnPoint;
    
    [SerializeField]
    private float _minSpawnVelocity;

    [SerializeField] 
    private float _maxSpawnVelocity;

    [SerializeField] 
    private float _timeToMaxVelocity;
    
    private bool isSpawningSphere = false;

    private void Awake()
    {
        this.enabled = VerifyReferences();
    }

    //Returns true if all References are set up correctly
    private bool VerifyReferences()
    {
        if (!_sphereSpawnUI)
        {
            Debug.LogError("There's no Sphere Spawn UI reference on the Sphere Spawner");
            return false;
        }
            

        if (!_spawnPoint)
        {
            Debug.LogError("There's no Spawn Point reference on the Sphere Spawner");
            return false;
        }
            

        return true;
    }

    private bool WantsToShoot()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }

    private bool IsChargingShot()
    {
        return !Input.GetKeyUp(KeyCode.Space) && !Input.GetMouseButtonUp(0);
    }
    
    private void Update()
    {
        if(isSpawningSphere || !WantsToShoot())
            return;

        StartCoroutine(PrepareSphereSpawn());

    }

    private IEnumerator PrepareSphereSpawn()
    {
        isSpawningSphere = true;
        
        float elapsedTime = 0f;
        
        while (IsChargingShot())
        {
            elapsedTime += Time.deltaTime;

            float currentNormalizedTime = elapsedTime / _timeToMaxVelocity;
            
            _sphereSpawnUI.SetCurrentValue(currentNormalizedTime);
            
            

            yield return null;
        }

        isSpawningSphere = false;
        
        _sphereSpawnUI.SetCurrentValue(0f);

        var normalizedTimeFactor = elapsedTime / _timeToMaxVelocity;

        var velocity = Mathf.Lerp(_minSpawnVelocity, _maxSpawnVelocity, normalizedTimeFactor);
        
        SpawnSphere(velocity);
    }

    private void SpawnSphere(float velocity)
    {
        var projectile = GameObject.Instantiate(_projectile, _spawnPoint.position, Quaternion.identity);

        var direction = _spawnPoint.TransformDirection(Vector3.forward * velocity);
        
        projectile.AddForce(direction, ForceMode.VelocityChange);
        
        OnProjectileSpawned?.Invoke();
    }
}