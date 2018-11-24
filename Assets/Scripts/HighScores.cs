using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class manages the high score table. We used a website/service called dreamlo.com that holds the leaderboard
//for us. 
public class HighScores : MonoBehaviour {

    //Our codes to access our leaderboard
    const string privateCode = "1gLJkEC7GEGCTNDx6MfFMAKy93nTEZt0SHaMskRuRMAA";
    const string publicCode = "5adeae71d6024519e0f1b351";
    const string webURL = "http://dreamlo.com/lb/";

    //Regular Expression that can be used to check if a string is alphanumeric only
    System.Text.RegularExpressions.Regex alphanumeric = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");

    //References the Highscores script
    static HighScores hs;

    //Add a new highscore to the leaderboard
    public void AddNewHighscore(string name, int score)
    {
        //Take the name and convert it to lowercase
        string nameInput = name.ToLower();

        //Make sure the user's "name" doesn't happen to be SQL code in disguise. This is a security measure to prevent sql injections on the leaderboard.
        //DROP, SELECT, INSERT and '*' are all SQL commands
        //If the name passes, the UploadScore subroutine is called.
        if(!(nameInput.Contains("drop")|| nameInput.Contains("select")||nameInput.Contains("insert") || nameInput.Contains("*")) && alphanumeric.IsMatch(name))
        {
            StartCoroutine(UploadScore(name, score));
        }
        
    }

    //Uses the url and private codes to communicate with the web service.
    IEnumerator UploadScore(string name, int score)
    {
        //Webrequest object
        WWW www = null;
        try
        {
            //Upload score to dreamlo leaderboard
            www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(name) + "/" + score);
        }
        catch(System.Exception e)
        {
            Debug.Log("Upload Failed");
        }
        
        yield return www;
    }
}
