using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMath : MonoBehaviour {

	// Use this for initialization
	void Start () {
       // int length = (int)c;
        
        float a = 5 / 2.0f;
       // Debug.Log(a);
        Debug.Log(Mathf.CeilToInt(a));
       // Debug.Log(Mathf.Round(2.6f));
       // Debug.Log(Mathf.Floor(a));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
