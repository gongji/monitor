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
        base.Enter(currentlist, callBack);
        SetSkyEffection();
        InitCameraPostion(callBack);
        SetTips();
        string sceneid = SceneData.GetIdByNumber(Constant.Main_dxName.ToLower());
        CreateSubsystem.Create(sceneid);
    }
   
    public void InitCameraPostion(System.Action callBack)
    {
        string sceneid = SceneData.GetIdByNumber(Constant.Main_dxName.ToLower());

       // Debug.Log("sceneid="+ sceneid);
        GameObject box =  SceneUtility.GetGameByRootName(Constant.Main_dxName,Constant.ColliderName,true);
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
                GameObject rootGameObjerct = SceneUtility.GetGameByRootName(object3dItem.number, object3dItem.number);
                if (rootGameObjerct != null)
                {

                    GameObject collider = FindObjUtility.GetTransformChildByName(rootGameObjerct.transform, Constant.ColliderName);
                    TipsMgr.Instance.CreateTips(collider, object3dItem, collider.transform,Vector3.one * 5 , Vector3.one * 10);
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
