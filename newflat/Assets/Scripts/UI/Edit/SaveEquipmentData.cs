using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        List<EquipmentItem> modityData = new List<EquipmentItem>();
        foreach (Object3DElement item in EquipmentData.equipmentDataDic.Values)
        {
            modityData.Add(item.equipmentData);
        }
        

        

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
