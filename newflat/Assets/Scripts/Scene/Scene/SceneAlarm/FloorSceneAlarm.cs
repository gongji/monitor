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
        if(isAlarm)
        {
            return;
        }


        isAlarm = true;
        //throw new System.NotImplementedException();
    }

    public override void Restore()
    {
        // throw new System.NotImplementedException();

        if (!isAlarm)
        {
            return;
        }

        isAlarm = false;

    }
}
