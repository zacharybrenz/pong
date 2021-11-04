using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scrolls the position of a RawImage's UV Rect.
public class ScrollingRawImage : MonoBehaviour
{
    [SerializeField]
    private Vector2 _speed = new Vector2(0.0f, 1.0f);

    private RawImage _rawImage = null;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
    }

    // Scroll the position of the texture based on the provided speed.
    private void Update()
    {
        Rect rect = _rawImage.uvRect;
        rect.position += _speed * Time.deltaTime;
        _rawImage.uvRect = rect;
    }
}
