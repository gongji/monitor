using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testcollider23 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(GetComponent<BoxCollider>().bounds.size);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
