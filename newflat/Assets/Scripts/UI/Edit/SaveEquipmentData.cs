using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// 保存设备的数据
/// </summary>
public static class SaveEquipmentData
{
    public static void StartSave()
    {
       
        Dictionary<string, object> result = new Dictionary<string, object>();

        //新增的数据

        List<EquipmentItem> AddData = GetEquipmentItem(Object3DElement.GetNewList());

        result.Add("add", AddData);

        //删除的数据

        string deleteStr = Utils.StrUtil.ConnetString(Object3DElement.GetDeleteList(), ",");
        
        result.Add("delete", deleteStr);

        //修改变化的
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
       // Debug.Log(resultPostData);
        postData.Add("result", resultPostData);
        Equipment3dProxy.PostEquipmentSaveData((a) => {
            MessageBox.Show("信息提示","保存成功");

        }, postData);



    }


    private static List<EquipmentItem> GetEquipmentItem(List<Object3DElement> list)
    {
        List<EquipmentItem> result = new List<EquipmentItem>();
        foreach(Object3DElement item in list)
        {
            FormatUtil.FormatEquipmentData(item.equipmentData);
            result.Add(item.equipmentData);
        }

        return result;
    }

    public static void SaveDoor(EquipmentItem doorData)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        //新增的数据
        List<EquipmentItem> AddData = new List<EquipmentItem>();

        if (string.IsNullOrEmpty(doorData.id))
        {
            AddData.Add(doorData);
        }
        
        result.Add("add", AddData);

        //删除的数据

     

        result.Add("delete", "");

        //修改变化的
        List<EquipmentItem> modityData = new List<EquipmentItem>();
        if(!string.IsNullOrEmpty(doorData.id))
        {
            modityData.Add(doorData);
        }
        result.Add("update", modityData);
        Dictionary<string, string> postData = new Dictionary<string, string>();
        string resultPostData = CollectionsConvert.ToJSON(result);
        postData.Add("result", resultPostData);
        Equipment3dProxy.PostEquipmentSaveData((a) => {
            MessageBox.Show("信息提示", "保存成功");

        }, postData);
    }

}
