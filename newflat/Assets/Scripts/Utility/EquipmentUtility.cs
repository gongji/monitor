using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EquipmentUtility  {

    /// <summary>
    /// 增加设备的类型
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static DataModel.Type GetTypeByTransform(Transform t)
    {

        Transform result = null;
        foreach(Transform item in t)
        {
            if(item.GetComponent<Camera>()!=null)
            {
                continue;
            }
            result = item;
        }

        if(!result)
        {
            return DataModel.Type.De_Normal;
        }

        //string name = t.GetChild(0).name;
        foreach (string enumItem in Enum.GetNames(typeof(DataModel.Type)))
        {

            if (enumItem.ToLower().Contains(result.name.ToLower()))
            {
                DataModel.Type etype = (DataModel.Type)Enum.Parse(typeof(DataModel.Type), enumItem, true);
                return etype;
            }
        }


        return DataModel.Type.De_Normal;
    }


    public static DataModel.Type GetTypeByName(string name)
    {
        DataModel.Type etype = (DataModel.Type)Enum.Parse(typeof(DataModel.Type), name, true);
        return etype;
    }
}
