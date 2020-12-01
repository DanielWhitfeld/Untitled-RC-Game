using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HighScores m_HighScores;

    //game timer and reference to game time
    private float m_gameTimer = 300f;
    public float GameTime { get { return m_gameTimer; } }

    //creates a new enum for gamestates
    public enum GameState
    {
        Start,
        Playing,
        Win,
        Lose
    };
    //game state instance and reference
    public GameState m_GameState;

    private void Awake()
    {
        m_GameState = GameState.Start;
    }

    private void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                Cursor.lockState = CursorLockMode.None;

                break;

            case GameState.Playing:
                Cursor.lockState = CursorLockMode.Locked;
                m_gameTimer -= Time.deltaTime;
                if (m_gameTimer <= 0)
                {
                    m_GameState = GameState.Lose;
                }
                else if(Input.GetKeyDown(KeyCode.Keypad1))//ADD DIFFERENT CODE FOR WINNING
                {
                    m_HighScores.AddScore(Mathf.RoundToInt(m_gameTimer));
                    m_HighScores.SaveScoresToFile();
                    m_GameState = GameState.Win;
                }
                break;

            case GameState.Win:
                Cursor.lockState = CursorLockMode.None;
                m_gameTimer = 300f;
                break;

            case GameState.Lose:
                Cursor.lockState = CursorLockMode.None;
                m_gameTimer = 300f;
                break;
        }
    }
}
