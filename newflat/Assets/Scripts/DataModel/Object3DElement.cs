﻿using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Object3DElement : MonoBehaviour {

    private Dictionary<MeshRenderer, List<Material>> materialDic = new Dictionary<MeshRenderer, List<Material>>();
   
    public DataModel.Type type;
    //原来的
    [SerializeField]
    public EquipmentItem preEquipmentData = null;
    //现在的
    [SerializeField]
    public EquipmentItem equipmentData = new EquipmentItem();

    public void SetEquipmentData(EquipmentItem _equipmentData)
    {
        this.equipmentData = _equipmentData;
        if(AppInfo.Platform == BRPlatform.Editor)
        {
            this.preEquipmentData = _equipmentData.Clone() as EquipmentItem;
        }
    }

    public string sceneId = string.Empty;

    private void Update()
    {
      

        if(type.ToString().StartsWith(Constant.Equipment_Prefix) &&  AppInfo.Platform == BRPlatform.Editor 
            && !string.IsNullOrEmpty(equipmentData.modelId))
        {
            equipmentData.Update(transform.localPosition, transform.localScale, transform.localEulerAngles);
        }
        
    }

    public void SelectHigh(bool isSelected)
    {
        if (isSelected)
        {
            EffectionUtility.PlaySinlgeMaterialEffect(transform,Color.yellow);
        }
        else
        {
            EffectionUtility.StopSinlgeMaterialEffect(transform);
        }
    }


    #region delete 
    private static List<string> delete3dObjects = new List<string>();

    public static void AddDeleteItem(string deleteItemId)
    {
        delete3dObjects.Add(deleteItemId);
    }
    public static List<string> GetDeleteList()
    {
        return delete3dObjects;
    }

    public static void ClearAllDelete()
    {
        delete3dObjects.Clear();
    }

    #endregion


    #region add 
    private static List<Object3DElement> add3dObjects = new List<Object3DElement>();

    public static void AddNewItem(Object3DElement newtem)
    {
        add3dObjects.Add(newtem);
    }
    public static List<Object3DElement> GetNewList()
    {
        return add3dObjects;
    }

    public static void ClearAllAdd()
    {
        add3dObjects.Clear();
    }

    public static void DeleteNewItem(Object3DElement item)
    {
        
        for(int i=0;i< add3dObjects.Count;i++)
        {
            if(add3dObjects[i].Equals(item))
            {
                
                add3dObjects.Remove(item);
                break;
            }
        }
    }

    #endregion




}
