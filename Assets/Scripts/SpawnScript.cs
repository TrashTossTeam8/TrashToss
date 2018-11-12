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
	public GameObject waterBottle;
    public GameObject cheeseBurger;
    public GameObject pizzaBox;
    public GameObject chipBag;
    public GameObject book;
    public GameObject toiletPaper;
    public GameObject eraser;
    public GameObject hat;
    public GameObject pencil;
    public GameObject paper;
    public GameObject glassBottle;
    public GameObject sodaCan;
    public GameObject waterMelon;
    public GameObject tree;
    public GameObject pineApple;
    public GameObject tomato;
    public GameObject rubberDuck;
    public GameObject pen;

	// This int variable is used to insure that the type of trash 
	// spawned is random.
	private int randomizer;

/*
This method is called when the game starts and is used to spawn a piece of
trash for the player to sort at the begining of the game.
*/ 
	public void Start ()
	{
		Spawner();
	}
    

    /*
    This method is used to randomly spawn a piece of trash so that the trash will
    not always have the same type (recyle, compost, landfill). Using an int that we
    randomize to a value between 1 and 3 which then determines through a switch case
    which type of trash will spawn.
    */
    void Spawner()
	{
		randomizer = (int)Random.Range (1f, 2f);
        

		// A switch statement that takes in our ramdom variable and uses that
		// to determine which type of trash to spawn (1 is recycle, 2 is compost,
		// and 3 is land fill). The Instantiate function is called to spawn the trash
		// and is able to take in a vector which acts as coordinates and spawns the 
		// object at that location. A rotation type is also passed in due to neccesity,
		// Unity needs that information to call instantiate at a specific location.
		switch (randomizer)
		{
		case 1:
			Instantiate (waterBottle,new Vector3(-.08f, 12.52f,-11.19f), transform.rotation);
			break;
		case 2:
			Instantiate (pizzaBox,new Vector3(0.75f, 12.52f, -11.19f), transform.rotation);
			break;
		case 3:
			Instantiate (chipBag, new Vector3 (-.08f, 12.52f, -11.19f), transform.rotation);
			break;
        case 4:
            Instantiate(cheeseBurger, new Vector3(-.08f, 12.52f, -11.19f), transform.rotation);
            break;
        case 5:
            Instantiate(book, new Vector3(-.08f, 12.52f, -11.19f), transform.rotation);
            break;
        case 6:
            Instantiate(toiletPaper, new Vector3(-.08f, 12.52f, -11.19f), transform.rotation);
            break;
        case 7:
            Instantiate(eraser, new Vector3(-2.5f, 7.52f, -11.19f), transform.rotation);
            break;
        case 8:
            Instantiate(hat, new Vector3(-7f, 11.52f, -17.19f), transform.rotation);
            break;
        case 9:
            Instantiate(paper, new Vector3(-.08f, 12.52f, -11.19f), transform.rotation);
            break;
        case 10:
            Instantiate(glassBottle, new Vector3(-.08f, 13.52f, -11.19f), transform.rotation);
            break;
        case 11:
            Instantiate(sodaCan, new Vector3(-.08f, 12.52f, -11.19f), transform.rotation);
            break;
        case 12:
            Instantiate(waterMelon, new Vector3(6f, 12.52f, -11.19f), transform.rotation);
            break;
        case 13:
            Instantiate(tree, new Vector3(-0.25f, 12.52f, -11.19f), transform.rotation);
            break;
        case 14:
            Instantiate(pineApple, new Vector3(-7.5f, 16f, -11.19f), transform.rotation);
            break;
        case 15:
            Instantiate(tomato, new Vector3(-.08f, 20f, 1f), transform.rotation);
            break;
        case 16:
            Instantiate(rubberDuck, new Vector3(-.08f, 10f, 1f), transform.rotation);
            break;
        case 17:
            Instantiate(pen, new Vector3(-.08f, 10f, 1f), transform.rotation);
            break;
                
            // Default is used incase there is an unforseen error computing the random
            // variable.
            default:
		    Instantiate (waterBottle,new Vector3(-.08f, 18.52f, -11.19f), transform.rotation);
		    break;

		}
	}
}
