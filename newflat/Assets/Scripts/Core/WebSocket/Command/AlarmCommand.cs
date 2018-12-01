using Core.Common.Logging;
using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设备报警
/// </summary>
[SCommand]
public class AlarmCommand : ICommand
{
    private static ILog log = LogManagers.GetLogger("AlarmCommand");


    public string Name
    {
        get
        {
            return "alarm";
        }
    }

    public object ExecuteCommand(string data)
    {
        log.Debug(data);
        EquipmentAlarmItem equipmentAlarm = Utils.CollectionsConvert.ToObject<EquipmentAlarmItem>(data);
        Dictionary<string, GameObject>  dic = EquipmentData.GetAllEquipmentData;
        if(dic.ContainsKey(equipmentAlarm.id))
        {
            //正常
            StartDoAlarmEquipment(equipmentAlarm, dic[equipmentAlarm.id]);
        }
        else
        {
            log.Debug(equipmentAlarm.id + "is not find");
        }

        return null;
    }

    /// <summary>
    /// //处理设备报警
    /// </summary>
    /// <param name="equipmentAlarm"></param>
    /// <param name="equipmentObject"></param>
    public static void  StartDoAlarmEquipment(EquipmentAlarmItem equipmentAlarm, GameObject equipmentObject)
    {

        Debug.Log("开始处理报警："+ equipmentAlarm.state + ":"+ equipmentAlarm.id);
        if (equipmentAlarm.state == 4)
        {
            //Debug.Log("报警恢复");
            equipmentObject.GetComponent<BaseEquipmentControl>().CancleAlarm();
        }
        else
        {
           // Debug.Log("开始报警");
            if(equipmentObject.GetComponent<Object3DElement>().type == DataModel.Type.De_LouShui)
            {
                equipmentObject.GetComponent<LouShuiControl>().LouShuiAlarm(equipmentAlarm.state, equipmentAlarm.segments);
            }
            else
            {
                equipmentObject.GetComponent<BaseEquipmentControl>().Alarm(equipmentAlarm.state);
            }
           
        }
    }
}
