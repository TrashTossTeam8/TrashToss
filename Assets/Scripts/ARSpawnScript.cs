using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

//This script handles the spawning and throwing of all waste objects within the game.

/*
This class is used to spawn a random trash object in the game so
that the player can attempt to sort it. This method randomizes the 
type of trash spawned. This way the player does not always start
the game sorting trash that is recyclable. 
*/

public class ARSpawnScript : MonoBehaviour
{

    //This is the object that represents the image target that all game assets are relative to. The 
    //three waste bins appear under this object when the game is played.
    public GameObject vuforiaTargetObject;

    //The default time between when the user throws an object and when a new object is spawned.
    public float spawnWaitTime = 2f;

    // This is a gameObject place holder for the recycable trash type.
    // the trash object is directly referenced in the Unity engine 
    // through drag and drop

    //In this project, there are three types of waste: recyclables, compost, and landfill items.
    //These arrays hold the objects of each type.
    [Header("TrashObjects")]
    public GameObject[] recyclableObjects;
    public GameObject[] compostObjects;
    public GameObject[] landfillObjects;

    //This boolean puts the game into a test mode which allows gives the developer unlimited time and a chosen object
    public bool debugUseTester;
    //This represents a waste object that is being tested on. This same object will be spawned repeatedly 
    //if the bool 'debugUserTester' is True.
    public GameObject testTrash;

    //These are the default values for the forces applied to the thrown objects.
    //xMult controls the X value sensitivity controlling left and right movement
    //YMult controls the Y value sensitivity controlling the height of the throw
    //Zmult controls the power of the throw and how far the object travels when thrown
    [Header("Throw Variables")]
    public float xMult = 1f;
    public float yMult = 1f;
    public float zMult = 1f;

    //Holds the current object being held
    public GameObject currentObject { get; private set; }

    /// <summary>
    /// Holds the time from when the users presses down on the trash object
    /// </summary>
    private float startTime;
    /// <summary>
    /// Holds the time from when the user lifts their finger from the trash object
    /// </summary>
    private float endTime;

    /// <summary>
    /// Holds the screen coordinates of the user's finger at the start of the throw
    /// </summary>
    public Vector2 startPos;
    /// <summary>
    /// Holds the screen coordinates of the user's finger once they left their finger from the trash object
    /// </summary>
    public Vector2 endPos;

    //The timer that controls whether there's still time left in the game
    public Timer clock;

    public GameObject spawnedObject;

    public float XaxisForce;
    public float YaxisForce;
    public float ZaxisForce;

    //Determines whether or not the user is currently touching the object
    private bool isHoldingToThrow = false;
    public bool isThrowing = false;

    /*
    This method is called when the game starts and is used to spawn a piece of
    trash for the player to sort at the begining of the game.
    */
    public void Start()
    {
        //Initiates the timer
        if (clock == null)
        {
            clock = FindObjectOfType<Timer>();
        }

        //Spawn the waste object
        currentObject = SpawnTrash();
        spawnedObject = currentObject;

        Debug.Log("NAME: " + currentObject.name);

    }

    void Update()
    {
        //Debug.Log("BLOCK CALLED");
        // We detect if the trash object is thrown to not start the rotation too early
        //spawnedObject.transform.Rotate(5f, YaxisForce, ZaxisForce);
        if (isThrowing == true && spawnedObject != null)
        {
            // We do the rotation based on the value of Y and Z axes
            //spawnedObject.transform.Rotate(5f, YaxisForce, ZaxisForce);
        }
    }

    /*
    This method is called when the user clicks down on their mouse on the object
    that this script is attatched to. All it does is keep track of the position of the user's finger
    and the time of when they first pressed the screen.
    */
    public void OnMouseDownForThrow()
    {
        Debug.Log("MOUSE DOWN");
        if (currentObject != null)
        {
            Debug.Log("TouchDown");
            isHoldingToThrow = true;
            startTime = Time.realtimeSinceStartup;
            startPos = Input.mousePosition;
        }

    }

    /*
    This method is called when the user lifts their finger from the screen. This method
    will make note of position of the user's finger when they lifted it up from the screen and the time
    when they did so. The throwBall method is then called to handle the arc of the throw.
    */
    public void OnMouseUpForThrow()
    {
        Debug.Log("MOUSE UP");
        if (currentObject != null && isHoldingToThrow == true)
        {
            Debug.Log("TouchUp");
            endTime = Time.realtimeSinceStartup;
            endPos = Input.mousePosition;
            throwBall();
        }

        isHoldingToThrow = false;
    }

