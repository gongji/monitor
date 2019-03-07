using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMouseOver : MonoBehaviour {


    public string sceneid;
	

    protected void OnMouseEnter()
    {
        if ((EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
        {
            return;
        }

        EffectionUtility.PlaySinlgeMaterialEffect(transform.parent, Color.yellow);
    }

    protected void OnMouseExit()
    {
        if ((EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
        {
            return;
        }

        EffectionUtility.StopSinlgeMaterialEffect(transform.parent);
    }

    protected void OnDisable()
    {
        EffectionUtility.StopMulitMaterialEffect(transform.parent);
    }
}
