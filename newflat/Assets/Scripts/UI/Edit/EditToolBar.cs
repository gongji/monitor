using DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditToolBar : MonoBehaviour {

    private Transform save;

    private Transform viewReset;

    private Transform savelocate;

    public static EditToolBar instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        save = transform.Find("Save");
        viewReset = transform.Find("ViewReset");
        savelocate = transform.Find("Savelocate");
       
        TransformControlUtility.AddEventToBtn(save.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { SaveData(save.gameObject);});
        TransformControlUtility.AddEventToBtn(viewReset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ResetView(); });
        TransformControlUtility.AddEventToBtn(savelocate.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { SaveSceneLocate(); });

        //if (GameObject.Find("FPSController") != null)
        //{
        //    GameObject.DestroyImmediate(GameObject.Find("FPSController"));
        //}

        
    }
    /// <param name="g"></param>
    private void SaveData(GameObject g)
    {
        SaveEquipmentData.StartSave();
    }
    //camera reset
    public void ResetView()
    {
        CameraInitSet.ResetCameraPostion();
    }

    //save locate point
    private void SaveSceneLocate()
    {

        CameraViewData.SaveSceneCameraView();
    }

  
    public void SetViewButtonControl()
    {
        if(AppInfo.currentView == ViewType.View3D)
        {
            savelocate.gameObject.SetActive(true);
        }
        else
        {
            savelocate.gameObject.SetActive(false);
        }
    }

   
}
