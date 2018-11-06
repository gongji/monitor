using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public object ExecuteCommand(Dictionary<string,object> data)
    {
        Debug.Log(data["id"]);
        throw new System.NotImplementedException();
    }
}
