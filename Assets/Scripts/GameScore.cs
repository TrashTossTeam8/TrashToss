using UnityEngine;
using UnityEngine.UI;

/*
The purpose of this class is to manage the score of the player
in game. The class sets up a player score and offers methods to 
be called by other scripts to manage the score. However do to
the cope of the game the only method that is really needed to 
be called by other scripts or objects is a method to increase 
the score.
*/

public class GameScore : MonoBehaviour {


	public Text score;	// The actual text object that is the game score

	// A variable used to hold the score value which is converted to a string
	// in order to display the actual player score. This variable is public
	// and static in order to allow other objects or sciprts to access it
	// and change the player score
	public static int playerScore = 0;	

/*
This method is used to display the score to the player.
The Score text vaiable is set to the int variable we are using
to keep track of score. We need the ToString so that the text 
variable type can display the value with out error of type mismatch.
*/
	public void Update()
	{
		score.text = playerScore.ToString ();
	}
}
