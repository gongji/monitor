using System;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using UnityEngine;
using DG.Tweening;
using System.Text.RegularExpressions;
using Core.Common.Logging;
using System.Linq;
using DG.Tweening;

public class FloorSet : FloorRoomSet {

    private static ILog log = LogManagers.GetLogger("FloorSet");
    
    public override void Enter(List<Object3dItem> currentDataList, Action callBack)
    {
        base.Enter(currentDataList, callBack);
        SetFloorRoomOffestPostion(currentDataList);
      
        //设置管网按钮是否可见
        SetJiDianButttonVisible(()=> {
            OnInit(currentDataList, callBack, "F", "返回");
            ExternalSceneSwitch.Instance.SaveSwitchData("2", SceneContext.currentSceneData.id);

        });
       
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
        GameObject box = null;
        box =  PullScreenCenter(nextid, (centerPostion, duringTime) => {

            //Camera.main.transform.DOMove(centerPostion, duringTime).OnComplete(() =>
            //{
            //    DOVirtual.DelayedCall(1.0f, () => {
            //        Exit(nextid);
            //        PlaneManger(null, false);
            //        if (callBack != null)
            //        {
            //            callBack.Invoke();
            //        }
            //    });
               
            //});

            if(box!=null)
            {
                CameraInitSet.SetCameraPosition(box.transform, () => {

                    DOVirtual.DelayedCall(1.0f, () => {
                        Exit(nextid);
                        PlaneManger(null, false);
                        if (callBack != null)
                        {
                            callBack.Invoke();
                        }
                    });
                });
            }
        });
          
    }

    
    private void SetJiDianButttonVisible(System.Action callBack)
    {
        if(BrowserToolBar.instance==null)
        {
            if(callBack!=null)
            {
                callBack.Invoke();
            }
            return;
        }
        
        List<Object3dItem> list = SceneData.GetCurrentGuangWang();
        if(list.Count>0 && AppInfo.currentView == ViewType.View3D)
        {
            BrowserToolBar.instance.HideShowJiDian(true);
            if(BrowserToolBar.instance.GetJiDianToggleState())
            {
                MTMsg.ShowJiDian(callBack);
            }
            else
            {
                if (callBack != null)
                {
                    callBack.Invoke();
                }
            }
           
        }
        else
        {
            if (callBack != null)
            {
                callBack.Invoke();
            }
            BrowserToolBar.instance.HideShowJiDian(false);
        }
        
    }
   
    public override void Exit(string nextid)
    {
       // Debug.Log("exit直接");
        base.Exit(nextid);
        DeleteNavigation();
        //DeleteTips();
        DestryPlane();
        MTMsg.AllJiDianHide();



    }
}
