using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is used to allow the user to throw the object
that this script is attatched to. When the player drags a
piece of trash to throw it, this class should give a intuitive
feel to the physics controlling the trash.
*/

public class Throw : MonoBehaviour
{
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

    public SpawnScript spawn;

    public ARSpawnScript otherSpawn;

    public Timer clock;


    /// <summary>
    /// These three force variables are used to calculate the arc of the throw in each dimension.
    /// </summary>
    private float XaxisForce;
    private float YaxisForce;
    private float ZaxisForce;

    /// <summary>
    /// The calculated product of the three force variables that determines the arc of the thrown object.
    /// </summary>
    private Vector3 calculatedForce;

    public bool isThrowing = false;

    /// <summary>
    /// Trash object reference.
    /// </summary>
    public Rigidbody trash;

    void Update()
    {
        //Debug.Log("isTrowing " + isThrowing);

        if (isThrowing == true)
        {

            if (trash.transform.position.y >= 5)
            {
                trash.transform.Rotate(5f, 0, 0);
            }
        }


    }


    /*
    This method is called when the user clicks down on their mouse on the object
    that this script is attatched to. All it does is keep track of the position of the user's finger
    and the time of when they first pressed the screen.
    */
    void OnMouseDown()
    {
        Debug.Log("TouchDown");
        startTime = clock.getFrameNumber();
        startPos = Input.mousePosition;
    }


    /*
    This method is called when the user lifts their finger from the screen. This method
    will make note of position of the user's finger when they lifted it up from the screen and the time
    when they did so. The throwBall method is then called to handle the arc of the throw.
    */
    public void OnMouseUp()
    {
        Debug.Log("TouchUp");
        endTime = clock.getFrameNumber();
        endPos = Input.mousePosition;
        throwBall();

    }

    /*
    This method is called every frame after OnMouseUp is called and calculates the arc of the thrown trash object.
    */
    void throwBall()
    {
        isThrowing = true;
        //Distance Formula that measures the length of the user's finger swipe
        double distance = (Mathf.Sqrt(Mathf.Pow(endPos.x - startPos.x, 2) + Mathf.Pow(endPos.y - startPos.y, 2)));


        //Calculating the force along each axis by comparing starting and ending values of time and finger position
        XaxisForce = ((endPos.x - startPos.x)/(endTime - startTime))*10;
        //YaxisForce = ((endPos.y - startPos.y)/(endTime - startTime))*10;
        //ZaxisForce = (endTime - startTime) * ((float)distance / 15);
        //ZaxisForce = ((((float)distance) / (endTime - startTime))*10)/2.75f;
        YaxisForce = 8.0f;
        ZaxisForce = 5.5f;

        Debug.Log("Force: " + ZaxisForce);



        //The final arc calculation that governs the throw of the trash object.
        //calculatedForce = new Vector3(XaxisForce/2, YaxisForce / 5, (ZaxisForce / 75) * 50f)/10;
        calculatedForce = new Vector3(XaxisForce / 2, YaxisForce / 5, (ZaxisForce) * 50f) / 10;

        //Applies gravity and the calculated arc to the trash object
        trash.useGravity = true;
        //trash.rotation()
        trash.velocity = calculatedForce;

        //spin();
        //trash.transform.Rotate(0, 90, 0);



        XaxisForce = 0;
        YaxisForce = 0;

        //Calls the wait a second funciton
        StartCoroutine(waitASecond());
        //isThrowing = false;
    }

    public void spin()
    {
        while(isThrowing)
        {
            trash.transform.Rotate(25f, 0, 0);
            if(trash.transform.position.y <= 5)
            {
                isThrowing = false;
                break;
            }
        }
    }

    //Function that pauses for a second before spawning a new ball
    IEnumerator waitASecond()
    {
        yield return new WaitForSeconds(2);
        if (spawn != null)
        {
            spawn.Start();
        }
        else
        {
            otherSpawn.Start();
        }
    }

}
