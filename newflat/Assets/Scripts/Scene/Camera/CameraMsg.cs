﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class CameraMsg  {

	
    public static Camera GetCurrentCamera()
    {
        if(AppInfo.Platform == BRPlatform.Editor)
        {
            return Camera.main;
        }


        if(BrowserToolBar.instance!=null && BrowserToolBar.instance.IsFlyCameraMode)
        {
            return Camera.main;
        }
        else
        {
            return GameObject.Find("FPSController").GetComponentInChildren<Camera>();
        }
    }
}