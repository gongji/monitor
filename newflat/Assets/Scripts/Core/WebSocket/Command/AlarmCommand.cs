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
        
        EquipmentAlarm equipmentAlarm = Utils.CollectionsConvert.ToObject<EquipmentAlarm>(data);
        Dictionary<string, GameObject>  dic = EquipmentData.GetAllEquipmentData;
        if(dic.ContainsKey(equipmentAlarm.id))
        {
            //正常
            if(equipmentAlarm.state == 4)
            {
                dic[equipmentAlarm.id].GetComponent<BaseEquipmentControl>().CancleAlarm();
            }
            else
            {
                dic[equipmentAlarm.id].GetComponent<BaseEquipmentControl>().Alarm();
            }
            
        }
        else
        {
            log.Debug(equipmentAlarm.id + "is not find");
        }

        return null;
    }
}
