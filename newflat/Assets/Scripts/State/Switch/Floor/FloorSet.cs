using System;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using UnityEngine;
using DG.Tweening;
using System.Text.RegularExpressions;
using Core.Common.Logging;
using System.Linq;

public class FloorSet : FloorRoomSet {

    private static ILog log = LogManagers.GetLogger("FloorSet");
    
    public override void Enter(List<Object3dItem> currentDataList, Action callBack)
    {
        base.Enter(currentDataList, callBack);
        SetFloorRoomOffestPostion(currentDataList);
        OnInit(currentDataList, callBack,"F","返回");
        //设置管网按钮是否可见
        SetGuanwamgButttonVisible();
    }
    /// <summary>
    /// 退出带动画，进入房间
    /// </summary>
    /// <param name="nextid"></param>
    /// <param name="callBack"></param>
    public override void Exit(string nextid, Action callBack)
    {
        Debug.Log("exit动画");
        base.Exit(nextid, callBack);
        //GuanWangMsg.AllGuanWangHide();
        PullScreenCenter(nextid, (centerPostion, duringTime) => {

            Camera.main.transform.DOMove(centerPostion, duringTime).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1.0f, () => {
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
    /// 设置管网的按钮是否可见,并显示管网
    /// </summary>
    private void SetGuanwamgButttonVisible()
    {
        List<Object3dItem> list = SceneData.GetCurrentGuangWang();
        if(list.Count>0)
        {
            BrowserToolBar.instance.HideShowGuanwang(true);
            //显示管网的模型
            if(BrowserToolBar.instance.GetGuanWangToggleState())
            {
                GuanWangMsg.ShowGuanWangShow();
            }
           
        }
        else
        {
            BrowserToolBar.instance.HideShowGuanwang(false);
        }
        
    }
    /// <summary>
    /// 直接退出
    /// </summary>
    /// <param name="nextid"></param>
    public override void Exit(string nextid)
    {
        Debug.Log("exit直接");
        base.Exit(nextid);
        DeleteNavigation();
        //DeleteTips();
        DestryPlane();
        GuanWangMsg.AllGuanWangHide();



    }
}
