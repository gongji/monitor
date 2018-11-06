using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSwitch : MonoBehaviour {

    public static ViewSwitch instance;
    private bool is3D = true;

    private void Awake()
    {
        instance = this;
    }
    void Start () {

        TransformControlUtility.AddEventToBtn(gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { Switch(); });
    }

     private void Switch()
    {
        is3D = !is3D;
        

        if(!is3D)
        {
            AppInfo.currentView = ViewType.View2D;
            GetComponentInChildren<Text>().text = "3D视角";
            Switch2D(null);
        }
        else
        {
           
            AppInfo.currentView = ViewType.View3D;
            GetComponentInChildren<Text>().text = "2D视角";
            Switch3D();
        }
    }

    private Vector3 cameraPosition = Vector3.zero;
    private Vector3 angel = Vector3.zero;

    public void Switch2D(Transform box)
    {
       
        if(box==null)
        {
            box = SceneContext.sceneBox;
        }
     
        Camera.main.transform.position = box.transform.position + Vector3.up * 10.0f;
        Camera.main.transform.eulerAngles = new Vector3(90, 0, 0);
      

        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;
        if (mCurrentState is AreaState)
        {
            Camera.main.orthographic = false;
        }
        else
        {
            Camera.main.orthographic = true;
        }
        //transform.position = new Vector3(box.center.x, 30, box.center.z);
    }

    public void Switch3D()
    { 
        CameraInitSet.ResetCameraPostion();
    }
	
}
