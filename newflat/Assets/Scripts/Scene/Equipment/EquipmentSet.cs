﻿using Core.Common.Logging;
using DataModel;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设备创建，显示，隐藏
/// </summary>
public sealed class EquipmentSet  {

    private static ILog log = LogManagers.GetLogger("EquipmentSet");
    /// <summary>
    /// 创建当前的设备
    /// </summary>
    public static void CreateEquipment()
    {
        EquipmentData.SearchCurrentEquipmentData(() => {
            StartCreateEquipment();
        });
       
    }

    private static void StartCreateEquipment()
    {
        List<EquipmentItem> currentEquipmentData = EquipmentData.GetCurrentEquipment;
        if (currentEquipmentData == null || currentEquipmentData.Count == 0)
        {
            return;
        }
        log.Debug("创建设备" + currentEquipmentData.Count);
        Dictionary<string, GameObject> modelPrefebDic = ModelData.GetmodelPrefebDic;
        //所有的设备
        Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;

        foreach (EquipmentItem equipmentItem in currentEquipmentData)
        {
            //避免重复创建
            if (!string.IsNullOrEmpty(equipmentItem.modelId) && modelPrefebDic.ContainsKey(equipmentItem.modelId) && !equipmentDic.ContainsKey(equipmentItem.id))
            {
                GameObject equipment = GameObject.Instantiate(modelPrefebDic[equipmentItem.modelId]);
                Object3dUtility.SetLayerValue(LayerMask.NameToLayer("equipment"), equipment);
                equipment.SetActive(false);

                AddScript(equipment, equipmentItem);
                if (!equipmentDic.ContainsKey(equipmentItem.id))
                {
                    equipmentDic.Add(equipmentItem.id, equipment);
                }

            }
        }

        SetCurrentEquipment();
    }

    private static void AddScript(GameObject equipment, EquipmentItem equipmentItem)
    {
        NormalEquipmentControl neControl = equipment.GetComponent<NormalEquipmentControl>();
        if (neControl == null && AppInfo.Platform == BRPlatform.Browser)
        {
            neControl = equipment.AddComponent<NormalEquipmentControl>();
            neControl.equipmentItem = equipmentItem;
        }
        Object3DElement equipmentObject3DElement = equipment.AddComponent<Object3DElement>();
        equipmentObject3DElement.type = Type.Equipment;
        equipmentObject3DElement.equipmentData = equipmentItem;
  
        equipment.name = equipmentItem.name;
        equipmentObject3DElement.SetEquipmentData(equipmentItem);

    }


    /// <summary>
    /// 隐藏设备标签
    /// </summary>
    public static void HideCurrentEquipmentTips()
    {
        List<EquipmentItem> currentEquipmentData = EquipmentData.GetCurrentEquipment;
        if(currentEquipmentData==null || currentEquipmentData.Count == 0)
        {
            return;
        }
        Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;

        foreach (EquipmentItem equipmentItem in currentEquipmentData)
        {
            if (!string.IsNullOrEmpty(equipmentItem.id) && equipmentDic.ContainsKey(equipmentItem.id))
            {
                BaseEquipmentControl bec = equipmentDic[equipmentItem.id].GetComponent<BaseEquipmentControl>();
                if(bec!=null)
                {
                    bec.SetTipsShow(false);
                }
            }
        }
    }

    /// <summary>
    /// 设置当前的设备父子归属关系，并显示出来
    /// </summary>
    private static void SetCurrentEquipment()
    {

        List<EquipmentItem> currentEquipmentData = EquipmentData.GetCurrentEquipment;
        Dictionary<string, GameObject> allEquipmentDic = EquipmentData.GetAllEquipmentData;

        IState istate = AppInfo.GetCurrentState;
        foreach (EquipmentItem equipmentItem in currentEquipmentData)
        {
            if(string.IsNullOrEmpty(equipmentItem.id))
            {
                continue;
            }
            GameObject equipment = allEquipmentDic[equipmentItem.id];
            //ShowOrHideEquipment(equipment,true);
            equipment.SetActive(true);
            BaseEquipmentControl bec = equipment.GetComponent<BaseEquipmentControl>();
            if(bec!=null)
            {
                bec.SetTipsShow(true);
            }
            //非园区的情况
            //if (string.IsNullOrEmpty(equipmentItem.parentid))
            //{
            //查找父对象
            Object3dItem parent = SceneData.FindObjUtilityect3dItemById(equipmentItem.sceneId);

            //楼层或者房间
            if (parent!=null &&(parent.type == Type.Floor || (parent.type == Type.Room && istate is RoomState)))
            {
                GameObject root = SceneUtility.GetGameByRootName(parent.number, parent.number);
                GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
                SetParent(equipment, box.transform, equipmentItem);
            }
            else
            {
                //园区
                if(parent==null)
                {
                    SetParent(equipment, null, equipmentItem);
                }
                //设置大楼下的房间
                else
                {
                    string parentparentid = parent.parentsId;
                    Object3dItem parentparentObject = SceneData.FindObjUtilityect3dItemById(parentparentid);
                    GameObject parentparentRoot = SceneUtility.GetGameByRootName(parentparentObject.number, parentparentObject.number);
                    GameObject root = FindObjUtility.GetTransformChildByName(parentparentRoot.transform, parent.number);

                    GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
                    SetParent(equipment, box.transform, equipmentItem);
                }
              
            }
            
        }

    }

    /// <summary>
    /// 设置设备父对象，并隐藏碰撞器
    /// </summary>
    /// <param name="equipment"></param>
    /// <param name="rootParent"></param>
    /// <param name="equipmentItem"></param>
    private static void SetParent(GameObject equipment, Transform rootParent, EquipmentItem equipmentItem)
    {
       
        Transform parent = equipment.transform.parent;
        if(rootParent!=null && rootParent.Equals(parent))
        {
            log.Debug("parent is same");
            return;
        }
        equipment.transform.SetParent(rootParent);
        equipment.transform.localScale = new Vector3( equipmentItem.scaleX, equipmentItem.scaleY, equipmentItem.scaleZ);
        equipment.transform.localPosition = new Vector3(equipmentItem.x, equipmentItem.y, equipmentItem.z);
        equipment.transform.localEulerAngles = new Vector3( equipmentItem.rotationX, equipmentItem.rotationY, equipmentItem.rotationZ);
        if(rootParent!=null)
        {
            BoxCollider collider = rootParent.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
        
    }
   

}
