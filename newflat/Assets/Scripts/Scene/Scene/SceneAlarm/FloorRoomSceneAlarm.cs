using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 楼层房间报警处理
/// </summary>
public class FloorRoomSceneAlarm : SceneAlarmBase
{

    private void Start()
    {
        Init();
    }


    private GameObject alarmEffection = null;
    public override void Alarm()
    {
        if (isAlarm)
        {
            return;
        }
       
       SetAlarm();
            
        isAlarm = true;
    }

    private  void  SetAlarm()
    {
        alarmEffection = GameObject.Instantiate(EffectionResouceLoader.instance.effection);
        alarmEffection.transform.localEulerAngles = new Vector3(0, -90, 270);
        SetAlarmPosition();
    }

    public override void Restore()
    {

        if (!isAlarm)
        {
            return;
        }
        DestroyAlarmObject();
    }

    protected void DestroyAlarmObject()
    {
        if (alarmEffection != null)
        {
            GameObject.DestroyImmediate(alarmEffection);
            alarmEffection = null;
            isAlarm = false;
        }
    }

    private void OnDestroy()
    {

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        DestroyAlarmObject();
    }

    private void SetAlarmPosition()
    {

        GameObject box = FindObjUtility.GetTransformChildByName(transform, Constant.ColliderName);
        box.GetComponent<BoxCollider>().enabled = true;
        Bounds bounds = box.GetComponent<BoxCollider>().bounds;
        if (box != null && bounds != null)
        {
            alarmEffection.transform.position = bounds.center + box.transform.up * bounds.size.y;
        }

        box.GetComponent<BoxCollider>().enabled = false;
    }
}
