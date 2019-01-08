using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画执行
/// </summary>
[SCommand]
public class AnimationCommand : ICommand
{
    public string Name
    {
        get
        {
            return "animation";
        }
    }

    public object ExecuteCommand(string data)
    {
        
        EquipmentAnimationItem item = Utils.CollectionsConvert.ToObject<EquipmentAnimationItem>(data);

        throw new System.NotImplementedException();
    }
}
