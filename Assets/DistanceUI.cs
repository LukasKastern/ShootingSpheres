using System;
using UnityEngine;
using UnityEngine.UI;

public class DistanceUI : MonoBehaviour
{
    public Camera Camera => CameraController.SingleTon.GetComponent<Camera>();

    [SerializeField] 
    private Text distanceValue;

    [SerializeField] 
    private Vector3 _cameraOffset;

    [SerializeField] 
    private GameObject _target;

    [SerializeField] 
    private Text _textPrefab;

    private void Awake()
    {
        var root = GameObject.FindObjectOfType<Canvas>();

        distanceValue = GameObject.Instantiate(_textPrefab, Vector3.up, Quaternion.identity, root.transform);
    }

    private void LateUpdate()
    {
        if (Camera.WorldToScreenPoint(_target.transform.position).z < 0)
        {
            distanceValue.enabled = false;
        }

        else
            distanceValue.enabled = true;

        distanceValue.transform.position = Camera.WorldToScreenPoint(_target.transform.position) + _cameraOffset;
        
        distanceValue.text = Vector3.Distance(CameraController.SingleTon.transform.position, _target.transform.position).ToString("0.00");
    }

    public void Disable()
    {
        this.enabled = false;
        distanceValue.enabled = false;
    }

    public void Enable()
    {
        this.enabled = true;
        distanceValue.enabled = true;
    }

    private void OnDestroy()
    {
        if(distanceValue!= null)
            GameObject.Destroy(distanceValue.gameObject);
    }
}