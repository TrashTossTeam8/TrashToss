using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour {

    //The text for the high score list
    public Text highscoreText;
    TextManager highscoreManager;
    
    
	void Start () {
        
        highscoreManager = GetComponent<TextManager>();
	}
    
    //After highscores are downloaded from the server this is called
    public void OnHighScoresDownloaded(Score[] highscoreList)
    {
        //Default case of empty
        if (highscoreList.Length == 0)
        {
            highscoreText.text = "";
        }

        else
        {
            for (int x = 0; x < highscoreList.Length; x++)
            {
                //Lists each name on its own column on the leaderboard
                Debug.Log(highscoreList[x].username + highscoreList[x].score);
                highscoreText.text = highscoreText.text + (x + 1) + ". " + highscoreList[x].username.Replace('+',' ') + ": " + highscoreList[x].score + "\n";
            }
        }
        
    }

	
}
