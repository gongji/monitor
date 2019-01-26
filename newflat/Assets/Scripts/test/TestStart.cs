using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStart : Test {

    // Use this for initialization

    public Transform cube;
	void Start () {

       // Debug.Log("test");
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            var rotation = Quaternion.LookRotation(cube.transform.right);
            transform.rotation = rotation;

        }
    }
}
