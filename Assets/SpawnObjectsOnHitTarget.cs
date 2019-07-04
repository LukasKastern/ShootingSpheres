using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class SpawnObjectsOnHitTarget : MonoBehaviour, ITargetable
    {
        [SerializeField] 
        private Rigidbody _objectToSpawnOnHit;

        [SerializeField] 
        private int _amountOfObjectsToSpawn;
        
        [SerializeField]
        private float _spawnRadius;

        [SerializeField]
        private float _minVelocityAmplifier;

        [SerializeField] 
        private float _maxVelocityAmplifier;

        [SerializeField] 
        private float _rotationAmplifier;
        
        private CollisionData _collisionData;

        public void OnHit(GameObject projectile, CollisionData collisionData)
        {
            _collisionData = collisionData;
            
            GameObject.Destroy(projectile.gameObject);
            
            SpawnObjects();
        }

        private void SpawnObjects()
        {
            for (int i = 0; i < _amountOfObjectsToSpawn; ++i)
            {
                var spawnPoint = GetObjectSpawnPoint();

                var direction = spawnPoint - transform.position;

                var rotation = Quaternion.LookRotation(direction);

                var spawnedObject = GameObject.Instantiate(_objectToSpawnOnHit, spawnPoint, rotation);

                var velocity = GetVelocity(_collisionData.RelativeVelocity);

                spawnedObject.AddForce(direction * velocity, ForceMode.VelocityChange);
                
                spawnedObject.AddTorque(GetRandomTorque());
            }
        }

        private Vector3 GetRandomTorque()
        {
            return new Vector3(Utility.RandomNumber, Utility.RandomNumber, Utility.RandomNumber) * _rotationAmplifier;
        }

        private float GetVelocity(float relativeVelocity)
        {
            var amplifier = Mathf.Lerp(_minVelocityAmplifier, _maxVelocityAmplifier, Random.Range(0f, 1f));

            return relativeVelocity * amplifier;
        }
        
        private Vector3 GetObjectSpawnPoint()
        {    
            return transform.position + Random.insideUnitSphere * _spawnRadius;
        }
    }
}