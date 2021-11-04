using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Handles scorekeeping and switching between screens
public class PongManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _p1ScoreText = null;
    private int _p1Score = 0;

    [SerializeField]
    private TextMeshProUGUI _p2ScoreText = null;
    private int _p2Score = 0;

    [SerializeField]
    private Ball _ball = null;

    [SerializeField]
    private GameObject _titleScreen = null;

    [SerializeField]
    private GameObject _gameBoard = null;

    [SerializeField]
    private GameObject _gameOverScreen = null;

    [SerializeField]
    private TextMeshProUGUI _gameOverText = null;

    private bool _gameOver = false;
    private float _gameOverTimer = 0.0f;
    private const float GAME_OVER_DELAY = 2.0f;

    private float _quitTimer = 0.0f;
    private const float QUIT_DELAY = 3.0f;

    // Display the title screen first
    private void Awake()
    {
        _titleScreen.SetActive(true);
        _gameOverScreen.SetActive(false);
        _gameBoard.SetActive(false);
    }

    // Begin the game
    public void StartGame()
    {
        // Show the game board
        _titleScreen.SetActive(false);
        _gameOverScreen.SetActive(false);
        _gameBoard.SetActive(true);

        // Reset the scores
        _p1Score = 0;
        _p2Score = 0;
        _p1ScoreText.text = "" + _p1Score;
        _p2ScoreText.text = "" + _p2Score;

        // Reset game over values
        _gameOver = false;
        _gameOverTimer = 0.0f;

        // Serve the first ball
        _ball.gameObject.SetActive(true);
        _ball.Reset();
        _ball.Serve();
    }

    private void Update()
    {
        // Show the game over screen after a certain delay has passed
        if (_gameOver)
        {
            _gameOverTimer -= Time.deltaTime;
            if (_gameOverTimer <= 0.0f)
            {
                _gameOverScreen.SetActive(true);
                _gameBoard.SetActive(false);
                _gameOver = false;
            }
        }

        // Quit the game if escape is held down
        if (Input.GetKey(KeyCode.Escape))
        {
            _quitTimer += Time.deltaTime;
            if (_quitTimer >= QUIT_DELAY)
            {
                Application.Quit();
            }
        }
    }

    // Score a point, then check if the game has finished
    public void Score(int player)
    {
        // There are only two players,
        // clamp the value just in case
        player = Mathf.Clamp(player, 1, 2);

        // Add a point to player 1's total
        if (player == 1)
        {
            _p1Score++;
            _p1ScoreText.text = "" + _p1Score;
        }

        // Add a point to player 2's total
        else
        {
            _p2Score++;
            _p2ScoreText.text = "" + _p2Score;
        }

        // If player 1 wins, set the text appropriately
        // and prepare game over screen
        if (_p1Score >= 5)
        {
            _gameOverText.text = "PLAYER 1 WINS";
            _gameOverText.color = _p1ScoreText.color;
            _gameOverTimer = GAME_OVER_DELAY;
            _gameOver = true;
            _ball.gameObject.SetActive(false);  
            _ball.Reset();
        }

        // If player 2 wins, do the same
        else if (_p2Score >= 5)
        {
            _gameOverText.text = "PLAYER 2 WINS";
            _gameOverText.color = _p2ScoreText.color;
            _gameOverTimer = GAME_OVER_DELAY;
            _gameOver = true;
            _ball.gameObject.SetActive(false);  
            _ball.Reset();
        }

        // Otherwise, serve the next ball
        else
        {
            _ball.Reset();
            _ball.Serve();
        }
    }
}
