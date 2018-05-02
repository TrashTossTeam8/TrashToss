using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TextManager works with the Dreamlo database to upload and download the highscores list

public class TextManager : MonoBehaviour
{

    //The textbox where the user inputs their name
    public GameObject inputField;
    InputField initialsField;

    //References the GameScore script
    GameScore highScore;

    //Needed for the dreamlo database to work
    const string privateCode = "1gLJkEC7GEGCTNDx6MfFMAKy93nTEZt0SHaMskRuRMAA";
    const string publicCode = "5adeae71d6024519e0f1b351";
    const string webURL = "http://dreamlo.com/lb/";

    //List of scores
    public Score[] scoreList;

    //References the DisplayHighscoresScript
    DisplayHighScores highscoresDisplay;

    private void Awake()
    {
        highscoresDisplay = GetComponent<DisplayHighScores>();
    }

    void Start()
    {
        initialsField = inputField.GetComponent<InputField>();
        DownloadHighScores();
    }

    public void Read()
    {
        string name = initialsField.text;
        int score = GameScore.playerScore;
        AddNewHighscore(name, score);
    }


    public void AddNewHighscore(string name, int score)
    {
        StartCoroutine(UploadScore(name, score));
    }

    IEnumerator UploadScore(string name, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(name) + "/" + score);
        yield return www;
    }

    public void DownloadHighScores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScore(www.text);
            highscoresDisplay.OnHighScoresDownloaded(scoreList);
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }

    void FormatHighScore(string input)
    {
        string[] board = input.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        scoreList = new Score[board.Length];

        for(int x = 0; x < board.Length; x++)
        {
            string[] info = board[x].Split(new char[] {'|'});
            string name = info[0];
            int score = int.Parse(info[1]);
            scoreList[x] = new Score(name, score);
            //Debug.Log(scoreList[x].username + ": " + scoreList[x].score);
        }

    }

}

public struct Score
{
    public string username;
    public int score;

    public Score(string x, int y)
    {
        username = x;
        score = y;
    }
}
