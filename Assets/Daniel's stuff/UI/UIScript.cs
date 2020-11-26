using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //Panels for each menu/game state
    public GameObject m_titlePanel;
    public GameObject m_playingPanel;
    public GameObject m_winLosePanel;
    public GameObject m_controlsPanel;
    public GameObject m_highScoresPanel;

    private void Awake()
    {
        m_playingPanel.SetActive(false);
        m_winLosePanel.SetActive(false);
        m_controlsPanel.SetActive(false);
        m_highScoresPanel.SetActive(false);
    }

    //Closes all of the menus and sets the UI to display the playing panel
    public void OnNewGame()
    {
        m_playingPanel.SetActive(true);

        m_titlePanel.SetActive(false);
        m_winLosePanel.SetActive(false);
        m_controlsPanel.SetActive(false);
        m_highScoresPanel.SetActive(false);
    }

    //Closes all of the menus except for the controls screen
    public void OnControls()
    {
        m_controlsPanel.SetActive(true);

        m_playingPanel.SetActive(false);
        m_titlePanel.SetActive(false);
        m_winLosePanel.SetActive(false);
        m_highScoresPanel.SetActive(false);
    }

    //Closes all of the menus except for the high scores screen
    public void OnHighScores()
    {
        m_highScoresPanel.SetActive(true);

        m_controlsPanel.SetActive(false);
        m_playingPanel.SetActive(false);
        m_titlePanel.SetActive(false);
        m_winLosePanel.SetActive(false);
    }

    //Closes the application
    public void OnQuit()
    {
        Application.Quit();
    }
}
