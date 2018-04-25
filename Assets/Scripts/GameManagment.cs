using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class is used to keep track of loading different scenes with in the game
and overall managing all game functions. This will be used to transition
between levels and keep the game flowing. ALso this is where all buttons in the
game will find their functionality.
*/ 

public class GameManagment : MonoBehaviour {

/*
This Method is used to return the user to the MainMenu
*/ 
	public void OnMenuButtonPress()
	{
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
		Application.LoadLevel("Main");
	}

    //For entering the name on the leaderboard
    public void OnTextBoxEntry()
    {

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
}
