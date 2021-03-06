﻿using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class FloorRoomSet : BaseSet
{
    protected float cameraMoveTime = 0.5f;

    protected Object3dItem currentObject;
    private static ILog log = LogManagers.GetLogger("FloorRoomSet");

    private GameObject plane;
    private int offestIndex = 1;

    public override void Enter(List<Object3dItem> currentDataList, Action callBack)
    {
        base.Enter(currentDataList, callBack);
        SubsystemMsg.Create(SceneContext.currentSceneData.id);

    }
     protected void OnInit(List<Object3dItem> currentDataList, Action callBack, string frontname, string backName)
    {
        SwitchBG(false);
        this.currentObject = SceneContext.currentSceneData;
        Transform box = SceneUtility.GetSceneCollider(currentObject.number).transform;
       // PlaneManger(box, true);
        CameraInitSet.StartSet(currentObject.id, box.transform, 0.5f, () =>
        {
            CreateTips(currentObject);
            if (AppInfo.Platform == BRPlatform.Browser)
            {
                CreateNavigation(currentObject, frontname, backName);
            }
            if (callBack != null)
            {
                callBack.Invoke();
            }
        });
    }
  

    
    protected void CreateTips(Object3dItem object3dItem)
    {
        GameObject g = SceneUtility.GetGameByRootName(object3dItem.number, object3dItem.number);
        //if (g.GetComponentsInChildren<MouseTips>().Length > 0)
        //{
        //    return;
        //}
        Regex jsRegex = new Regex("fj\\d");

        foreach (Transform child in g.transform)
        {
            string[] names = child.name.ToLower().Split('_');
            string endStr = names[names.Length - 1].ToLower().Trim();
            if (jsRegex.IsMatch(endStr))
            {
                Object3dItem roomObject = GetRoomObjectById(object3dItem, child.name);
                GameObject collider = FindObjUtility.GetTransformChildByName(child, Constant.ColliderName);
                if (roomObject != null && collider != null)
                {

                    TipsMgr.Instance.CreateTips(collider, roomObject, child, Vector3.one * 1f, Vector3.one * 3.0f);
                }
            }
        }
    }


    //protected void DeleteTips()
    //{
        //Object3dItem parentObject = SceneData.FindObjUtilityect3dItemById(currentObject.parentid);
        //if (parentObject != null && parentObject.childs != null && parentObject.childs != null)
        //{
        //    foreach (Object3dItem item in parentObject.childs)
        //    {
        //        GameObject root = SceneUtility.GetGameByRootName(item.code, item.code);
        //        if (root != null)
        //        {
        //            MouseTips[] mtips = root.GetComponentsInChildren<MouseTips>(true);
        //            foreach (MouseTips mousetip in mtips)
        //            {
        //                GameObject.Destroy(mousetip.transform.parent.gameObject);

        //            }
        //        }

        //    }

        //}


    //}

    /// <summary>
    /// 通过房间的编号获取对象的数据
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private Object3dItem GetRoomObjectById(Object3dItem floorObject3dItem, string code)
    {
        if (floorObject3dItem.childs != null && floorObject3dItem.childs.Count > 0)
        {
            foreach (Object3dItem roomObject in floorObject3dItem.childs)
            {
                if (!string.IsNullOrEmpty(roomObject.number) &&  roomObject.number.ToLower().Equals(code.ToLower()))
                {
                    return roomObject;


                }
            }
        }
        return null;
    }


 
    /// <summary>
    /// create floor ui navigation
    /// </summary>
    /// <param name="currentData"></param>
    protected override void CreateNavigation(Object3dItem currentData, string frontname,string backName)
    {
        base.CreateNavigation(currentData, frontname, backName);
        string parentid = currentData.parentsId;
        Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(parentid);
        if (object3dItem.childs != null && (object3dItem.childs.Count > 0))
        {
            
            fnu.CreateFloorRoomNavagitionList(GetRangeList(object3dItem.childs, currentData), navigationUI.transform, currentData, frontname, backName);
        }
    }


    private List<Object3dItem>  GetRangeList(List<Object3dItem> items,Object3dItem currentData)
    {
        int maxIndex = currentData.sortIndex + 3;
        int minIndex = currentData.sortIndex -3;

         IEnumerable<Object3dItem> result =
              from object3dItem in items 
              where object3dItem.sortIndex>=minIndex && object3dItem.sortIndex<=maxIndex 
              orderby object3dItem.sortIndex ascending 
              select object3dItem;
        return result.ToList<Object3dItem>();
    }

    protected void DeleteNavigation()
    {
        if (navigationUI != null)
        {
            GameObject.Destroy(navigationUI);
        }
    }

    /// <summary>
    /// 退出前，camera move screenCenter
    /// </summary>
    /// <param name="nextid"></param>
    /// <param name="callBack"></param>
    /// <param name="isRoom"></param>
    protected GameObject PullScreenCenter(string nextid,System.Action<Vector3,float> callBack,bool isRoom = false)
    {
        Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(nextid);
        if (object3dItem == null)
        {
            log.Debug("room is null");
            return null;
        }
        GameObject box = null;
        float duringTime = 1.0f;
        GameObject root = SceneUtility.GetGameByRootName(currentObject.number, currentObject.number);
        if(isRoom)
        {
            box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
        }
        else
        {
            GameObject fj = FindObjUtility.GetTransformChildByName(root.transform, object3dItem.number);
            if(fj)
            {
                box = FindObjUtility.GetTransformChildByName(fj.transform, Constant.ColliderName);
            }
            else
            {
                log.Debug("fj is null");
                return null;
            }
           
        }
        
        if(box == null)
        {
            log.Debug("room is null");
            return null;
        }
        Vector3 center = box.GetComponent<BoxCollider>().bounds.center;
        CameraAnimation.RotationScreenCenter(center, duringTime, callBack);

        return box;


    }
    /// <summary>
    /// 创建楼层或者房间下边的地面
    /// </summary>
    /// <param name="isEnter"></param>
    protected void PlaneManger(Transform box, bool isEnter)
    {
        DestryPlane();
        if (isEnter&&box!=null &&  box.GetComponent<BoxCollider>()!=null)
        {
            bool isEnable = box.GetComponent<BoxCollider>().enabled;
            plane = GameObject.Instantiate(Resources.Load<GameObject>("Other/dimian"));
            box.GetComponent<BoxCollider>().enabled = true;
            Bounds bc = box.GetComponent<BoxCollider>().bounds;
            Vector3 center = box.transform.position;
            //plane.transform.SetParent(box);
            plane.transform.position = new Vector3(center.x, center.y-0.01f, center.z);
            plane.transform.localScale = new Vector3(bc.size.x  * 1.3f, 0.1f,  bc.size.z * 1.3f);
            box.GetComponent<BoxCollider>().enabled = isEnable;
        }
        
    }

    protected void DestryPlane()
    {
        if (plane != null)
        {
            GameObject.Destroy(plane);
        }
    }

    /// <summary>
    /// 设置偏移位置，
    /// </summary>
    /// <param name="currentDataList"></param>
    protected void SetFloorRoomOffestPostion(List<Object3dItem> currentDataList)
    {

        foreach (Object3dItem  item in currentDataList)
        {
           GameObject root =  SceneUtility.GetGameByRootName(item.number, item.number);
            if(root!=null && root.GetComponent<TransformObject>()!=null)
            {
                root.GetComponent<TransformObject>().Reset();
                //root.transform.position = root.GetComponent<TransformObject>().defaultPostion + Vector3.up * offestIndex * 100;
            }
        }
        offestIndex++;
        SceneContext.offestIndex = offestIndex;
    }

}
