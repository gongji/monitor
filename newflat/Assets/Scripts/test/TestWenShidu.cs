﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWenShidu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<WenShiduDataUpdate>().UpdateaData(100,100);
        }
	}
}