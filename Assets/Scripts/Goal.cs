using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// The goal zone that the ball can score in
public class Goal : MonoBehaviour
{
    [SerializeField]
    private Paddle _player = null;

    [SerializeField]
    private UnityEvent _onScore = new UnityEvent();

    [SerializeField]
    private ParticleSystem _scoreParticle = null;

    private AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // If the ball enters the goal zone,
    // play a particle and sound effect,
    // shake the camera,
    // and then invoke any events (e.g., score a point)
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            _scoreParticle.transform.position = col.ClosestPoint(col.transform.position);
            ParticleSystem.MainModule module = _scoreParticle.main;
            module.startColor = _player.Color;
            _scoreParticle.Play();
            _audioSource.Play();
            CameraShake.Shake(0.3f);
            _onScore.Invoke();
        }
    }
}
