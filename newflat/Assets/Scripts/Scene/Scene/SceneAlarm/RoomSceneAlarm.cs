using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 房间报警
/// </summary>
public class RoomSceneAlarm : SceneAlarmBase
{
    private void Start()
    {
        sceneId = GetComponent<Object3DElement>().sceneId;
    }

    public override void Alarm()
    {
        throw new System.NotImplementedException();
    }

    public override void Restore()
    {
        throw new System.NotImplementedException();
    }
}
