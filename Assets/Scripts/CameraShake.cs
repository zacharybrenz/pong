using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple camera shake implementation.
// Pulled from another project.
public class CameraShake : MonoBehaviour
{  
    private static CameraShake _instance = null;

    Vector3 _origin = Vector3.zero;

    private float _duration = 0f;
    private float _magnitude = 0.7f;
    private const float MAGNITUDE_MULT = 0.5f;
    private float _damping = 1.0f;
    
    private void Awake()
    {
        if (_instance == null) { _instance = this; }

        _origin = transform.localPosition;
    }

    private void Update()
    {
        if (_duration > 0.0f)
        {
            transform.localPosition = _origin + Random.insideUnitSphere * _magnitude * MAGNITUDE_MULT;
            
            _duration -= Time.deltaTime * _damping;
        }

        else
        {
            _duration = 0.0f;
            transform.localPosition = _origin;
        }
    }

    public void ShakeCamera(float duration, float? magnitude = null, float? damping = null)
    {
        if (magnitude != null) { _magnitude = (float)magnitude; }
        if (damping != null) { _damping = (float)damping; }

        _duration = duration;
    }

    public static void Shake(float duration, float? magnitude = null, float? damping = null)
    {
        _instance.ShakeCamera(duration, magnitude, damping);
    }
}
