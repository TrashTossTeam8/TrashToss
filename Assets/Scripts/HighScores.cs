using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour {

    const string privateCode = "1gLJkEC7GEGCTNDx6MfFMAKy93nTEZt0SHaMskRuRMAA";
    const string publicCode = "5adeae71d6024519e0f1b351";
    const string webURL = "http://dreamlo.com/lb/";

    static HighScores hs;

    public void AddNewHighscore(string name, int score)
    {
        StartCoroutine(UploadScore(name, score));
    }

    IEnumerator UploadScore(string name, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(name) + "/" + score);
        yield return www;
    }
}
