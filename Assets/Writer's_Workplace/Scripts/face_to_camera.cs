using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_to_camera : MonoBehaviour {
	public Camera cam;

	// Update is called once per frame
	void Update () {
		Vector3 pos = cam.transform.position;
		pos.y = transform.position.y;
		transform.LookAt(pos);
	}
}
