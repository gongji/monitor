using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class CameraMsg  {

	
    public static Camera GetCurrentCamera()
    {
        if(BrowserToolBar.instance.IsFlyCameraMode)
        {
            return Camera.main;
        }
        else
        {
            return GameObject.Find("FPSController").GetComponentInChildren<Camera>();
        }
    }
}
