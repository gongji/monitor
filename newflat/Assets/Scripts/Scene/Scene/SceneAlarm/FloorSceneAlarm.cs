using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSceneAlarm : SceneAlarmBase
{

    private Transform wqQiang = null;
    private void Start()
    { 
        Init();
        wqQiang = transform.Find("qt/qt_wq");
    }
    public override void Alarm()
    {
        if(isAlarm)
        {
            return;
        }

        EffectionUtility.PlayMulitMaterialEffect(wqQiang, Color.red);
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
        EffectionUtility.StopMulitMaterialEffect (wqQiang);
        isAlarm = false;

    }
}
