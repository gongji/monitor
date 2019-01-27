using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class CameraMsg  {

	
    public static Camera GetCurrentCamera()
    {
        if(AppInfo.Platform == BRPlatform.Editor)
        {
            return Camera.main;
        }

        
        if(BrowserToolBar.instance!=null && ThirdSwitchMsg.instacne.IsFlyCameraMode)
        {
            return Camera.main;
        }
        else
        {
            return GameObject.Find("thirdPerson").GetComponentInChildren<Camera>();
        }
    }
}
