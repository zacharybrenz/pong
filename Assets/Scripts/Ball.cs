using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The ball that will bounce around
public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody = null;
    private SpriteRenderer _sprite = null;

    [SerializeField]
    private float _launchSpeed = 400.0f;

    [SerializeField]
    private float _launchDelay = 3.0f;

    private float _launchTimer = 0.0f;
    private bool _launch = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Reset the ball's position to the origin.
    public void Reset()
    {
        transform.position = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
        SetColor(Color.white);
    }

    // Prepares the next launch of the ball.
    public void Serve()
    {
        _launchTimer = _launchDelay;
        _launch = true;
    }

    // Applies force to the ball in a randomized direction
    private void Launch()
    {
        _rigidbody.velocity = Vector2.zero;

        float angle = Random.Range(30, 60);
        angle += Random.Range(0, 4) * 90.0f;
        angle = Mathf.Deg2Rad * angle;
        
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        Vector2 dir = new Vector2(x, y);
        dir = dir.normalized;

        _rigidbody.AddForce(dir * _launchSpeed);
    }

    // Sets the color of the ball's sprite
    public void SetColor(Color color)
    {
        _sprite.color = color;
    }

    // After the provided delay has passed,
    // launches the ball in a random direction.
    private void Update()
    {
        if (_launch)
        {
            _launchTimer -= Time.deltaTime;
            if (_launchTimer <= 0.0f)
            {
                Launch();
                _launch = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // Failsafe for if ball breaks boundaries
        if (Mathf.Abs(_rigidbody.position.x) > 1000.0f || Mathf.Abs(_rigidbody.position.y) > 1000.0f)
        {
            Reset();
            Serve();
        }
    }
}
