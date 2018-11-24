using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clock : MonoBehaviour {

	public Transform hour;
	public Transform minute;
	public Transform seconds;
	public float hour_offset;
	public float minute_offset;
	public float second_offset;
	public bool negative = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		System.DateTime t = System.DateTime.Now;
		float h = (t.Hour * 30f) + (t.Minute * 0.5f);
		float m = (t.Minute * 6f);
		float s = (t.Second * 6f);

		if (negative) {
			h = -h;
			m = -m;
			s = -s;
		}

		h += hour_offset;
		m += minute_offset;
		s += second_offset;

		hour.localRotation = Quaternion.Euler (0f, 0f, h);
		minute.localRotation = Quaternion.Euler (0f, 0f, m);
		if (seconds)
			seconds.localRotation = Quaternion.Euler (0f, 0f, s);

	}
}
