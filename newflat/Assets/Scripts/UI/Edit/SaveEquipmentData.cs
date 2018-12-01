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
        foreach (Object3DElement item in EquipmentData.allEquipmentDataDic.Values)
        {
            bool isSame = Object3dUtility.IsCompareObjectProperty(item.equipmentData, item.preEquipmentData);
            if(!isSame)
            {
                modityData.Add(item.equipmentData);
            }
        }

        result.Add("update", modityData);


        Dictionary<string, string> postData = new Dictionary<string, string>();
        postData.Add("result", CollectionsConvert.ToJSON(result));
        Equipment3dProxy.PostEquipmentSaveData((a) => {
            MessageBox.Show("信息提示","保存成功");

        }, postData);



    }


    private static List<EquipmentItem> GetEquipmentItem(List<Object3DElement> list)
    {
        List<EquipmentItem> result = new List<EquipmentItem>();
        foreach(Object3DElement item in list)
        {
            result.Add(item.equipmentData);
        }

        return result;


    }
}
