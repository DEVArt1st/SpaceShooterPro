using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform _transform;
    private float _shakeDuration = 0f;
    [SerializeField]
    private float _shakeMagnitude = 0.7f;
    private float _dampingSpeed = 1.0f;

    Vector3 _initialPosition;

    private void Awake()
    {
        if (_transform == null)
        {
            _transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void OnEnable()
    {
        _initialPosition = _transform.position;
    }

    private void Update()
    {
        if (_shakeDuration > 0)
        {
            _transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;

            _shakeDuration -= Time.deltaTime * _dampingSpeed;
        }
        else
        {
            _shakeDuration = 0f;
            _transform.localPosition = _initialPosition;
        }
    }

    public void TriggerShake()
    {
        _shakeDuration = 2.0f;
    }
}
