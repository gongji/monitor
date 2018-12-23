using Core.Common.Logging;
using DataModel;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设备创建，显示，隐藏
/// </summary>
public sealed class EquipmentSet  {

    private static ILog log = LogManagers.GetLogger("EquipmentSet");
    /// <summary>
    /// 创建当前场景的设备
    /// </summary>
    public static void CreateEquipment()
    {
        //查询当前的数据库
        EquipmentData.SearchCurrentEquipmentDataDownModel(() => {
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
            DataModel.Type type = (DataModel.Type)Enum.Parse(typeof(DataModel.Type), equipmentItem.type);

            GameObject equipment = null;
            //创建有模型的设备
            if (!string.IsNullOrEmpty(equipmentItem.modelId) && modelPrefebDic.ContainsKey(equipmentItem.modelId) && 
                !equipmentDic.ContainsKey(equipmentItem.id))
            {
                equipment = GameObject.Instantiate(modelPrefebDic[equipmentItem.modelId]);
                SetEquipmentLayerAndScripts(equipment, equipmentItem, equipmentDic);

            }
            //创建漏水
            else if(type == DataModel.Type.De_LouShui && !equipmentDic.ContainsKey(equipmentItem.id))
            {
               // Debug.Log(equipmentItem.loushuiPoints);
                //仅仅用于测试
               // equipmentItem.loushuiPoints = "-1.67,0.596,-5.06418|-1.67,0.596,-3.19418|-1.67,0.596,-1.29418|1.02,0.596,-1.29418|1.02,0.596,1.47582";
                equipment = new GameObject();
                SetEquipmentLayerAndScripts(equipment, equipmentItem, equipmentDic);

                LouShuiControl loushui =  equipment.GetComponent<LouShuiControl>();
                if(loushui!=null)
                {
                    loushui.CreateLouShui(equipmentItem.loushuiPoints);
                }

            }

            
      
        }

        SetCurrentEquipmentShow();
    }

    private static void SetEquipmentLayerAndScripts(GameObject equipment,EquipmentItem equipmentItem, Dictionary<string, GameObject> equipmentDic)
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
        
        if (type == DataModel.Type.De_Door)
        {

        }
        //漏水绳
        else if (type == DataModel.Type.De_LouShui)
        {
            equipment.AddComponent<LouShuiControl>();
        }
        else if(type == DataModel.Type.De_Normal)
        {
            equipment.AddComponent<NormalEquipmentControl>();
        }
        else if (type == DataModel.Type.De_WenShidu)
        {
            equipment.AddComponent<WenShiduEquipmentControl>();
        }
        else if(type == DataModel.Type.De_Camera)
        {
            equipment.AddComponent<CameraEquipmentControl>();
        }
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
