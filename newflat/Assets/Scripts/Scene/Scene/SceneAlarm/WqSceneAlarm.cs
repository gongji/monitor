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
        EffectionUtility.PlayOutlineEffect(transform, Color.white, Color.red);

    }

    public override void Restore()
    {
        // throw new System.NotImplementedException();
        EffectionUtility.StopOutlineEffect(transform);

    }

    private void OnDisable()
    {
        
    }
}
