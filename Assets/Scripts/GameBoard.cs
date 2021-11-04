using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Positions the walls and paddles based on the screen size.
public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private Walls _walls = new Walls();

    [SerializeField]
    private Paddle _leftPaddle = null;

    [SerializeField]
    private Paddle _rightPaddle = null;

    private void Start()
    {
        Vector2 max = ScreenBounds.Instance.Max;
        Vector2 min = ScreenBounds.Instance.Min;
        Vector2 topLeft = new Vector2(min.x, max.y);
        Vector2 topRight = new Vector2(max.x, max.y);
        Vector2 bottomLeft = new Vector2(min.x, min.y);
        Vector2 bottomRight = new Vector2(max.x, min.y);

        Vector2 pos = Vector2.zero;

        // Top Wall
        pos = (topLeft + topRight) / 2.0f;
        pos.y += 0.5f;
        _walls.Top.transform.position = pos;
        _walls.Top.size = new Vector2(Mathf.Abs(topRight.x - topLeft.x), 1);

        // Bottom Wall
        pos = (bottomLeft + bottomRight) / 2.0f;
        pos.y -= 0.5f;
        _walls.Bottom.transform.position = pos;
        _walls.Bottom.size = new Vector2(Mathf.Abs(bottomRight.x - bottomLeft.x), 1);

        // Left Wall
        pos = (topLeft + bottomLeft) / 2.0f;
        pos.x -= 50.0f;
        _walls.Left.transform.position = pos;
        _walls.Left.size = new Vector2(100, Mathf.Abs(topLeft.y - bottomLeft.y));

        // Right Wall
        pos = (topRight + bottomRight) / 2.0f;
        pos.x += 50.0f;
        _walls.Right.transform.position = pos;
        _walls.Right.size = new Vector2(100, Mathf.Abs(topRight.y - bottomRight.y));

        // Left Paddle
        pos = (topLeft + bottomLeft) / 2.0f;
        pos.x += 1.0f;
        _leftPaddle.transform.position = pos;

        // Left Paddle
        pos = (topRight + bottomRight) / 2.0f;
        pos.x -= 1.0f;
        _rightPaddle.transform.position = pos;
    }

    [System.Serializable]
    private class Walls
    {
        [SerializeField]
        public BoxCollider2D Top = null;

        [SerializeField]
        public BoxCollider2D Bottom = null;

        [SerializeField]
        public BoxCollider2D Left = null;

        [SerializeField]
        public BoxCollider2D Right = null;
    }
}