    /*
    This method is used to randomally spawn a piece of trash so that the trash will
    not always have the same type (recyle, compost, landfill). Using an int that we
    randomize to a value between 1 and 3 which then determines through a switch case
    which type of trash will spawn.
    */
    public GameObject SpawnTrash()
    {

        // This int variable is used to insure that the type of trash 
        // spawned is random.
        // Randomizing the int variable to a whole integer
        // between the values of 1 and 3 thus determining trash
        // type
        int randomizer = Random.Range(0, 3);

        isThrowing = false;



        // Write to concel the random variable value in order
        // to tell if our code is working correclty and spawning
        // the right type of trash
        Debug.Log("NUMBER GENERATED: " + randomizer);


        // A switch statement that takes in our ramdom variable and uses that
        // to determine which type of trash to spawn (0 is recycle, 1 is compost,
        // and 2 is land fill). The Instantiate function is called to spawn the trash
        // and is able to take in a vector which acts as coordinates and spawns the 
        // object at that location. A rotation type is also passed in due to neccesity,
        // Unity needs that information to call instantiate at a specific location.
        //GameObject spawnedObject;

        //Tests to see if test mode is on
        if (debugUseTester && testTrash != null)
        {
            randomizer = -1;
        }
        switch (randomizer)
        {
            default:
            case 0:
                // Spawn Recycling randomly from recycleObjects array
                spawnedObject = Instantiate(recyclableObjects[Random.Range(0, recyclableObjects.Length - 1)], new Vector3(0, 0, 1), transform.rotation);
                //Marks the object as a recyclable
                spawnedObject.tag = "Recycle T";
                break;
            case 1:
                // Spawn Compost randomly from compostObjects array
                spawnedObject = Instantiate(compostObjects[Random.Range(0, compostObjects.Length - 1)], Vector3.zero, transform.rotation);
                //Marks the object as a compost
                spawnedObject.tag = "Compost T";
                break;
            case 2:
                // Spawn Landfill randomly from landfillObjects array
                spawnedObject = Instantiate(landfillObjects[Random.Range(0, landfillObjects.Length - 1)], Vector3.zero, transform.rotation);
                //Marks the object as a landfill object
                spawnedObject.tag = "Land Fill T";
                break;
            case -1:
                //Spawn the test trash while in test mode
                spawnedObject = Instantiate(testTrash, Vector3.zero, transform.rotation);
                break;
        }

        //Changes the parent from the camera to the image target. This is necessary to make sure
        //the object is oriented correctly
        spawnedObject.transform.SetParent(this.transform);
        //Places the object in front of the camera
        spawnedObject.transform.localPosition = new Vector3(0, 0, 1);
        //Sets the size of the object
        spawnedObject.transform.localScale = Vector3.one;



        //Gets the rigidbody of the object. This is the part of the object that physics can be applied to.
        Rigidbody spawnedObjectRigidbody = spawnedObject.GetComponent<Rigidbody>();
        //Activates the physics for the objects
        spawnedObjectRigidbody.isKinematic = true;

        currentObject = spawnedObject;

        return spawnedObject;
    }

    /*
    This method is called every frame after OnMouseUp is called and calculates the arc of the thrown trash object.
    */
    void throwBall()
    {

        //Makes sure object is held
        if (currentObject == null)
        {
            return;
        }

        isThrowing = true;

        //Gets the position of the camera
        Vector3 delta = Camera.main.transform.position;
        Debug.Log("CAMERA POSITION: " + Camera.main.transform.position.z);

        // Screen Drag deltas. This calculates the length of the user's swipe on the screen.
        float xDelta = (endPos.x - startPos.x) / Screen.width;
        float yDelta = (endPos.y - startPos.y) / Screen.height;

        //Calculating the force along each axis by comparing starting and ending values of time and finger position
        float XaxisForce = (xDelta * xMult);
        float YaxisForce = (yDelta * yMult);

        //Gets the position of the camera
        Vector3 cameraPos = Camera.main.transform.position;

        //Gets the position of the image target
        Vector3 projectedCamPos = Vector3.Project(cameraPos, Vector3.up);

        //Calculates the distance between the user and the image target
        float distance = Vector3.Distance(projectedCamPos, Vector3.zero);

        //Calculates the power of the throw
        float ZaxisForce = zMult * YaxisForce;


        //The final arc calculation that governs the throw of the trash object.
        var calculatedForce = new Vector3(XaxisForce, YaxisForce, ZaxisForce);

        // Transforms the camera-relative force into world space.
        Vector3 worldSpaceForceDirection = Camera.main.transform.TransformDirection(calculatedForce);

        //Makes it so the user is able to rotate their camera and throw an object in any direction.
        calculatedForce = worldSpaceForceDirection * calculatedForce.magnitude;

        //Gets the rigid body component of the held object that controls the physics.
        Rigidbody spawnObjRB = currentObject.GetComponent<Rigidbody>();

        //Applies gravity and the calculated arc to the trash object
        spawnObjRB.useGravity = true;
        spawnObjRB.isKinematic = false;
        spawnObjRB.velocity = calculatedForce;

        //Sets the parent of the object to be the trash bins so that orientation is set correctly.
        currentObject.transform.SetParent(vuforiaTargetObject.transform);

        //Resets the positions for the next object
        startPos = Vector2.zero;
        endPos = Vector2.zero;
        currentObject = null;

        //Calls the 'wait a second' funciton
        StartCoroutine(CoWaitToSpawnTrash(spawnWaitTime));

    }


    //Function that pauses for a second before spawning a new object
    IEnumerator CoWaitToSpawnTrash(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        currentObject = SpawnTrash();
    }

}