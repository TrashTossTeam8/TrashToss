using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/*
This class is used to allow the user to throw the object
that this script is attatched to. When the player drags a
piece of trash to throw it, this class should give a intuitive 
feel to the physics controlling the trash.
*/

public class ThrowAR : MonoBehaviour
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

	/// <summary>
	/// Trash object reference.
	/// </summary>
	public Rigidbody trash;


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
		Debug.Log("ITS USING THE RIGHT SCRIPT TO THROW");
		endTime = clock.getFrameNumber();
		endPos = Input.mousePosition;
		throwBall();
	}

	/*
    This method is called every frame after OnMouseUp is called and calculates the arc of the thrown trash object.
    */
	void throwBall()
	{

		//Distance Formula that measures the length of the user's finger swipe
		double distance = (Mathf.Sqrt(Mathf.Pow(endPos.x - startPos.x, 2) + Mathf.Pow(endPos.y - startPos.y, 2)));


		//Calculating the force along each axis by comparing starting and ending values of time and finger position
		XaxisForce = ((endPos.x - startPos.x) / (endTime - startTime)) * 10;
        YaxisForce = ((endPos.y - startPos.y) / (endTime - startTime)) * 10;
        ZaxisForce = ((((float)distance * 50) / (endTime - startTime)) * 10) / 50;

        Debug.Log("Force: " + ZaxisForce);

        
		//The final arc calculation that governs the throw of the trash object.
		calculatedForce = new Vector3(XaxisForce / 2, YaxisForce / 5, (ZaxisForce / 75) * 50f) / 10;
        Debug.Log ("CALCULATED FORCE: " + calculatedForce);

		//Applies gravity and the calculated arc to the trash object
		trash.useGravity = true;
		trash.velocity = calculatedForce;

		XaxisForce = 0;
		YaxisForce = 0;

		//Calls the wait a second funciton
		StartCoroutine(waitASecond());

	}

	//Function that pauses for a second before spawning a new ball
	IEnumerator waitASecond()
	{
		yield return new WaitForSeconds(2);
        //if (spawn != null)
        //{
        //	spawn.Start();
        //}
        //else
        //{
        //	otherSpawn.Start();
        //}
        GameObject heldTrash;
        if(otherSpawn)
        {
            heldTrash = otherSpawn.SpawnTrash();
        }
	}

}

