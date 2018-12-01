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
            return "alarmEvent";
        }
    }

    public object ExecuteCommand(string data)
    {

        Debug.Log("执行："+ data);
        AlarmEventItem alarmEventItem = Utils.CollectionsConvert.ToObject<AlarmEventItem>(data);
        if(alarmEventItem!=null)
        {
            Debug.Log("执行1");
            ShowAlarmEvent.instance.Show(alarmEventItem);
        }
        else
        {
            Debug.Log("result i null"+ data);
        }

        return null;
        //throw new System.NotImplementedException();
    }
}
