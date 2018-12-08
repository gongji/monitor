using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneAlarmBase : MonoBehaviour {

    public string sceneId = string.Empty;
    protected bool isAlarm = false;
    public virtual void Alarm() {

        isAlarm = true;
    }
    public virtual void Restore() {
        isAlarm = false;
    }

    protected void Init()
    {
        if (GetComponent<Object3DElement>() != null)
        {
            sceneId = GetComponent<Object3DElement>().sceneId;
        }
        else
        {
            sceneId = SceneData.GetIdByNumber(transform.name);
        }
        SceneData.sceneAlarmDic.Add(sceneId, this);



    }
}
