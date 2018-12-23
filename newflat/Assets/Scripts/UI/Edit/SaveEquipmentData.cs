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

        List<Object3DElement> addObject3DElements = Object3DElement.GetNewList();
        List<EquipmentItem> AddData = FormatAddEquipmentItem(addObject3DElements);

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
        Debug.Log(resultPostData);
        postData.Add("result", resultPostData);
        Equipment3dProxy.PostEquipmentSaveData((jsonString) => {

            List<EquipmentGuid>  addResultData = Utils.CollectionsConvert.ParseKey<List<EquipmentGuid>>("data",jsonString);
            CallBackUpdate(addObject3DElements, addResultData, modityData);
            //Debug.Log(resultData.Count);

            MessageBox.Show("信息提示","保存成功");

        }, postData);

    }


   


    /// <summary>
    /// 更新成功后，回调同步
    /// </summary>
    /// <param name="OriginalAddData"></param>
    /// <param name="addResultData"></param>
    /// <param name="modityData"></param>
    public  static void  CallBackUpdate(List<Object3DElement> addObject3DElements, List<EquipmentGuid> addResultData, List<EquipmentItem> modityData)
    {
        //清除所有的删除
        Object3DElement.ClearAllDelete();
        
        //同步修改的数据
        foreach(EquipmentItem modityItem  in modityData)
        {
            GameObject eg = EquipmentData.GetAllEquipmentData[modityItem.id];
            eg.GetComponent<Object3DElement>().preEquipmentData = modityItem.Clone() as EquipmentItem;
        }
        //处理增加

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
    /// 保存门禁数据
    /// </summary>
    /// <param name="doorData"></param>
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
