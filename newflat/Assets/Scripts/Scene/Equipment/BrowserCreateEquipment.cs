﻿using Core.Common.Logging;
using DataModel;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// 设备创建，显示，隐藏，browser
/// </summary>
public sealed class BrowserCreateEquipment
{
    private static ILog log = LogManagers.GetLogger("BrowserCreateEquipment");
    /// <summary>
    /// 创建当前场景的设备
    /// </summary>
    public static void CreateEquipment(System.Action createCallBack)
    {
        //search database
        EquipmentData.SearchCurrentEquipmentDataDownModel(() => {
            List<EquipmentItem> currentEquipmentData = EquipmentData.GetCurrentEquipment;
            if (currentEquipmentData == null || currentEquipmentData.Count == 0)
            {
                if (createCallBack != null)
                {
                    createCallBack.Invoke();
                }

                return;
            }
            log.Debug("创建设备" + currentEquipmentData.Count);


            StartCreateEquipment(currentEquipmentData, createCallBack);
            
        });
       
    }

    private static List<GameObject> gs = new List<GameObject>();
    private static float u_high = 0.04445f;
    public static void StartCreateEquipment(List<EquipmentItem> crateEquipmentDatas,System.Action createCallBack)
    {
        gs.Clear();
        Dictionary<string, GameObject> modelPrefebDic = ModelData.GetmodelPrefebDic;
        //所有的设备
        Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;

        foreach (EquipmentItem equipmentItem in crateEquipmentDatas)
        {
            DataModel.Type type = (DataModel.Type)Enum.Parse(typeof(DataModel.Type), equipmentItem.type);

            GameObject equipment = null;
            //创建有模型的设备
            if (!string.IsNullOrEmpty(equipmentItem.modelId) && modelPrefebDic.ContainsKey(equipmentItem.modelId) && 
                !equipmentDic.ContainsKey(equipmentItem.id))
            {
                equipment = GameObject.Instantiate(modelPrefebDic[equipmentItem.modelId]);
                SetEquipmentLayerAndScripts(equipment, equipmentItem, equipmentDic);

                if (type == DataModel.Type.De_It && !string.IsNullOrEmpty(equipmentItem.parentsId))
                {

                    SetItEquipmentProperty(equipmentItem, equipment);
                }
                else
                {
                    SetCurrentEquipmentShow();
                }
         
            }
            //创建漏水
            else if(type == DataModel.Type.De_LouShui && !equipmentDic.ContainsKey(equipmentItem.id))
            {
                equipment = new GameObject();
                SetEquipmentLayerAndScripts(equipment, equipmentItem, equipmentDic);

                LouShuiEquipmentControl loushui =  equipment.GetComponent<LouShuiEquipmentControl>();
                equipment.name = equipmentItem.name;
                if (loushui!=null)
                {
                    loushui.CreateLouShui(equipmentItem.loushuiPoints);
                }
                SetCurrentEquipmentShow();

                //it equipment
            } 
            if(equipment!=null)
            {
                gs.Add(equipment);
            }
        }

      
        if(AppInfo.Platform == BRPlatform.Browser)
        {
            EquipmentServiceInit.Init(gs);
        }

        if(createCallBack!=null)
        {
            createCallBack.Invoke();
        }
       
    }

    private static void SetEquipmentLayerAndScripts(GameObject equipment,EquipmentItem equipmentItem, 
        Dictionary<string, GameObject> equipmentDic)
    {
        Object3dUtility.SetLayerValue(LayerMask.NameToLayer("equipment"), equipment);
        equipment.SetActive(false);

        AddScript(equipment, equipmentItem);

        if (AppInfo.Platform == BRPlatform.Browser)
        {
            AddControlScripts(equipment.GetComponent<Object3DElement>().type, equipment);
        }

        if (!equipmentDic.ContainsKey(equipmentItem.id))
        {
            equipmentDic.Add(equipmentItem.id, equipment);
        }
    }

    private static void AddScript(GameObject equipment, EquipmentItem equipmentItem)
    {
        
        Object3DElement equipmentObject3DElement = equipment.AddComponent<Object3DElement>();

        equipmentObject3DElement.type = EquipmentUtility.GetTypeByName(equipmentItem.type);
        equipmentObject3DElement.equipmentData = equipmentItem;
  
        equipment.name = equipmentItem.name;
        equipmentObject3DElement.SetEquipmentData(equipmentItem);

    }

    private  static void AddControlScripts(DataModel.Type type,GameObject equipment)
    {
        string[] names =equipment.transform.GetChild(0).name.Split('_');
         var t =  System.Type.GetType(names[0]+"EquipmentControl");
		 equipment.AddComponent(t);

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
                NormalEquipmentControl bec = equipmentDic[equipmentItem.id].GetComponent<NormalEquipmentControl>();
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
    private static void SetCurrentEquipmentShow()
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
            NormalEquipmentControl bec = equipment.GetComponent<NormalEquipmentControl>();
            if(bec!=null)
            {
                bec.SetTipsShow(true);
            }
          
            //查找父对象
            Object3dItem parentScene = SceneData.FindObjUtilityect3dItemById(equipmentItem.sceneId);

            Object3DElement eObject3DElement = null;
            SceneData.gameObjectDic.TryGetValue(equipmentItem.sceneId, out eObject3DElement);
           

            //楼层或者房间

            if (eObject3DElement != null &&(eObject3DElement.type == DataModel.Type.Floor || eObject3DElement.type == DataModel.Type.Area || 
                (eObject3DElement.type == DataModel.Type.Room && istate is RoomState)))
            {
                GameObject root = SceneUtility.GetGameByRootName(parentScene.number, parentScene.number);
                GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
                SetParent(equipment, box.transform, equipmentItem);
            }
            else
            {
                //当前为楼层或者大楼显示房间的设备
                string parentparentid = parentScene.parentsId;
                Object3dItem parentparentObject = SceneData.FindObjUtilityect3dItemById(parentparentid);
                GameObject parentparentRoot = SceneUtility.GetGameByRootName(parentparentObject.number, parentparentObject.number);
                GameObject root = FindObjUtility.GetTransformChildByName(parentparentRoot.transform, parentScene.number);

                GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
                SetParent(equipment, box.transform, equipmentItem);
               
              
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
        Vector3 defaultAngles = new Vector3( equipmentItem.rotationX, equipmentItem.rotationY, equipmentItem.rotationZ);
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

    private static void SetItEquipmentProperty(EquipmentItem equipmentItem,GameObject equipment)
    {
        Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;
        Transform jiguiTransform = equipmentDic[equipmentItem.parentsId].transform;
        equipment.transform.SetParent(jiguiTransform);
        equipment.transform.localScale = Vector3.one;
        equipment.transform.localRotation = Quaternion.identity;
        float x = jiguiTransform.localPosition.x;
        float z = jiguiTransform.localPosition.z;

        float y = u_high * equipmentItem.childPosition-1;
        equipment.transform.localPosition = new Vector3(x, y, z);
        equipment.SetActive(true);
    }


    
   

}
