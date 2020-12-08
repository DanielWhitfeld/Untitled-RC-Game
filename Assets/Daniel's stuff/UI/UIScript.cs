using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //reference to the game manager and high scores
    public GameManager GM;
    public HighScores m_highScores;

    //Panels and their components for each menu/game state
    public GameObject m_playingPanel;
    public Text m_Timer;

    public GameObject m_titlePanel;

    public GameObject m_winLosePanel;
    public Text m_winLoseText;
    public Text m_titleRetryButtonText;

    public GameObject m_controlsPanel;
    public Text m_controlsRetryButtonText;

    public GameObject m_highScoresPanel;
    public Text m_highRetryButtonText;
    public Text m_highScoresText;

    private bool m_hasWon;

    public void Awake()
    {
        m_hasWon = true;
        m_playingPanel.SetActive(false);
        m_winLosePanel.SetActive(false);
        m_controlsPanel.SetActive(false);
        m_highScoresPanel.SetActive(false);
    }

    private void Update()
    {
        int seconds = Mathf.RoundToInt(GM.GameTime);
        m_Timer.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));
        //add reference to win/lose state of the game
        if (!(m_highScoresPanel.activeInHierarchy == true || m_controlsPanel.activeInHierarchy == true))
        {
            if (GM.m_GameState == GameManager.GameState.Win)
            {
                m_hasWon = true;
                OnWinOrLose();
            }
            else if (GM.m_GameState == GameManager.GameState.Lose)
            {
                m_hasWon = false;
                OnWinOrLose();
            }
        }
    }

    //Closes all of the menus and sets the UI to display the playing panel
    public void OnNewGame()
    {
        m_playingPanel.SetActive(true);

        m_titlePanel.SetActive(false);
        m_winLosePanel.SetActive(false);
        m_controlsPanel.SetActive(false);
        m_highScoresPanel.SetActive(false);

        //sets the gamestate to playing
        GM.m_GameState = GameManager.GameState.Playing;
    }

    //Closes all of the menus except for the controls screen
    public void OnControls()
    {
        if (m_hasWon == true)
        {
            m_controlsRetryButtonText.text = "New Game";
        }
        else if (m_hasWon == false)
        {
            m_controlsRetryButtonText.text = "Try Again";
        }
        m_controlsPanel.SetActive(true);

        m_playingPanel.SetActive(false);
        m_titlePanel.SetActive(false);
        m_winLosePanel.SetActive(false);
        m_highScoresPanel.SetActive(false);
    }

    //Closes all of the menus except for the high scores screen
    public void OnHighScores()
    {
        if (m_hasWon == true)
        {
            m_highRetryButtonText.text = "New Game";
        }
        else if (m_hasWon == false)
        {
            m_highRetryButtonText.text = "Try Again";
        }

        string text = "";
        for(int i = 0; i < m_highScores.scores.Length; i++)
        {
            int seconds = m_highScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        m_highScoresText.text = text;

        m_highScoresPanel.SetActive(true);

        m_controlsPanel.SetActive(false);
        m_playingPanel.SetActive(false);
        m_titlePanel.SetActive(false);
        m_winLosePanel.SetActive(false);
    }

    public void OnWinOrLose()
    {
        if(m_hasWon == true)
        {
            m_winLoseText.text = "YOU WON!";
            m_titleRetryButtonText.text = "New Game";
        }
        else if(m_hasWon == false)
        {
            m_winLoseText.text = "YOU LOST!";
            m_titleRetryButtonText.text = "Try Again";
        }

        m_winLosePanel.SetActive(true);

        m_titlePanel.SetActive(false);
        m_playingPanel.SetActive(false);
        m_controlsPanel.SetActive(false);
        m_highScoresPanel.SetActive(false);
    }
    
    //Closes the application
    public void OnQuit()
    {
        Application.Quit();
    }
}
