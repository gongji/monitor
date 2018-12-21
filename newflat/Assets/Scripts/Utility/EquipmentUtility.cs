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

        string name = t.GetChild(0).name;
        foreach (string temp in Enum.GetNames(typeof(DataModel.Type)))
        {

            if (temp.Contains(name))
            {
                DataModel.Type etype = (DataModel.Type)Enum.Parse(typeof(DataModel.Type), temp, true);
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
