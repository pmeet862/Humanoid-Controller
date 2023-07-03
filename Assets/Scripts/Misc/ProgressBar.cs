using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Slider _progressBar;
    [SerializeField] HumanoidLandInput _input;

    public float _progressBarVal { get; private set; } = 0.0f;

    float _targetVal = 0.0f;

    float _fillSpeed = 0.0f;

    private void OnEnable()
    {
        _progressBar.value = 0.0f;
    }


    private void Update()
    {
        if (_progressBar.value < _targetVal && _input.TeleportIsPressed)
        {
            _progressBar.value += _fillSpeed * Time.deltaTime;
        }
        else
        {
            // Debug.Log("Reset");
            _progressBar.value = 0.0f;
        }
        _progressBarVal = _progressBar.value;
    }

    public void IncreamentProgress(float valToIncrease)
    {
        _targetVal = _progressBar.value + valToIncrease;
        _fillSpeed = valToIncrease;
    }
}
