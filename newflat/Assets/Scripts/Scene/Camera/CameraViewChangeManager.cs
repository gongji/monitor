using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对2d和3d视角进行管理
/// </summary>
public class CameraViewChangeManager : MonoBehaviour {

	void Update () {
      
        if(Camera.main.gameObject.GetComponent<CameraObjectController>()==null)
        {
            return;
        }
        Camera.main.nearClipPlane = 0.01f;
        
        if (AppInfo.GetCurrentState is BuilderState)
        {

            Camera.main.orthographic = true;
            Camera.main.gameObject.GetComponent<CameraObjectController>().enabled = false;
            Camera.main.farClipPlane = 100;
        }
        else if(AppInfo.GetCurrentState is AreaState ||  AppInfo.currentView == ViewType.View3D)
        {
            Camera.main.orthographic = false;
            Camera.main.gameObject.GetComponent<CameraObjectController>().enabled = true;
            Camera.main.farClipPlane = 1000;
        }else
        {
            Camera.main.orthographic = true;
            Camera.main.gameObject.GetComponent<CameraObjectController>().enabled = true;
            Camera.main.farClipPlane = 1000;
        }
    }
}
