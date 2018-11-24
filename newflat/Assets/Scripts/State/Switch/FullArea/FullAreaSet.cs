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
        SaveOrResetFloorPostion(currentlist);
        CameraInitSet.StartSet(SceneContext.buiderId, null, 0.5f, callBack);


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

    #endregion

    #region 退出场景的逻辑处理
    public override void Exit(string nextid, System.Action callBack)
    {

        base.Exit(nextid, callBack);
        
        Exit(nextid);
        if (callBack != null)
        {
            callBack.Invoke();
        }


    }
   

   public override void Exit(string nextid)
    {
        base.Exit(nextid);
        
        RenderSettingsValue.SetNoAreaEffction();
    }
    #endregion
}
