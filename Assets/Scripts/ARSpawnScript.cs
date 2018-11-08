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
    public GameObject vuforiaTargetObject;


    public float spawnWaitTime = 2f;

    // This is a gameObject place holder for the recycable trash type.
    // the trash object is directly referenced in the Unity engine 
    // through drag and drop

    [Header("TrashObjects")]
    public GameObject[] recyclableObjects;
    public GameObject[] compostObjects;
    public GameObject[] landfillObjects;

    public bool debugUseTester;
    public GameObject testTrash;

    //public GameObject waterBottle;
    //public GameObject cheeseBurger;
    //public GameObject pizzaBox;
    //public GameObject chipBag;
    //public GameObject book;
    //public GameObject toiletPaper;
    //public GameObject donut;
    //public GameObject hat;
    //public GameObject pencil;
    //public GameObject paper;
    //public GameObject glassBottle;
    //public GameObject sodaCan;
    //public GameObject waterMelon;
    //public GameObject tree;
    //public GameObject pineApple;
    //public GameObject tomato;
    //public GameObject rubberDuck;
    //public GameObject pen;
    //public GameObject coffeeLid;
    //public GameObject plasticBox;
    //public GameObject pillBottle;

    // This is a gameObject place holder for the Compost trash type.
    //// the trash object is directly referenced in the Unity engine 
    //// through drag and drop
    //public GameObject cTrash;

    //// This is a gameObject place holder for the Land Fill trash type.
    //// the trash object is directly referenced in the Unity engine 
    //// through drag and drop
    //public GameObject lTrash;

    //public GameObject[] rTrashArray; // array of available recyclables

    [Header("Throw Variables")]
    public float xMult = 1f;
    public float yMult = 1f;
    public float zMult = 1f; //0,4

    // Am I holding an object?
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


    public Timer clock;
    public GameObject spawnedObject;

    public float XaxisForce;
    public float YaxisForce;
    public float ZaxisForce;

    private bool isHoldingToThrow = false;
    public bool isThrowing = false;

    void Update()
    {
        // We detect if the trash object is thrown to not start the rotation too early
        if (isThrowing == true && spawnedObject != null)
        {
            // We do the rotation based on the value of Y and Z axes
            spawnedObject.transform.Rotate(5f, YaxisForce , ZaxisForce);
        }
    }

    /*
    This method is called when the game starts and is used to spawn a piece of
    trash for the player to sort at the begining of the game.
    */
    public void Start()
    {
        if (clock == null)
        {
            clock = FindObjectOfType<Timer>();
        }

        currentObject = SpawnTrash();
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
        // to determine which type of trash to spawn (1 is recycle, 2 is compost,
        // and 3 is land fill). The Instantiate function is called to spawn the trash
        // and is able to take in a vector which acts as coordinates and spawns the 
        // object at that location. A rotation type is also passed in due to neccesity,
        // Unity needs that information to call instantiate at a specific location.
        
        if(debugUseTester && testTrash != null)
        {
            randomizer = -1;
        }
        switch (randomizer)
        {
            default:
            case 0:
                // Spawn Recycling
                spawnedObject = Instantiate(recyclableObjects[Random.Range(0, recyclableObjects.Length - 1)], Vector3.zero, transform.rotation);
                spawnedObject.tag = "Recycle T";
                break;
            case 1:
                // Spawn Compost
                spawnedObject = Instantiate(compostObjects[Random.Range(0, compostObjects.Length - 1)], Vector3.zero, transform.rotation);
                spawnedObject.tag = "Compost T";
                break;
            case 2:
                // Spawn Landfill
                spawnedObject = Instantiate(landfillObjects[Random.Range(0, landfillObjects.Length - 1)], Vector3.zero, transform.rotation);
                spawnedObject.tag = "Land Fill T";
                break;
            case -1:
                spawnedObject = Instantiate(testTrash, Vector3.zero, transform.rotation);
                //spawnedObject.tag = "Recycle T";
                break;
        }


        //spawnedObject.transform.position = Vector3.zero;
        spawnedObject.transform.SetParent(this.transform);
        spawnedObject.transform.localPosition = new Vector3(0,0,1); // Put object in front of us.
        spawnedObject.transform.localScale = Vector3.one;

        

        // Turn off Physics of the Object.
        Rigidbody spawnedObjectRigidbody = spawnedObject.GetComponent<Rigidbody>();
        spawnedObjectRigidbody.isKinematic = true; // Freeze Object Physics

        currentObject = spawnedObject;

        //ToDo: Throw Object after input.

        return spawnedObject;
    }

    /*
    This method is called every frame after OnMouseUp is called and calculates the arc of the thrown trash object.
    */
    void throwBall()
    {
        if (currentObject == null)
        {
            return;
        }

        isThrowing = true;

        Vector3 delta = Camera.main.transform.position;
        Debug.Log("CAMERA POSITION: " + Camera.main.transform.position.z);

        // Screen Drag deltas
        float xDelta = (endPos.x - startPos.x) / Screen.width;
        float yDelta = (endPos.y - startPos.y) / Screen.height;

        //Distance Formula that measures the length of the user's finger swipe
        //float distance = (Mathf.Sqrt(Mathf.Pow(xDelta, 2) + Mathf.Pow(yDelta, 2)));


        //Calculating the force along each axis by comparing starting and ending values of time and finger position
        XaxisForce = (xDelta * xMult);
        YaxisForce = (yDelta * yMult);
        //float ZaxisForce = (((endTime - startTime) / (distance)) * zMult)*1.5f;

        //float YaxisForce = yForce;

        //float zCoordinate = Camera.main.transform.position.z;
        //float imageTargetZLocation = 10;


        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 projectedCamPos = Vector3.Project(cameraPos, Vector3.up);
        //cameraPos = new Vector3(cameraPos.x, 0, cameraPos.z);

        float distance = Vector3.Distance(projectedCamPos, Vector3.zero);
        //distance = projectedCamPos.magnitude;


        //float distance = Mathf.Abs(imageTargetZLocation - zCoordinate);

        //float ZaxisForce = 1.25f * distance;

        //float ZaxisForce = zMult * distance;
        ZaxisForce = zMult * YaxisForce;

        Debug.Log("FORCE: " + distance);
        



        //The final arc calculation that governs the throw of the trash object.
        //var calculatedForce = new Vector3(XaxisForce / 10, YaxisForce / 15, (ZaxisForce / 300) * 50f) * 2;
        var calculatedForce = new Vector3(XaxisForce, YaxisForce , ZaxisForce);
        //Debug.Log("CALCULATED FORCE: " + calculatedForce);

        // Transforms the camera-relative force into world space.
        Vector3 worldSpaceForceDirection = Camera.main.transform.TransformDirection(calculatedForce);
        calculatedForce = worldSpaceForceDirection * calculatedForce.magnitude;

        Rigidbody spawnObjRB = currentObject.GetComponent<Rigidbody>();

        //Applies gravity and the calculated arc to the trash object

        spawnObjRB.useGravity = true;
        spawnObjRB.isKinematic = false;

        spawnObjRB.velocity = calculatedForce;

        currentObject.transform.SetParent(vuforiaTargetObject.transform);

        startPos = Vector2.zero;
        endPos = Vector2.zero;
        currentObject = null;

        //Calls the 'wait a second' funciton
        StartCoroutine(CoWaitToSpawnTrash(spawnWaitTime));

    }


    //Function that pauses for a second before spawning a new ball
    IEnumerator CoWaitToSpawnTrash(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        currentObject = SpawnTrash();
    }

}