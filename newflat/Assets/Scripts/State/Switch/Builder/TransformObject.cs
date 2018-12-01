using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformObject : MonoBehaviour {

    public Vector3 defaultPostion = Vector3.one;
    public Vector3 defaultRotaion = Vector3.one;
    
     void Awake()
    {
        defaultRotaion = transform.eulerAngles;
        defaultPostion = transform.position;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
         transform.eulerAngles = defaultRotaion ;
         transform.position = defaultPostion;
    }
}
