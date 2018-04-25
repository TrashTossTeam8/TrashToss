using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used to submit the user's score to the dreamlo database
public class SubmitScore : MonoBehaviour {

    //Script that contains the user's score
    public GameScore gameScore;

    //Script that communicates with the dreamlo database
    public HighScores highScores;

    public int score = GameScore.playerScore;

    public string name = "Austin";

    private void Start()
    {
        submit();
    }

    public void submit()
    {
        Debug.Log("DING");
        highScores.AddNewHighscore(name, score);
    }
	
}
