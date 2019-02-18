using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System.Linq;

public class FullAreaSet : BaseSet
{
    private static ILog log = LogManagers.GetLogger("FullAreaSet");
    private GameObject colorImageUI;
    #region 
    public override void Enter(List<Object3dItem> currentlist, System. Action callBack)
    {
        base.Enter(currentlist, callBack);
        //Object3dItem currentScene = SceneContext.currentSceneData;
        CameraInitSet.StartSet(SceneContext.areaBuiderId, null, 0.5f, ()=> {

           SetSkyEffection();
        
           Object3dItem currentWq = SceneData.FindObjUtilityect3dItemById(SceneContext.areaBuiderId);
           CreateNavigation(currentWq, null, "返回");

            SwitchCamera();
            if (callBack != null)
            {
                callBack.Invoke();
            }
            ExternalSceneSwitch.Instance.SaveSwitchData("5", SceneContext.currentSceneData.id);
        });
    }
    private GameObject uiTempObject;
  
    protected override void CreateNavigation(Object3dItem currentData, string frontname, string backName)
    {
        base.CreateNavigation(currentData, frontname, backName);
       // string parentid = currentData.parentsId;
        List<Object3dItem> wqList = SceneData.GetAllWq();
        //Object3dItem tempWqItem = null;
        //for (int i=0;i< wqList.Count;i++)
        //{
        //   if(wqList[i].id.Equals(SceneContext.areaBuiderId))
        //    {
        //        tempWqItem = wqList[i];
        //        wqList.RemoveAt(i);
        //        break;
        //    }
        //}
        //if(tempWqItem!=null)
        //{
        //    wqList.Insert(0, tempWqItem);
        //}
       

       // Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(parentid);
        if (fnu!=null)
        {
            fnu.CreateFloorRoomNavagitionList(wqList, navigationUI.transform, currentData, frontname, backName,true);
        }
       
    }
    /// <summary>
    /// set rotation camera
    /// </summary>
    /// <param name="list"></param>
    private void SwitchCamera()
    {
        Object3dItem  item =  SceneData.FindObjUtilityect3dItemById(SceneContext.areaBuiderId);
        GameObject root = SceneUtility.GetGameByRootName(item.number, item.number);
        GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
        if(box!=null)
        {
            CameraInitSet.SetRotationCamera(box.transform, true);
        }
       
    }

    private FlyTextMeshModel tmm = null;

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
        CameraInitSet.SetObjectCamera();
        if (navigationUI != null)
        {
            GameObject.Destroy(navigationUI);
        }

    }
    #endregion
}
