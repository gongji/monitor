using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChanors : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            GetComponent<RectTransform>().anchorMin = new Vector2(1,0);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        }
	}
}
