﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTheMusicGoing : MonoBehaviour {

	void Start()
	{
		DontDestroyOnLoad (transform.gameObject);
	}
}
