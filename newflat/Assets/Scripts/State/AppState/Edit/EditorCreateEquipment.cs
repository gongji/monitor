using Core.Common.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 创建编辑态设备
/// </summary>
public sealed class EditorCreateEquipment
{

    private static ILog log = LogManagers.GetLogger("CreateEquipment");

    public static void Create(GameObject modelPrefeb,Vector3 hitpostion,Transform hitTransform)
    {
        if (string.IsNullOrEmpty(modelPrefeb.name))
        {
            log.Error("modelid is null");
            return;
        }
        GameObject Equipmentinstance = GameObject.Instantiate(modelPrefeb);
        Object3DElement equipment3DElement = Equipmentinstance.AddComponent<Object3DElement>();

        //增加，保存的时候使用
        Object3DElement.AddNewItem(equipment3DElement);
        equipment3DElement.type = EquipmentUtility.GetTypeByTransform(Equipmentinstance.transform);
        Equipmentinstance.transform.position = hitpostion;
        Equipmentinstance.name = modelPrefeb.name;
        Equipmentinstance.transform.localRotation = Quaternion.identity;

        Debug.Log(hitTransform.name);
        //设置父对象
        SetParent(hitTransform, hitpostion, Equipmentinstance, equipment3DElement);
        Object3dUtility.SetObjectLayer(LayerMask.NameToLayer("equipment"), Equipmentinstance);

        //  object3DElement.equipmentData.id = Guid.NewGuid().ToString();
        string[] str = modelPrefeb.name.Split(',');
        equipment3DElement.equipmentData.name = str[1];

        equipment3DElement.equipmentData.modelId = str[0];
        equipment3DElement.equipmentData.type = equipment3DElement.type.ToString();
        Equipmentinstance.transform.name = str[1];

        ShowModelList.instance.RemoveReset();
        UIElementCommandBar.instance.SelectEquipment(Equipmentinstance);

        DOVirtual.DelayedCall(1.0f, () => {
            PropertySet.instance.UpdateData(equipment3DElement.equipmentData);
        });
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
            object3DElement.equipmentData.sceneId = null;
        }
        else
        {
            object3DElement.equipmentData.sceneId = parent.GetComponent<Object3DElement>().sceneId;

        }

        Equipmentinstance.transform.localScale = Vector3.one * 1f;
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
