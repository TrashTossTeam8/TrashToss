using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is called simply to destroy the trash object 
after it collides with an object. This way they do not
stack up and clutter the scene. If the trash was allowed
to clutter it might compromise performance.
*/

public class DestroyR : MonoBehaviour {

/*
This method is called when the object that this script is 
attatched to collides with any other game object that has 
a collider. After a determined amount of time the object 
that this scirpt is attatched too (usually trash objects)
is destroyed.
*/

	void OnCollisionEnter(Collision floor)
	{
		Destroy (gameObject, 2f);
	}
}