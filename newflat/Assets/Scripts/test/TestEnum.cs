using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnum : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (string temp in Enum.GetNames(typeof(DataModel.Type)))
        {

            if(temp.Contains(transform.name))
            {
                DataModel.Type type = (DataModel.Type)Enum.Parse(typeof(DataModel.Type), temp, true);
                gameObject.AddComponent<Object3DElement>().type = type;
            }
        }

           
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
