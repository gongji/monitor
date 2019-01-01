using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAdjust : MonoBehaviour {

	// Update is called once per frame
	void Update () {
       Vector3 size =   GetComponentInParent<BoxCollider>().size;
        //Debug.Log(size);
       // Debug.Log(GetComponentInParent<BoxCollider>().bounds.size);

        transform.localScale = new Vector3(size.x * 0.1f, size.y *0.1f, 0.4f);
    }
}
