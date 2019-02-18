using System;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 房间设置
/// </summary>
public class RoomSet : FloorRoomSet
{
    public override void Enter(List<Object3dItem> currentData, Action callBack)
    {
        base.Enter(currentData, callBack);
        SetFloorRoomOffestPostion(currentData);
        //SetCurrentYValue(currentData);
        OnInit(currentData, callBack,"R","返回");
        ExternalSceneSwitch.Instance.SaveSwitchData("3", SceneContext.currentSceneData.id);
    }

    /// <summary>
    /// 相机移动到设备定位点
    /// </summary>
    public void CameraLocateEquipment()
    {
        GameObject cameraGameObject = SceneUtility.GetSceneCameraObject(currentObject.number, "EquipmentCamera");
        CameraAnimation.CameraMove(Camera.main, cameraGameObject.transform.position, cameraGameObject.transform.eulerAngles, cameraMoveTime, () => {

        });
    }
    /// <summary>
    /// 动画退出,退到上一级层
    /// </summary>
    /// <param name="nextid"></param>
    /// <param name="callBack"></param>
    public override void Exit(string nextid, Action callBack)
    {
        PullScreenCenter(nextid, (centerPostion, duringTime) => {

            Vector3 tartPostion = centerPostion - Camera.main.transform.forward * 5;
            Camera.main.transform.DOMove(tartPostion, duringTime).OnComplete(() =>
            {
                Exit(nextid);
                PlaneManger(null, false);
                if (callBack != null)
                {
                    callBack.Invoke();
                }

            });

            
            Main.instance.StartCoroutine(EffectionUtility.BlurEffection(duringTime,0,10));
        },true);
    }


 

    public override void Exit(string nextid)
    {
        base.Exit(nextid);

        DeleteNavigation();
       // DeleteTips();
        ShowOrHide(false);
        DestryPlane();
    }
    /// <summary>
    /// 设置当前的房间的高度值，在切换的时候看到动画效果
    /// </summary>
    //private void SetCurrentYValue(List<Object3dItem> currentDataList)
    //{
    //    if(currentDataList.Count ==1)
    //    {
    //        Object3dItem currentData = currentDataList[0];
    //        GameObject root = SceneUtility.GetGameByRootName(currentData.number, currentData.number);
    //        //没考虑到多层
    //        int index =  int.Parse( currentData.number.Substring(currentData.number.Length - 1, 1));
    //        root.transform.position =new  Vector3(root.transform.position.x, index *100, root.transform.position.z);
    //    }
        
    //}

    private void ShowOrHide(bool isShow)
    {
        if (currentObject != null)
        {
            GameObject root = SceneUtility.GetGameByRootName(currentObject.number, currentObject.number);
            root.SetActive(false);
        }

    }
        
}
