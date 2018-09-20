using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/*
This class is used to spawn a random trash object in the game so
that the player can attempt to sort it. This method randomizes the 
type of trash spawned. This way the player does not always start
the game sorting trash that is recyclable. 
*/

public class ARSpawnScript : MonoBehaviour
{
    public GameObject logo;

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

    public GameObject[] rTrashArray; // array of available recyclables

    // This int variable is used to insure that the type of trash 
    // spawned is random.
    private int randomizer;

    public Vector3 spawnPosition = new Vector3(0f, -0.45f, 0.75f);

    /*
    This method is called when the game starts and is used to spawn a piece of
    trash for the player to sort at the begining of the game.
    */
    public void Start()
    {
        SpawnTrash();
    }

    /*
    This method is used to randomally spawn a piece of trash so that the trash will
    not always have the same type (recyle, compost, landfill). Using an int that we
    randomize to a value between 1 and 3 which then determines through a switch case
    which type of trash will spawn.
    */
    public GameObject SpawnTrash()
    {
        
        // Randomizing the int variable to a whole integer
        // between the values of 1 and 3 thus determining trash
        // type
        randomizer = (int)Random.Range(1f, 4f);

        // Write to concel the random variable value in order
        // to tell if our code is working correclty and spawning
        // the right type of trash
        Debug.Log(randomizer);

        GameObject spawnedObject;

        // A switch statement that takes in our ramdom variable and uses that
        // to determine which type of trash to spawn (1 is recycle, 2 is compost,
        // and 3 is land fill). The Instantiate function is called to spawn the trash
        // and is able to take in a vector which acts as coordinates and spawns the 
        // object at that location. A rotation type is also passed in due to neccesity,
        // Unity needs that information to call instantiate at a specific location.
        switch (randomizer)
        {
            case 1:
                
			    spawnedObject = Instantiate(rTrash,new Vector3(0f,-35f,200.4f), transform.rotation);
                rTrash.transform.parent = logo.transform;
                Debug.Log("IT SURE DOES GO HERE1");
                break;
            case 2:

                spawnedObject = Instantiate(cTrash,new Vector3(0f,-35f,200.4f), transform.rotation);
                Debug.Log("IT SURE DOES GO HERE2");
                cTrash.transform.parent = logo.transform;
                break;
            case 3:

                spawnedObject = Instantiate(lTrash, Vector3.zero, transform.rotation); //new Vector3(0f,-35f,200.4f)
                Debug.Log("IT SURE DOES GO HERE3");
                //lTrash.transform.parent = logo.transform;
                spawnedObject.transform.SetParent(Camera.main.transform);
                spawnedObject.transform.localPosition = spawnPosition;

                break;
            // Default is used incase thier is an unforseen error computing the random
            // variable.
            default:
                spawnedObject = Instantiate(rTrash,new Vector3(0f,-35f,200.4f), transform.rotation);
                break;

        }

        // Turn off Physics of the Object.
        Rigidbody spawnedObjectRigidbody = spawnedObject.GetComponent<Rigidbody>();
        spawnedObjectRigidbody.isKinematic = true; // Freeze Object Physics


        //ToDo: Throw Object after input.

        return spawnedObject;
    }
}
