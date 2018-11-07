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

    //References the Highscores script
    static HighScores hs;

    //Add a new highscore to the leaderboard
    public void AddNewHighscore(string name, int score)
    {
        StartCoroutine(UploadScore(name, score));
    }

    //Uses the url and private codes to communicate with the web service.
    IEnumerator UploadScore(string name, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(name) + "/" + score);
        yield return www;
    }
}
