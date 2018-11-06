using Core.Common.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建设备
/// </summary>
public sealed class CreateEquipment  {

    private static ILog log = LogManagers.GetLogger("CreateEquipment");

    public static void Create(GameObject modelPrefeb,Vector3 hitpostion,Transform hitTransform)
    {
        if (string.IsNullOrEmpty(modelPrefeb.name))
        {
            log.Error("modelid is null");
            return;
        }
        GameObject Equipmentinstance = GameObject.Instantiate(modelPrefeb);
        Object3DElement object3DElement = Equipmentinstance.AddComponent<Object3DElement>();
        object3DElement.type = DataModel.Type.Equipment;
        Equipmentinstance.transform.position = hitpostion;
        Equipmentinstance.transform.localRotation = Quaternion.identity;

        //设置父对象
        SetParent(hitTransform, hitpostion, Equipmentinstance, object3DElement);
        Object3dUtility.SetLayerValue(LayerMask.NameToLayer("equipment"), Equipmentinstance);

        //  object3DElement.equipmentData.id = Guid.NewGuid().ToString();
        object3DElement.equipmentData.name = "设备";
        
        object3DElement.equipmentData.modelid = modelPrefeb.name;

        ShowModelList.instance.RemoveReset();
        UIElementCommandBar.instance.SelectEquipment(Equipmentinstance);


    }

    /// <summary>
    /// 设置父对象
    /// </summary>
    /// <param name="hitTransform"></param>
    /// <param name="hitpostion"></param>
    /// <param name="Equipmentinstance"></param>
    /// <param name="object3DElement"></param>
    private static void SetParent(Transform hitTransform,Vector3 hitpostion,GameObject Equipmentinstance, Object3DElement object3DElement)
    {
        Transform parent = EditEquipmentSet.GetEquipmentParent(hitTransform, hitpostion);
        Debug.Log(parent);
        if (parent == null)
        {
            object3DElement.equipmentData.parentid = null;
        }
        else
        {
            object3DElement.equipmentData.parentid = parent.name;

        }

        Equipmentinstance.transform.localScale = Vector3.one * 0.1f;
        if (parent != null)
        {
            GameObject parentbox = FindObjUtility.GetTransformChildByName(parent, Constant.ColliderName);
            Debug.Log(parentbox);
            if (parentbox != null)
            {
                Equipmentinstance.transform.SetParent(parentbox.transform);

                return;
            }
           
        }
       
        Equipmentinstance.transform.SetParent(parent);
    }
}
