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

    private void Start()
    {
        save = transform.Find("Save");
        viewReset = transform.Find("ViewReset");
        savelocate = transform.Find("Savelocate");

        TransformControlUtility.AddEventToBtn(save.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { Save(save.gameObject);});
        TransformControlUtility.AddEventToBtn(viewReset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ResetView(); });

        TransformControlUtility.AddEventToBtn(savelocate.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { SaveLocate(); });

    }

    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="g"></param>
    private void Save(GameObject g)
    {
        SaveEquipmentData.StartSave();


    }

    public void ResetView()
    {
        CameraInitSet.ResetCameraPostion();
    }

    //

    private void SaveLocate()
    {
        CameraViewEdit.SaveSceneCameraView();
    }
}
