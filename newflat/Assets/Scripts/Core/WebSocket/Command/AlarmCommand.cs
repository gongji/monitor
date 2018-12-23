﻿using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设备报警
/// </summary>
[SCommand]
public class AlarmCommand : ICommand
{
    public string Name
    {
        get
        {

            return "alarm";
        }
    }

    public object ExecuteCommand(string data)
    {
        
        List<EquipmentAlarm> list = Utils.CollectionsConvert.ToObject<List<EquipmentAlarm>>(data);

        throw new System.NotImplementedException();
    }
}
