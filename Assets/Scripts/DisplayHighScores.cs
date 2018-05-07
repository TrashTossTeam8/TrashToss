using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour {

    public Text highscoreText;
    TextManager highscoreManager;
    
    
	void Start () {
        
        highscoreManager = GetComponent<TextManager>();
	}
    

    public void OnHighScoresDownloaded(Score[] highscoreList)
    {
        if (highscoreList.Length == 0)
        {
            highscoreText.text = "";
        }

        else
        {


            for (int x = 0; x < highscoreList.Length; x++)
            {

                Debug.Log(highscoreList[x].username + highscoreList[x].score);
                highscoreText.text = highscoreText.text + (x + 1) + ". " + highscoreList[x].username + ": " + highscoreList[x].score + "\n";
                //Debug.Log(highscoreText.text);
            }
        }
        
    }

	
}
