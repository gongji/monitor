using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMath : MonoBehaviour {

    // Use this for initialization

    public Transform target;
    private Bounds bounds;
	void Start () {
        // int length = (int)c;

        // float a = 5 / 2.0f;
        // Debug.Log(a);
        // Debug.Log(Mathf.CeilToInt(a));
        // Debug.Log(Mathf.Round(2.6f));
        // Debug.Log(Mathf.Floor(a));

        bounds = target. GetComponent<BoxCollider>().bounds;

        
    }
	
	// Update is called once per frame
	void Update () {
		
        //if(Input.GetKeyUp(KeyCode.A))
        //{
        //    SetCamera();
        //}
	}
    Vector3 up;
    private void SetCamera(Bounds b)
    {
        float maxWidth = 0.0f;
        if (bounds.size.x > bounds.size.z)
        {
            up = target.TransformDirection(Vector3.forward);
            maxWidth = bounds.size.x;
        }
        else

        {
            up = target.TransformDirection(Vector3.right);
            maxWidth = bounds.size.z;
        }


        Camera.main.orthographic = true;
        Camera.main.transform.position = target.transform.position + target.up * 2;
        Camera.main.orthographicSize = maxWidth / 2 / (Screen.width * 1.0f / Screen.height * 1.0f);
        Quaternion rot = Quaternion.LookRotation(Vector3.down, up);
        Camera.main.transform.rotation = rot;

    }
}
