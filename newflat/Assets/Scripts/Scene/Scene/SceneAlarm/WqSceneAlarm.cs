using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WqSceneAlarm : SceneAlarmBase
{
    private void Start()
    {
        if(GetComponent<Object3DElement>()!=null)
        {
            sceneId = GetComponent<Object3DElement>().sceneId;
        }
        else
        {
            sceneId = SceneData.GetIdByNumber(transform.name);
        }
       
    }

    public override void Alarm()
    {
        //throw new System.NotImplementedException();
    }

    public override void Restore()
    {
       // throw new System.NotImplementedException();
    }
}
