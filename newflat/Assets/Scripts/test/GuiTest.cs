using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string text = "nihaogggggg\nrrrr";
        
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 1000), text);
    }

}
