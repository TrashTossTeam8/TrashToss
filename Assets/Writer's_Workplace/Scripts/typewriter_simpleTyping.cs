using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class typewriter_simpleTyping : MonoBehaviour {

	typewriter type;

	// Use this for initialization
	void Start () {
		type = GameObject.Find("Typewriter").transform.GetChild(0).GetComponent<typewriter>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Typing
		typewriter.errors err = typewriter.errors.not_set;
		if (Input.GetKeyDown (KeyCode.F5)) {
			type.TypeString ("Me - autotyping!\nI can write the whole page.\n", 0.1f, 0.4f);
		}

		//Keys A-Z
		for (int i = 0; i < type.letters.Length; i++) {
			if (Input.GetKeyDown (type.letters.Substring (i, 1).ToLower ())) {
				err = type.PressButtonAndType (i);
			}
			if (Input.GetKeyUp (type.letters.Substring (i, 1).ToLower ())) {
				err = type.ReleaseButton (i);
			}
		}
		//Space
		if (Input.GetKeyDown (KeyCode.Space))
			type.PressSpace ();
		if (Input.GetKeyUp (KeyCode.Space))
			type.ReleaseSpace ();
		//Shifts
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
			type.PressShift ();
		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift))
			type.ReleaseShift ();
		//Paper
		if (Input.GetKeyDown (KeyCode.F2))
			err = type.PaperInit ();
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter))
			err = type.PaperToNextPosition (); 		//Roll paper to next line
		if (Input.GetKeyDown (KeyCode.End))
			err = type.PaperOut ();
		//Carriage return
		if (Input.GetKeyDown (KeyCode.Tab))
			type.CarriageReturn ();

		//Hide / Show last error
		if (err != typewriter.errors.not_set) {
			Debug.Log("Typewriter return error: " + err.ToString() );
		}
	}
}
