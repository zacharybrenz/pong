using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script that turns the screen boundaries into
// world coordinates. Pulled from another project.
public class ScreenBounds : MonoBehaviour
{
    public static ScreenBounds Instance = null;

    [SerializeField]
    [Range(0.0f, 0.5f)]
    private float _xMin = 0.0f;

    [SerializeField]
    [Range(0.5f, 1.0f)]
    private float _xMax = 1.0f;

    [SerializeField]
    [Range(0.0f, 0.5f)]
    private float _yMin = 0.0f;

    [SerializeField]
    [Range(0.5f, 1.0f)]
    private float _yMax = 1.0f;

    private Rect _playArea = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        CalculateBounds();
    }

    public void CalculateBounds()
    {
        Vector3 min = Camera.main.ScreenToWorldPoint(new Vector3(_xMin * Screen.width, _yMin * Screen.height, 0.0f));
        Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(_xMax * Screen.width, _yMax * Screen.height, 0.0f));
        _playArea = new Rect(min.x, min.y, max.x, max.y);
    }

    public Vector3 Clamp(Vector3 pos, Collider2D collider = null)
    {
        Rect bounds = _playArea;

        if (collider != null)
        {
            bounds.x += collider.bounds.size.x / 2.0f;
            bounds.width -= collider.bounds.size.x / 2.0f;
            bounds.y += collider.bounds.size.y / 2.0f;
            bounds.height -= collider.bounds.size.y / 2.0f;
        }

        float xPos = Mathf.Clamp(pos.x, bounds.x, bounds.width);
        float yPos = Mathf.Clamp(pos.y, bounds.y, bounds.height);

        return new Vector3(xPos, yPos, pos.z);
    }

    public Vector3 RandomPoint(Collider2D collider = null)
    {
        Rect bounds = _playArea;

        if (collider != null)
        {
            bounds.x += collider.bounds.size.x / 2.0f;
            bounds.width -= collider.bounds.size.x / 2.0f;
            bounds.y += collider.bounds.size.y / 2.0f;
            bounds.height -= collider.bounds.size.y / 2.0f;
        }

        float x = Random.Range(bounds.x, bounds.width);
        float y = Random.Range(bounds.y, bounds.height);

        return new Vector3(x, y, 0.0f);
    }

    public Vector2 Min
    {
        get { return _playArea.position; }
        set { }
    }

    public Vector2 Max
    {
        get { return _playArea.size; }
        set { }
    }
}
