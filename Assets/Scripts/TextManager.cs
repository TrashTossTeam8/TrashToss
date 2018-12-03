using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    //Needed for the dreamlo database to work.
    const string privateCode = "1gLJkEC7GEGCTNDx6MfFMAKy93nTEZt0SHaMskRuRMAA";
    const string publicCode = "5adeae71d6024519e0f1b351";
    const string webURL = "http://dreamlo.com/lb/";

    //List of scores
    public Score[] scoreList;

    //References the DisplayHighscoresScript
    DisplayHighScores highscoresDisplay;

    //If gameOver is false, the user has not yet played a game and should not see the input field on the leaderboard
    bool gameOver = false;

    //Parts of the UI that allow user to input names on the leaderboard. These should not always be visible.
    public CanvasGroup inputUIElements;

    //Gets the display for the highscores
    private void Awake()
    {
        highscoresDisplay = GetComponent<DisplayHighScores>();
    }

    //Downloads the highscores
    void Start()
    {
        //Gets the scene that was loaded previously. This is so that when the leaderboard is loaded its known whether or not the player has just played a game
        //or is just looking at the leaderboard. This is important to be sure that people who haven't played the game can't add their name to it.
        int prevLevel = Indestructable.instance.prevScene;
        
        //Field to enter names
        initialsField = inputField.GetComponent<InputField>();

        //If the user accessed the leaderboard without finishing the game, don't show them the UI for adding a name to the leaderboard.
        if(prevLevel == 2)
        {
            HideInput();
        }
        else
        {
            UnHideInput();
        }
        DownloadHighScores();
    }

    //Lists the highscores
    public void Read()
    {
        string name = initialsField.text;
        int score = GameScore.playerScore;
        AddNewHighscore(name, score);
    }

    //Hide the input box for the leaderboard
    public void HideInput()
    {
        inputUIElements.alpha = 0f;
    }

    //Reveal the input box for the leaderboard
    public void UnHideInput()
    {
        inputUIElements.alpha = 1f;
    }

    //Adds a new highscore
    public void AddNewHighscore(string name, int score)
    {
        StartCoroutine(UploadScore(name, score));
        Score[] blankList = new Score[0];
        highscoresDisplay.OnHighScoresDownloaded(blankList);

        //Slight pause to give the database server time to submit the score before updating the leaderboard
        Thread.Sleep(100);

        DownloadHighScores();
    }

    //Uploads a highscore
    IEnumerator UploadScore(string name, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(name) + "/" + score);
        yield return www;
    }
    
    //Initates the downloading of highscores
    public void DownloadHighScores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    //Downloads all highscores from the web service
    IEnumerator DownloadHighscoresFromDatabase()
    {

        //Uses a public code and URL for the webservice to download the scores
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

    //Formats the highscores and places them in the listbox on the leaderboard page
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
        }

    }

}

//Score object
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
