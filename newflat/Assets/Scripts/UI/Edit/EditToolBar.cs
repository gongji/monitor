using DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditToolBar : MonoBehaviour {

    private Transform save;

    private Transform viewReset;

    private void Start()
    {
        save = transform.Find("Save");
        viewReset = transform.Find("ViewReset");

        TransformControlUtility.AddEventToBtn(save.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { Save(save.gameObject);});
        TransformControlUtility.AddEventToBtn(viewReset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ResetView(); });

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
}
