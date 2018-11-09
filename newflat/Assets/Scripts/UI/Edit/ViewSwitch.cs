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
    
        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;
        if (mCurrentState is AreaState)
        {
            Camera.main.orthographic = false;
            Camera.main.transform.position = box.transform.position + Vector3.up * 10.0f;
            Camera.main.transform.eulerAngles = new Vector3(90, 0, 0);

        }
        else
        {
            Camera.main.orthographic = true;
            bool flag = box.GetComponent<BoxCollider>().enabled;
            box.GetComponent<BoxCollider>().enabled = true;
            Set2Dview(box);
            box.GetComponent<BoxCollider>().enabled = flag;
        }
        //transform.position = new Vector3(box.center.x, 30, box.center.z);
    }

    private void Set2Dview(Transform box)
    {
       Bounds  bounds = box.GetComponent<BoxCollider>().bounds;
        float maxWidth = 0.0f;
        Vector3 up;
        if (bounds.size.x > bounds.size.z)
        {
            up = box.TransformDirection(Vector3.forward);
            maxWidth = bounds.size.x;
        }
        else

        {
            up = box.TransformDirection(Vector3.right);
            maxWidth = bounds.size.z;
        }
        Camera.main.orthographic = true;
        Camera.main.transform.position = box.transform.position + box.up * 2;
        Camera.main.orthographicSize = maxWidth / 2 / (Screen.width * 1.0f / Screen.height * 1.0f);
        Quaternion rot = Quaternion.LookRotation(Vector3.down, up);
        Camera.main.transform.rotation = rot;
    }

    public void Switch3D()
    { 
        CameraInitSet.ResetCameraPostion();
    }
	
}
