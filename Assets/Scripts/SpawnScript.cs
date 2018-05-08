using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is used to spawn a random trash object in the game so
that the player can attempt to sort it. This method randomizes the 
type of trash spawned. This way the player does not always start
the game sorting trash that is recyclable. 
*/

public class SpawnScript : MonoBehaviour {

	// This is a gameObject place holder for the recycable trash type.
	// the trash object is directly referenced in the Unity engine 
	// through drag and drop
	public GameObject rTrash;

	// This is a gameObject place holder for the Compost trash type.
	// the trash object is directly referenced in the Unity engine 
	// through drag and drop
	public GameObject cTrash;

	// This is a gameObject place holder for the Land Fill trash type.
	// the trash object is directly referenced in the Unity engine 
	// through drag and drop
	public GameObject lTrash;

	// This int variable is used to insure that the type of trash 
	// spawned is random.
	private int randomizer;

/*
This method is called when the game starts and is used to spawn a piece of
trash for the player to sort at the begining of the game.
*/ 
	public void Start ()
	{
		Spawner ();	
	}

/*
This method is used to randomally spawn a piece of trash so that the trash will
not always have the same type (recyle, compost, landfill). Using an int that we
randomize to a value between 1 and 3 which then determines through a switch case
which type of trash will spawn.
*/ 
	void Spawner()
	{
        
		// Randomizing the int variable to a whole integer
		// between the values of 1 and 3 thus determining trash
		// type
		randomizer = (int)Random.Range (1f, 4f);

		// Write to concel the random variable value in order
		// to tell if our code is working correclty and spawning
		// the right type of trash
		Debug.Log (randomizer);

		// A switch statement that takes in our ramdom variable and uses that
		// to determine which type of trash to spawn (1 is recycle, 2 is compost,
		// and 3 is land fill). The Instantiate function is called to spawn the trash
		// and is able to take in a vector which acts as coordinates and spawns the 
		// object at that location. A rotation type is also passed in due to neccesity,
		// Unity needs that information to call instantiate at a specific location.
		switch (randomizer)
		{
		case 1:
			Instantiate (rTrash,new Vector3(-.08f, 12.52f,-11.19f), transform.rotation);
			break;
		case 2:
			Instantiate (cTrash,new Vector3(-.08f, 12.52f, -11.19f), transform.rotation);
			break;
		case 3:
			Instantiate (lTrash, new Vector3 (-.08f, 12.52f, -11.19f), transform.rotation);
			break;
		// Default is used incase thier is an unforseen error computing the random
		// variable.
		default:
			Instantiate (rTrash,new Vector3(-.08f, 12.52f, -11.19f), transform.rotation);
			break;

		}
	}
}
