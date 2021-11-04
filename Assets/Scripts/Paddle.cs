using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The paddle that the players will control
public class Paddle : MonoBehaviour
{
    private Rigidbody2D _rigidbody = null;

    private Collider2D _collider = null;

    private SpriteRenderer _sprite = null;

    private Animator _animator = null;

    private AudioSource _audioSource = null;

    [SerializeField]
    private Color _color = Color.red;

    public Color Color
    {
        get { return _color; }
        set { }
    }

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private KeyCode _upKey = KeyCode.UpArrow;

    [SerializeField]
    private KeyCode _downKey = KeyCode.DownArrow;

    private float _direction = 0.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _sprite.color = _color;
    }

    // Handle input from the player
    private void Update()
    {
        if (Input.GetKey(_upKey))
        {
            _direction = 1.0f;
        }

        else if (Input.GetKey(_downKey))
        {
            _direction = -1.0f;
        }

        else
        {
            _direction = 0.0f;
        }
    }

    // Apply movement input from Update
    private void FixedUpdate()
    {
        Vector2 pos = _rigidbody.position;
        pos.y +=  _direction * _speed;
        pos = ScreenBounds.Instance.Clamp(pos, _collider);
        _rigidbody.MovePosition(pos);
    }

    // If the ball is hit, change its color and
    // play an animation and sound effect.
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Ball ball = col.gameObject.GetComponent<Ball>();
            ball.SetColor(_color);
            _animator.SetTrigger("Bounce");
            _audioSource.Play();
        }
    }
}
