/*
This is a temporary script to attempt to resolve the issue of the ARcamera
target displaying as shaky and shrinking away when in AR mode. This script
is currently not attatched to any ingame object and therefore is not 
doing anything in the program. - Lawrence Mueller
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth : MonoBehaviour {

	public GameObject ARcam;
	private float x, y, z;

	void Update () {
		Vector3 pos = ARcam.transform.position;
		x = x + ((pos.x - x) / 8);
		y = y + ((pos.x - y) / 8);
		z = z + ((pos.x - z) / 8);
		Vector3 cam = transform.position;
		cam.x = x;
		cam.y = y;
		cam.z = z;
		transform.position = cam;
	}
}
