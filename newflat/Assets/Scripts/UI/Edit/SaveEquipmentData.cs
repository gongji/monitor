﻿using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public static class SaveEquipmentData
{
    public static void StartSave()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        List<Object3DElement> addObject3DElements = Object3DElement.GetNewList();
        List<EquipmentItem> AddData = FormatAddEquipmentItem(addObject3DElements);

        result.Add("add", AddData);
        string deleteStr = Utils.StrUtil.ConnetString(Object3DElement.GetDeleteList(), ",");
        
        result.Add("delete", deleteStr);
        List<EquipmentItem> modityData = new List<EquipmentItem>();
        foreach (GameObject item in EquipmentData.GetAllEquipmentData.Values)
        {
            if(!item)
            {
                continue;
            }
            Object3DElement object3DElement = item.GetComponent<Object3DElement>();
            if(string.IsNullOrEmpty(object3DElement.equipmentData.modelId))
            {
                continue;
            }
            bool isSame = Object3dUtility.IsCompareObjectProperty(object3DElement.equipmentData, object3DElement.preEquipmentData);
            //不相同
            if(!isSame)
            {
                FormatUtil.FormatEquipmentData(object3DElement.equipmentData);
                modityData.Add(object3DElement.equipmentData);
            }
        }

        result.Add("update", modityData);

        if(AddData.Count == 0 && string.IsNullOrEmpty(deleteStr.Trim()) && modityData.Count==0)
        {
            MessageBox.Show("信息提示", "无变化数据");
            return;
        }

        Dictionary<string, string> postData = new Dictionary<string, string>();
        string resultPostData = CollectionsConvert.ToJSON(result);
        Debug.Log("savedata:"+resultPostData);
        postData.Add("result", resultPostData);
        Equipment3dProxy.PostEquipmentSaveData((jsonString) => {

            List<EquipmentGuid>  addResultData = Utils.CollectionsConvert.ParseKey<List<EquipmentGuid>>("data",jsonString);
            CallBackUpdate(addObject3DElements, addResultData, modityData);
            //Debug.Log(resultData.Count);

            MessageBox.Show("信息提示","保存成功");

        }, postData);

    }

    /// <summary>
    /// callback synchronization
    /// </summary>
    /// <param name="OriginalAddData"></param>
    /// <param name="addResultData"></param>
    /// <param name="modityData"></param>
    public static void  CallBackUpdate(List<Object3DElement> addObject3DElements, List<EquipmentGuid> addResultData, List<EquipmentItem> modityData)
    {
        
        Object3DElement.ClearAllDelete();

        //synchronization modity
        foreach (EquipmentItem modityItem  in modityData)
        {
            GameObject eg = EquipmentData.GetAllEquipmentData[modityItem.id];
            eg.GetComponent<Object3DElement>().preEquipmentData = modityItem.Clone() as EquipmentItem;
        }
      //add

        Dictionary<string, GameObject> allequipmentDic = EquipmentData.GetAllEquipmentData;

        if(addResultData!=null)
        {
            foreach (EquipmentGuid guidEquipment in addResultData)
            {

                foreach (Object3DElement original in addObject3DElements)
                {
                    if (original.equipmentData.guid.Equals(guidEquipment.guid))
                    {
                        original.equipmentData.id = guidEquipment.id;
                        original.preEquipmentData = original.equipmentData.Clone() as EquipmentItem;
                        allequipmentDic.Add(original.equipmentData.id, original.gameObject);

                        break;
                    }
                }
            }

            Object3DElement.ClearAllAdd();
        }
       


    }

    private static List<EquipmentItem> FormatAddEquipmentItem(List<Object3DElement> list)
    {
        List<EquipmentItem> result = new List<EquipmentItem>();
        foreach (Object3DElement item in list)
        {
            EquipmentItem equipmentItem = item.equipmentData;
            FormatUtil.FormatEquipmentData(equipmentItem);
            equipmentItem.guid = Utils.StrUtil.GetNewGuid();
            result.Add(item.equipmentData);
        }

        return result;
    }

    /// <summary>
    /// save door
    /// </summary>
    /// <param name="doorData"></param>
    public static void SaveDoor(EquipmentItem doorData)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        //add
        List<EquipmentItem> AddData = new List<EquipmentItem>();

        if (string.IsNullOrEmpty(doorData.id))
        {
            AddData.Add(doorData);
        }
        
        result.Add("add", AddData);

        //delete
        result.Add("delete", "");

        //modity 
        List<EquipmentItem> modityData = new List<EquipmentItem>();
        if(!string.IsNullOrEmpty(doorData.id))
        {
            modityData.Add(doorData);
        }
        result.Add("update", modityData);
        Dictionary<string, string> postData = new Dictionary<string, string>();
        string resultPostData = CollectionsConvert.ToJSON(result);
        Debug.Log("save door data:"+ resultPostData);
        postData.Add("result", resultPostData);
        Equipment3dProxy.PostEquipmentSaveData((jsonString) => {
            List<EquipmentGuid> addResultData = Utils.CollectionsConvert.ParseKey<List<EquipmentGuid>>("data", jsonString);
            if(addResultData!=null && addResultData.Count==1)
            {
                doorData.id = addResultData[0].id;
            }
            MessageBox.Show("信息提示", "保存成功");

        }, postData);
    }

}
