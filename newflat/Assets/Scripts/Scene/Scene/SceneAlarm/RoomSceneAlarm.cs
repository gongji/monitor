using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 房间报警
/// </summary>
public class RoomSceneAlarm : SceneAlarmBase
{
    private void Start()
    {
        Init();
    }

    private GameObject alarmEffection = null;
    public override void Alarm()
    {
        if(alarmEffection!=null)
        {
            string url = Application.streamingAssetsPath + "/R/alarmeffection";

            ResourceUtility.Instance.GetHttpAssetBundle(url, (result) => {

                alarmEffection = result.LoadAsset<GameObject>("alarmeffection");
            });
        }
        

        //throw new System.NotImplementedException();
    }

    public override void Restore()
    {
        // throw new System.NotImplementedException();
        DestroyAlarmObject();
    }

    private void DestroyAlarmObject()
    {
        if (alarmEffection != null)
        {
            GameObject.DestroyImmediate(alarmEffection);
            alarmEffection = null;
        }
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
        Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(sceneId);
        if(object3dItem!=null)
        {
            GameObject box =  SceneUtility.GetSceneCollider(object3dItem.number);
            if(box!=null && box.GetComponent<BoxCollider>().bounds!=null)
            {
                
            }
        }
        
    }
}
