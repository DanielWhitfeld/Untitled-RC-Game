using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScores : MonoBehaviour
{
    //creates a list for all of the scores
    public int[] scores = new int[10];

    public string m_scoreFileName = "highscores.txt";

    private string m_currentDirectory;

    private void Start()
    {
        m_currentDirectory = Application.dataPath;
        Debug.Log("Our current directory is: " + m_currentDirectory);

        LoadScoresFromFile();
    }

    private void Update()
    {
        
    }

    public void LoadScoresFromFile()
    {
        //Checks to see if file exists
        bool fileExists = File.Exists(m_currentDirectory + "\\" + m_scoreFileName);
        if(fileExists == true)
        {
            Debug.Log("Found high score file " + m_scoreFileName);
        }
        else
        {
            //if the file does not exist the function will be aborted
            Debug.Log("The file " + m_scoreFileName + " does not exist. No scores will be loaded.", this);
            return;
        }

        //clears high scores list of older values
        scores = new int[scores.Length];

        //creates a new instance of the stream reader
        StreamReader fileReader = new StreamReader(m_currentDirectory + "\\" + m_scoreFileName);

        int scoreCount = 0;
        while(fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();

            int readScore = -1;
            bool didParse = int.TryParse(fileLine, out readScore);
            if(didParse)
            {
                scores[scoreCount] = readScore;
            }
            else
            {
                //if the value is invalid display an error message and use a default value of 0
                Debug.Log("Invalid line scores file at " + scoreCount + ", using default value.", this);
                scores[scoreCount] = 0;
            }
            //increment the counter
            scoreCount++;
        }
        //Close the stream
        fileReader.Close();
        Debug.Log("High socres read from " + m_scoreFileName);
    }

    public void SaveScoresToFile()
    {
        //creates a new instance of the stream writer
        StreamWriter fileWriter = new StreamWriter(m_currentDirectory + "\\" + m_scoreFileName);

        //writes the lines to the file
        for(int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }

        //Close the stream
        fileWriter.Close();
        Debug.Log("High scores written to " + m_scoreFileName);
    }

    public void AddScore(int newScore)
    {
        int desiredIndex = -1;
        for(int i = 0; i < scores.Length; i++)
        {
            if(scores[i] < newScore || scores[i] == 300)
            {
                //gives desired index the correct position for the score and breaks from the loop
                desiredIndex = i;
                break;
            }
        }

        //if the score is not high enough it will not be added to the list and the function will be aborted
        if(desiredIndex < 0)
        {
            Debug.Log("Score of " + newScore + " not high enough for high scores list", this);
            return;
        }

        for(int i = scores.Length - 1; i > desiredIndex; i--)
        {
            scores[i] = scores[i - 1];
        }

        //insert new score into its place
        scores[desiredIndex] = newScore;
        Debug.Log("Score of " + newScore + " entered into high scores at position " + desiredIndex, this);
    }
}
