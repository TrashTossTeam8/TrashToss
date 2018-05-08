using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTheMusicGoing : MonoBehaviour {

	// Use this for initialization
	// and to keep the beat going ~~~~
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}

}
