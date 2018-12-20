using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoushui : MonoBehaviour {

    // Use this for initialization

    private string loushuiData = "";
	void Start () {
        loushuiData = "15,25,17|26,25,34|5,25,37|24,25,19|24,25,78";

    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<LouShuiControl>().CreateLouShui(loushuiData);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<LouShuiControl>().Alarm(1);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<LouShuiControl>().Alarm(2);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<LouShuiControl>().CancleAlarm();
        }


    }
}
