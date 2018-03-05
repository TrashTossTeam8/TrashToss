using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is used to allow the user to throw the object
that this scirpt is attatched to. When the player drags a
piece of trash to throw it, this class should give a intuitive 
feel to the physics controlling the trash.
*/ 

public class Throw : MonoBehaviour {

	// This boolean is used to tell if the user is currently 
	// draging the trash in order to throw it
	bool drag = false;

	// This variable is the distance between the trash and the 
	// camera which is representational of the player
	float trashDistance;

	// This variable controls the initial value of speed that the 
	// trash is going to have
	public float throwSpeed;

	// This variable determines how far up the thrown object
	// will travel after being thrown
	public float arc;

	// This vairable is the max speed that the thrown trash can reach
	// during its travel
	public float speed;

/*
This method is called when the user clicks down on thier mouse on the object
that this script is attatched to. All this method does is calculate the
trashDistance and set drag to true becasue we know the user has clicked on
the trash indicating a desire to drag it.
*/ 
	void OnMouseDown()
	{
		trashDistance = Vector3.Distance (transform.position, Camera.main.transform.position);
		drag = true;
	}

/*
This method is called when the user lets go of the clicked mouse. This method
will calculate the speed, arc, and distance of the thrown trash. The in Unity 
gravity of the object is also turned on to allow drop.
*/ 
	public void OnMouseUp()
	{
		this.GetComponent<Rigidbody> ().useGravity = true;
		this.GetComponent<Rigidbody> ().velocity += this.transform.forward * throwSpeed;
		this.GetComponent<Rigidbody> ().velocity += this.transform.up * arc;
		drag = false;

	}

/*
This method is called every frame and attempts to calculate where the user is throwing
the trash in relation to the camera. 
*/ 
		
	void Update () {
		if (drag)
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint (trashDistance);
			transform.position = Vector3.Lerp (this.transform.position, rayPoint, speed * Time.deltaTime);
		}
	}
}
