using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public static CameraController SingleTon;
    
    [SerializeField]
    private float _rotationSpeed;

    [SerializeField] 
    private float _maxXAngle;

    [SerializeField] 
    private float _minXAngle;
    
    private Camera _camera;

    private float xAngle = 0;

    private void Awake()
    {
        SingleTon = this;
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        var input = GetHorizontalRotationInput();

        var currentRotation = _camera.transform.rotation.eulerAngles;

        GetClampedVerticalInput();
        
        currentRotation = new Vector3(xAngle, currentRotation.y + input, currentRotation.z);

        transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void  GetClampedVerticalInput()
    {
        var verticalInput = GetVerticalRotationInput() + xAngle;

        xAngle = Mathf.Clamp(verticalInput, _minXAngle, _maxXAngle);
    }
    
    
    protected virtual float GetHorizontalRotationInput()
    {
        return Input.GetAxis("Horizontal") * _rotationSpeed;
    }

    protected virtual float GetVerticalRotationInput()
    {
        return Input.GetAxis("Vertical") * _rotationSpeed;
    }
    
    
    
}
