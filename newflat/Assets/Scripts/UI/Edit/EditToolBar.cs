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
        List<EquipmentItem> equipmentItemList = new List<EquipmentItem>();
        Object3DElement[] object3DElements = GameObject.FindObjectsOfType<Object3DElement>();
      
        foreach(Object3DElement item in object3DElements)
        {
            if(item.type == DataModel.Type.Equipment)
            {

                if(string.IsNullOrEmpty(item.equipmentData.id))
                {
                    item.equipmentData.id = Guid.NewGuid().ToString();
                }
               
                equipmentItemList.Add(item.equipmentData);
            }
            
        }
        //Debug.Log(equipmentItemList.Count);
        FileUtils.WriteContent(Application.streamingAssetsPath + "/equipment1.bat", FileUtils.WriteType.Write, Utils.CollectionsConvert.ToJSON(equipmentItemList));


    }

    public void ResetView()
    {
        CameraInitSet.ResetCameraPostion();
    }
}
