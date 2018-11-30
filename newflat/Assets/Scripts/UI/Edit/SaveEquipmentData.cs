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
        result.Add("add", Object3DElement.GetNewList());
        string deleteStr = Utils.StrUtil.ConnetString(Object3DElement.GetDeleteList(), ",");
        //删除的数据
        result.Add("delete", deleteStr);
        

    }
}
