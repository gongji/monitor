using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMouseOver : MonoBehaviour {


    public string sceneid;
	

    protected void OnMouseEnter()
    {
        EffectionUtility.PlayMulitMaterialEffect(transform.parent, Color.blue);
    }

    protected void OnMouseExit()
    {
        EffectionUtility.StopMulitMaterialEffect(transform.parent);
    }

    protected void OnDisable()
    {
        EffectionUtility.StopMulitMaterialEffect(transform.parent);
    }
}
