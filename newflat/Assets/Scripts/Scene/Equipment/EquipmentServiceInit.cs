using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EquipmentServiceInit
{
    public static void Init(List<GameObject> gs)
    {
        if (gs.Count == 0)
        {

            return;
        }
        List<string> ids = new List<string>();
        foreach (GameObject child in gs)
        {
            string id = child.GetComponent<Object3DElement>().equipmentData.id;
            if (!string.IsNullOrEmpty(id))
            {
                ids.Add(child.GetComponent<Object3DElement>().equipmentData.id);
            }

        }

        SetEquipmentAlarmInitState(ids);
        SetEquipmentAnimationInitState(ids);
        gs.Clear();
    }
    /// <summary>
    /// alarm init
    /// </summary>
    private static void SetEquipmentAlarmInitState(List<string> ids)
    {
      

        if (ids.Count > 0)
        {

            string resultPostData = FormatUtil.ConnetString(ids, ",");

            Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;
            EquipmentAlarmProxy.GetEquipmentAlarmStateList((result) =>
            {
                List<EquipmentAlarmItem> list = Utils.CollectionsConvert.ToObject<List<EquipmentAlarmItem>>(result);
                if (list == null || list.Count == 0)
                {

                    Debug.Log("equipmentState is data is null=" + resultPostData);
                    return;
                }
                foreach (EquipmentAlarmItem dataItem in list)
                {
                    if (equipmentDic.ContainsKey(dataItem.id))
                    {
                        AlarmCommand.StartDoAlarmEquipment(dataItem, equipmentDic[dataItem.id]);
                    }
                }
            }
            , resultPostData);
        }


    }

    /// <summary>
    /// animatiaon init
    /// </summary>
    /// <param name="ids"></param>
    private static void SetEquipmentAnimationInitState(List<string> ids)
    {
        if (ids.Count > 0)
        {
            EquipmentAnimationProxy.GetAlarmEquipmentList((result) => {



            }, ids);

        }
    }
}

   
