using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSet:BaseSet
{
    private List<Object3dItem> list;

    private static ILog log = LogManagers.GetLogger("AreaSet");
    #region 设置全员场景初始化
    public override void Enter(List<Object3dItem> list, System. Action callBack)
    {
        base.Enter(list, callBack);
        this.list = list;
        ShowOrHideScene(true);
        SetSkyEffection();
        SetCameraProccessEffection();
        InitCameraPostion(callBack);
        SetTips();

    }
    /// <summary>
    /// 设置效果
    /// </summary>
    public  void SetSkyEffection()
    {
        GameObject renderGameObjerct = SceneUtility.GetGameByRootName(Constant.SkyboxName, "main");
        if (renderGameObjerct != null)
        {
            renderGameObjerct.GetComponent<RenderSettingsValue>().SetRenderSettings();
        }

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
        GameObject box =  SceneUtility.GetGameByRootName("main_dx",Constant.ColliderName,true);
        if(box!=null)
        {
            CameraInitSet.StartSet(Constant.AreaViewName, box.transform, 0.2f, callBack);

        }
        else
        {
            log.Error("box not find");
        }
       
    }
    /// <summary>
    /// 设置标签
    /// </summary>
    public  void SetTips()
    {
        
        List<Object3dItem> wqList = SceneData.GetAllWq();

        if (wqList != null && wqList.Count > 0)
        {
            foreach (Object3dItem object3dItem in wqList)
            {
                GameObject rootGameObjerct = SceneUtility.GetGameByRootName(object3dItem.code, object3dItem.code);
                if (rootGameObjerct != null)
                {

                    GameObject collider = FindObjUtility.GetTransformChildByName(rootGameObjerct.transform, Constant.ColliderName);
                    TipsMgr.Instance.CreateTips(collider, object3dItem, collider.transform,Vector3.one , Vector3.one * 2);
                }
            }
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
                    ShowOrHideScene(false);
                    RenderSettingsValue.SetNoAreaEffction();
                });

            });
        }
    }
    public void ShowOrHideScene(bool isVisible)
    {
        foreach(Object3dItem object3dItem in list)
        {
           
            SceneUtility.SetRootGameObjects(object3dItem.code, isVisible);
        }

    }

   public override void Exit(string nextid)
    {
        base.Exit(nextid);
        ShowOrHideScene(false);
        RenderSettingsValue.SetNoAreaEffction();
    }
    #endregion
}
