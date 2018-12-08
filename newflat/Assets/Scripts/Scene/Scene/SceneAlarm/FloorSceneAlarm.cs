using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSceneAlarm : SceneAlarmBase
{
    private void Start()
    { 
        Init();
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
