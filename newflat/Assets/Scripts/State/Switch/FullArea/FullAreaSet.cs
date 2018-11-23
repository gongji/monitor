using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAreaSet : BaseSet
{
   

    private static ILog log = LogManagers.GetLogger("FullAreaSet");
    #region 设置全员场景初始化
    public override void Enter(List<Object3dItem> currentlist, System. Action callBack)
    {
        base.Enter(currentlist, callBack);
      //  SetSkyEffection();
       // SetCameraProccessEffection();
        InitCameraPostion(callBack);
     

    }
    /// <summary>
    /// 设置效果
    /// </summary>
    public  void SetSkyEffection()
    {
       

    }
    /// <summary>
    /// 设置相机特效和相机的范围
    /// </summary>
    public  void SetCameraProccessEffection()
    {
        GameObject cameraGameObject = SceneUtility.GetGameByComponent<Camera>(Constant.SkyboxName);
        //cameraGameObject.layer = LayerMask.NameToLayer("PostProcessing");
       // Camera.main.GetComponent<PostProcessVolume>().sharedProfile = cameraGameObject.GetComponent<PostProcessVolume>().sharedProfile;


    }

    public void InitCameraPostion(System.Action callBack)
    {
        GameObject box =  SceneUtility.GetGameByRootName(Constant.Main_dxName,Constant.ColliderName,true);
        if(box!=null)
        {
            CameraInitSet.StartSet(Constant.AreaViewName, box.transform, 0.2f, callBack);

        }
        else
        {
            log.Error("box not find");
        }
       
    }
  


    #endregion



    #region 退出场景的逻辑处理
    public override void Exit(string nextid, System.Action callBack)
    {

        base.Exit(nextid, callBack);
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
    }
   

   public override void Exit(string nextid)
    {
        base.Exit(nextid);
        
        RenderSettingsValue.SetNoAreaEffction();
    }
    #endregion
}
