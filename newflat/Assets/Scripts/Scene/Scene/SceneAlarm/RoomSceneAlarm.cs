﻿using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 房间报警
/// </summary>
public class RoomSceneAlarm : FloorRoomSceneAlarm
{
    private void Start()
    {
        Init();
    }

    public override void Alarm()
    {
        base.Alarm();
    }

    public override void Restore()
    {
        base.Restore();
    }

    private void OnDisable()
    {
        DestroyAlarmObject();
    }

   
}
