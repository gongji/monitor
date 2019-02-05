using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneAlarmBase : MonoBehaviour {

    public string sceneId = string.Empty;
    public string number = string.Empty;
    [SerializeField]
    protected bool isAlarm = false;
    public virtual void Alarm() {

        isAlarm = true;
    }
    public virtual void Restore() {
        isAlarm = false;
    }

    private string claasName = "";
    protected void Init()
    {
        System.Type t = this.GetType();
        claasName = t.Name;
        number = claasName;
        if (GetComponent<Object3DElement>() != null)
        {
            sceneId = GetComponent<Object3DElement>().sceneId;
        }
        else
        {
            sceneId = SceneData.GetIdByNumber(transform.name);
        }
        SceneData.sceneAlarmDic.Add(claasName + "_"+sceneId, this);
    }

    protected void RemoveDic()
    {
        if(SceneData.sceneAlarmDic.ContainsKey(claasName + "_" + sceneId))
        {
            SceneData.sceneAlarmDic.Remove(claasName + "_" + sceneId);
        }
    }

    protected void RemoveScene()
    {
        if(SceneData.sceneAlarmDic.ContainsKey(sceneId))
        {
            SceneData.sceneAlarmDic.Remove(sceneId);
        }
       

    }
}
