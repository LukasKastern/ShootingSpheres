using UnityEngine;
using UnityEngine.UI;

public class ProjectileShotUI : MonoBehaviour
{
    [SerializeField] 
    private Text _projectilesShotDisplay;

    private int _projectilesShot;
    
    private void Awake()
    {
        SphereSpawner.OnProjectileSpawned += IncreaseProjectileNumber;
    }

    private void OnDestroy()
    {
        SphereSpawner.OnProjectileSpawned -= IncreaseProjectileNumber;
    }
    
    private void IncreaseProjectileNumber()
    {
        ++_projectilesShot;

        _projectilesShotDisplay.text = _projectilesShot.ToString();
    }
    

}