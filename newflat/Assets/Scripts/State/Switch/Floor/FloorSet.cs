using System;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using UnityEngine;
using DG.Tweening;
using System.Text.RegularExpressions;
using Core.Common.Logging;

public class FloorSet : FloorRoomSet {

    private static ILog log = LogManagers.GetLogger("FloorSet");
    
    public override void Enter(List<Object3dItem> currentDataList, Action callBack)
    {
        base.Enter(currentDataList, callBack);
        OnInit(currentDataList, callBack,"F","返回");

    }
    /// <summary>
    /// 退出带动画，进入房间
    /// </summary>
    /// <param name="nextid"></param>
    /// <param name="callBack"></param>
    public override void Exit(string nextid, Action callBack)
    {
        base.Exit(nextid, callBack);
        ExeRotaionAnimation(nextid, (centerPostion, duringTime) => {

            Camera.main.transform.DOMove(centerPostion, duringTime).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1.5f, () => {
                    Exit(nextid);
                    PlaneManger(null, false);
                    if (callBack != null)
                    {
                        callBack.Invoke();
                    }
                });
               
            });
        });
          
    }
    /// <summary>
    /// 直接退出
    /// </summary>
    /// <param name="nextid"></param>
    public override void Exit(string nextid)
    {
        base.Exit(nextid);
        DeleteNavigation();
        //DeleteTips();
        DestryPlane();
        GameObject root = SceneUtility.GetGameByRootName(currentObject.number, currentObject.number);
        if (root != null)
        {
            root.SetActive(false);
        }
       

    }
}
