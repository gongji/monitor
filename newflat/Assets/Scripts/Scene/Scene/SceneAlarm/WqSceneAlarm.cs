using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WqSceneAlarm : SceneAlarmBase
{
    private void Start()
    {
      
        Init();
    }

    public override void Alarm()
    {
        //throw new System.NotImplementedException();
        if(isAlarm)
        {
            return;
        }
        isAlarm = true;
        EffectionUtility.PlayMulitMaterialEffect(transform, Color.red);

    }


    public override void Restore()
    {
        // throw new System.NotImplementedException();
        if(!isAlarm)
        {
            return;
        }
        isAlarm = false;
        EffectionUtility.StopMulitMaterialEffect(transform);

    }

    private void OnDisable()
    {
        
    }
}
