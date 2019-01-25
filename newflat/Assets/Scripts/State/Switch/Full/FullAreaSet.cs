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
    #region 设置全员场景初始化
    public override void Enter(List<Object3dItem> currentlist, System. Action callBack)
    {
        base.Enter(currentlist, callBack);
        //SaveOrResetFloorPostion(currentlist);
        Object3dItem currentScene = SceneContext.currentSceneData;
       // Debug.Log(currentScene.number);
        SceneContext.currentSceneData = FindMapWqItem();
        CameraInitSet.StartSet(SceneContext.buiderId, null, 0.5f, ()=> {

           SetSkyEffection();
        
           Object3dItem currentWq = SceneData.FindObjUtilityect3dItemById(SceneContext.buiderId);
           CreateNavigation(currentWq, null, "返回");

            if (callBack != null)
            {
                callBack.Invoke();
            }


        });
    }
    private GameObject uiTempObject;
  
    protected override void CreateNavigation(Object3dItem currentData, string frontname, string backName)
    {
        base.CreateNavigation(currentData, frontname, backName);
       // string parentid = currentData.parentsId;
        List<Object3dItem> wqList = SceneData.GetAllWq();
        Object3dItem tempWqItem = null;
        for (int i=0;i< wqList.Count;i++)
        {
           if(wqList[i].id.Equals(SceneContext.buiderId))
            {
                tempWqItem = wqList[i];
                wqList.RemoveAt(i);
                break;
            }
        }
        if(tempWqItem!=null)
        {
            wqList.Insert(0, tempWqItem);
        }
       

       // Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(parentid);
        if (fnu!=null)
        {
            fnu.CreateFloorRoomNavagitionList(wqList, navigationUI.transform, currentData, frontname, backName,true);
        }
        
       
    }

 

    /// <summary>
    /// 切换为旋转相机
    /// </summary>
    /// <param name="list"></param>
    private void SwitchCamera(List<Transform> list)
    {
        if(list==null || list.Count == 0)
        {
            return;
        }

        IEnumerable<Transform> result =
            from t in list
            orderby t.name ascending
            select t;

        Transform resultTransform = result.ToList<Transform>()[0];
        //Debug.Log(resultTransform);
        CameraInitSet.SetRotationCamera(resultTransform,true);
    }

    /// <summary>
    /// 查找wq的场景
    /// </summary>
    /// <returns></returns>
    private Object3dItem FindMapWqItem()
    {
        foreach (Object3dItem item in currentlist)
        {
            if (item.number.Contains(Constant.WQName))
            {
                return item;
            }
        }

        return null;
    }

    private Transform FindMapWqTransform()
    {
        Object3dItem mapWqItem = FindMapWqItem();
        GameObject g =   SceneUtility.GetGameByRootName(mapWqItem.number, mapWqItem.number);
        if(g!=null)
        {
            return g.transform;
        }

        return null;
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

        if(uiTempObject)
        {
            uiTempObject.GetComponent<ColorAreaNavigationUI>().DeleteAllUI();
            GameObject.DestroyImmediate(uiTempObject);
        }
        
        RenderSettingsValue.SetNoAreaEffction();
        CameraInitSet.SetObjectCamera();

        if(colorImageUI!=null)
        {
            GameObject.DestroyImmediate(colorImageUI);
        }
        
    }
    #endregion
}
