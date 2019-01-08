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
        if (alarmEffection == null)
        {
            string url = Application.streamingAssetsPath + "/"+ PlatformMsg.instance.currentPlatform.ToString()+ "/R/alarmeffection";

            ResourceUtility.Instance.GetHttpAssetBundle(url, (result) =>
            {
                alarmEffection = GameObject.Instantiate(result.LoadAsset<GameObject>("alarmeffection"));
                alarmEffection.transform.localEulerAngles = new Vector3(0, -90, 270);
                SetAlarmPosition();

            });
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


        DestroyAlarmObject();
    }

    private void DestroyAlarmObject()
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
