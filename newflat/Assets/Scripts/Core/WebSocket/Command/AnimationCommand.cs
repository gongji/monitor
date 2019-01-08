﻿using Core.Server.Command;
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
        ExecuteAnimation(item);
        // throw new System.NotImplementedException();
        return null;
    }

    /// <summary>
    ///  execute single equipment animation
    /// </summary>
    /// <param name="item"></param>
    public static void ExecuteAnimation(EquipmentAnimationItem item)
    {
        Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;
        if(equipmentDic.ContainsKey(item.id))
        {
            List<AnimationItem> AnimationItemList = item.animation;
            if (AnimationItemList != null && AnimationItemList.Count > 0)
            {
                foreach(AnimationItem _item in AnimationItemList)
                {
                    equipmentDic[item.id].GetComponent<BaseEquipmentControl>().ExeAnimation(_item.name, _item.isRun);
                }
            }
            
        }
    }
}
