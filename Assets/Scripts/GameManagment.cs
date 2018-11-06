using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
This class is used to keep track of loading different scenes with in the game
and overall managing all game functions. This will be used to transition
between levels and keep the game flowing. ALso this is where all buttons in the
game will find their functionality.
*/

public class GameManagment : MonoBehaviour {

    public static GameManagment instance;
    //TextManager tm = new TextManager();
/*
This Method is used to return the user to the MainMenu
*/ 
	public void OnMenuButtonPress()
	{
        GameScore.playerScore = 0;
		Application.LoadLevel("MainMenu");
	}

/*
This Method is used to take the user to the Instrucion Scene
*/ 
	public void OnInstructionButtonPress()
	{
		Application.LoadLevel("Instruction");
	}

/*
This Method is used to take the user to the LeaderBoard
*/ 
	public void OnLeaderboardButtonPress()
	{
		Application.LoadLevel("Leaderboard");
	}

/*
This Method is used to take the user to the Main level where the user
can play the game
*/ 
	public void OnOfflineButton()
	{
		Application.LoadLevel("ARgame_NoAR");
	}

/*
This Method is used to take the user to the Augmented Reality level where the user
can play the game
*/ 
	public void OnButtonAR()
	{
		Application.LoadLevel("ARgame");
	}

/*
This Method is used to take the user to the scene that allows them
to select if they would like to play the AR version or the non AR
version. Also in the scene that this method takes you is a map of 
all waste stations on campus for the users convienience
*/ 
	public void OnStartGame()
	{
		Application.LoadLevel("map");
	}

    //Button functionality for the leaderboard submit button. When user hits the button, the score is sent to the leaderboard.
    //public void OnSubmitScore()
    //{
    //    tm.Submit();
    //}
}
