using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 报警事件
/// </summary>
[SCommand]
public class EventCommand : ICommand
{
    public string Name
    {
        get
        {

            return "event";
        }
    }

    public object ExecuteCommand(string data)
    {
        
        List<AlarmEventItem> list = Utils.CollectionsConvert.ToObject<List<AlarmEventItem>>(data);

        throw new System.NotImplementedException();
    }
}
