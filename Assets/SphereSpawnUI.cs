using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SphereSpawnUI : MonoBehaviour
{
    [SerializeField] 
    private Color _minValueColor;

    [SerializeField] 
    private Color _maxValueCOlor;
    
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    
    public void SetCurrentValue(float value)
    {
        _slider.fillRect.GetComponent<Image>().color = Color.Lerp(_minValueColor, _maxValueCOlor, value);
        
        _slider.value = value;
    }
    
}