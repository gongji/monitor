﻿using Core.Common.Logging;
using DataModel;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using DG.Tweening;


public sealed class InitCreateEquipment
{
    private static ILog log = LogManagers.GetLogger("BrowserCreateEquipment");
    /// <summary>
    ///create currrent equipment
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
            log.Debug("create equipment" + currentEquipmentData.Count);


            StartCreateEquipment(currentEquipmentData, createCallBack);
            
        });
       
    }

    private static List<GameObject> gs = new List<GameObject>();
    private static float u_high = 0.04445f;
    public static void StartCreateEquipment(List<EquipmentItem> createEquipmentDatas,System.Action createCallBack)
    {
        gs.Clear();
        Dictionary<string, GameObject> modelPrefebDic = ModelData.GetmodelPrefebDic;
      
        Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;

        foreach (EquipmentItem equipmentItem in createEquipmentDatas)
        {
            DataModel.Type type = (DataModel.Type)Enum.Parse(typeof(DataModel.Type), equipmentItem.type);

            GameObject equipment = null;
         
            if (!string.IsNullOrEmpty(equipmentItem.modelId) && modelPrefebDic.ContainsKey(equipmentItem.modelId) && 
                !equipmentDic.ContainsKey(equipmentItem.id))
            {
                equipment = GameObject.Instantiate(modelPrefebDic[equipmentItem.modelId]);
                SetEquipmentLayerAndScripts(equipment, equipmentItem, equipmentDic);

                if (type == DataModel.Type.De_It && !string.IsNullOrEmpty(equipmentItem.parentsId))
                {

                    SetItEquipmentProperty(equipmentItem, equipment);
                }
               
         
            }
           
            else if(type == DataModel.Type.De_LouShui && !equipmentDic.ContainsKey(equipmentItem.id))
            {
                equipment = new GameObject();
                SetEquipmentLayerAndScripts(equipment, equipmentItem, equipmentDic);

                LouShuiEquipmentControl loushui =  equipment.GetComponent<LouShuiEquipmentControl>();
                if(loushui==null)
                {
                    loushui = equipment.AddComponent<LouShuiEquipmentControl>();
                }
                equipment.name = equipmentItem.name;
                if (loushui!=null)
                {
                    DOVirtual.DelayedCall(1.0f, () => {
                        loushui.CreateLouShui(equipmentItem.loushuiPoints);
                    });
                   
                }

            } 
            if(equipment!=null)
            {
                gs.Add(equipment);
            }
        }
        SetCurrentEquipmentShow();

        if (AppInfo.Platform == BRPlatform.Browser)
        {
            EquipmentServiceInit.Init(gs);
        }

        if(createCallBack!=null)
        {
            createCallBack.Invoke();
        }
       
    }

    private void FindLouShuiBox(string sceneId)
    {
       
    }

    private static void SetEquipmentLayerAndScripts(GameObject equipment,EquipmentItem equipmentItem, 
        Dictionary<string, GameObject> equipmentDic)
    {
        Object3dUtility.SetObjectLayer(LayerMask.NameToLayer("equipment"), equipment);
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
        string resultName = "";
        foreach(Transform child in equipment.transform)
        {
            if(child.GetComponent<Camera>()!=null)
            {
                continue;
            }
            resultName = child.name;
        }
        if(!string.IsNullOrEmpty(resultName))
        {
            var t = System.Type.GetType(resultName + "EquipmentControl");
            equipment.AddComponent(t);
        }
    
    }
   
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
            if(equipment.GetComponent<Object3DElement>().type == DataModel.Type.De_It)
            {
                continue;
            }
            //ShowOrHideEquipment(equipment,true);
            equipment.SetActive(true);
            NormalEquipmentControl bec = equipment.GetComponent<NormalEquipmentControl>();
            if(bec!=null)
            {
                bec.SetTipsShow(true);
            }
          
            //find parent
            Object3dItem parentScene = SceneData.FindObjUtilityect3dItemById(equipmentItem.sceneId);

            Object3DElement eObject3DElement = null;
            SceneData.gameObjectDic.TryGetValue(equipmentItem.sceneId, out eObject3DElement);
           

            //floor or room

            if (eObject3DElement != null &&(eObject3DElement.type == DataModel.Type.Floor || eObject3DElement.type == DataModel.Type.Area || 
                (eObject3DElement.type == DataModel.Type.Room && istate is RoomState)))
            {
                GameObject root = SceneUtility.GetGameByRootName(parentScene.number, parentScene.number);
                GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
                SetParent(equipment, box.transform, equipmentItem);
            }
            else
            {
                //builder
                string parentparentid = parentScene.parentsId;
                Object3dItem parentparentObject = SceneData.FindObjUtilityect3dItemById(parentparentid);
                GameObject parentparentRoot = SceneUtility.GetGameByRootName(parentparentObject.number, parentparentObject.number);
                if(parentparentRoot!=null)
                {
                    GameObject root = FindObjUtility.GetTransformChildByName(parentparentRoot.transform, parentScene.number);
                    if (root != null)
                    {
                        GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
                        SetParent(equipment, box.transform, equipmentItem);
                    }
                }
            }
            
        }

    }

    /// <summary>
    /// set parent
    /// </summary>
    /// <param name="equipment"></param>
    /// <param name="rootParent"></param>
    /// <param name="equipmentItem"></param>
    private static void SetParent(GameObject equipment, Transform rootParent, EquipmentItem equipmentItem)
    {
       
        Transform parent = equipment.transform.parent;
        if(rootParent!=null && rootParent.Equals(parent))
        {
           // log.Debug("parent is same");
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
        equipment.SetActive(true);
        equipment.transform.SetParent(jiguiTransform);
       // equipment.transform.localScale = Vector3.one;
        equipment.transform.localRotation = Quaternion.identity;
        Debug.Log(equipmentItem.childPosition);
        float y = u_high * (equipmentItem.childPosition-1);
        if(y<0.01f)
        {
            y = 0.01f;
        }
        equipment.transform.localPosition = new Vector3(0, y, 0);
        
    }


    
   

}
