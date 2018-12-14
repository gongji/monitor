using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<FlyingText>().Initialize();
        FlyingText.GetObject("你好");


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
       // EffectionUtility.playSelectingEffect(transform);
    }

    private void OnMouseExit()
    {
       // EffectionUtility.StopFlashingEffect(transform);
    }
}
