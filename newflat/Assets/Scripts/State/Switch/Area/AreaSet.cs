using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSet:BaseSet
{
    private static ILog log = LogManagers.GetLogger("AreaSet");
    #region 设置全员场景初始化
    public override void Enter(List<Object3dItem> currentlist, System. Action callBack)
    {
        log.Debug("AreaSet start init");
        base.Enter(currentlist, callBack);
        SetSkyEffection();
        InitCameraPostion(callBack);
        TipsMgr.Instance.Create3dText(SceneData.GetAllWq());
        string sceneid = SceneData.GetIdByNumber(Constant.Main_dxName.ToLower());

        if(AppInfo.Platform == BRPlatform.Browser)
        {
            SubsystemMsg.Create(sceneid);
           // BrowserToolBar.instance.FullAndColorAreaButtonReset();
        }

        ExternalSceneSwitch.Instance.SaveSwitchData("0", "0");

    }
   
    public void InitCameraPostion(System.Action callBack)
    {
        string sceneid = SceneData.GetIdByNumber(Constant.Main_dxName.ToLower());

       // Debug.Log("sceneid="+ sceneid);
        GameObject box =  SceneUtility.GetSceneCollider(Constant.Main_dxName);
        if(box!=null)
        {
            CameraInitSet.StartSet(sceneid, box.transform, 0.2f, callBack);

        }
        else
        {
            log.Error("box not find");

            if(callBack!=null)
            {
                callBack.Invoke();
            }
        }
       
    }
    

    #endregion



    #region exit 
    public override void Exit(string nextid, System.Action callBack)
    {

        base.Exit(nextid, callBack);
        //find wq camera
        GameObject cameraObject = SceneParse.FindWQMoveCamera(nextid);
        if (cameraObject != null)
        {

            CameraAnimation.CameraMove(Camera.main, cameraObject.transform.position, cameraObject.transform.eulerAngles, 0.5f, () => {
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    if (callBack != null)
                    {
                        callBack.Invoke();
                    }
                    Exit(nextid);
                });

            });
        }
        else
        {
            if (callBack != null)
            {
                callBack.Invoke();
            }
            Exit(nextid);
        }
    }
   

   public override void Exit(string nextid)
    {
        base.Exit(nextid);
        
        RenderSettingsValue.SetNoAreaEffction();
    }
    #endregion
}
