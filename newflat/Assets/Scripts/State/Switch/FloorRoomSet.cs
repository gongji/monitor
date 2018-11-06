using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class FloorRoomSet : BaseSet
{
    protected float cameraMoveTime = 0.5f;

    protected Object3dItem currentObject;
    private static ILog log = LogManagers.GetLogger("FloorRoomSet");

    private GameObject plane;
    protected void OnInit(List<Object3dItem> currentDataList, Action callBack,string frontname,string backName)
    {
        SwitchBG(false);
        if (currentDataList.Count == 1)
        {
            Object3dItem currentData = currentDataList[0];
            this.currentObject = currentData;
            ShowHideScene(currentData);
            Transform box = SceneUtility.GetSceneCollider(currentObject.code).transform;

            PlaneManger(box,true);
            CameraInitSet.StartSet(currentObject.code, box.transform, 0.5f, ()=> {
                CreateTips(currentData);
                if (AppInfo.Platform == BRPlatform.Browser)
                {
                    CreateNavigation(currentData, frontname, backName);
                }
                if(callBack!=null)
                {
                    callBack.Invoke();
                }
            });
        }
    }
    protected void CreateTips(Object3dItem object3dItem)
    {
        GameObject g = SceneUtility.GetGameByRootName(object3dItem.code, object3dItem.code);
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

                    TipsMgr.Instance.CreateTips(collider, roomObject, child, Vector3.one * 0.4f, Vector3.one * 3.0f);
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
                if (!string.IsNullOrEmpty(roomObject.code) &&  roomObject.code.ToLower().Equals(code.ToLower()))
                {
                    return roomObject;


                }
            }
        }
        return null;
    }


    private GameObject navigationUI = null;
    /// <summary>
    /// 创建UI导航
    /// </summary>
    /// <param name="currentData"></param>
    protected void CreateNavigation(Object3dItem currentData, string frontname,string backName)
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        NavigationUI fnui = canvas.GetComponentInChildren<NavigationUI>();
        if (fnui != null)
        {
            return;
        }
        navigationUI = TransformControlUtility.CreateItem("TextNavigation", GameObject.Find("Canvas").transform);
        navigationUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        NavigationUI fnu = navigationUI.GetComponent<NavigationUI>();
        string parentid = currentData.parentid;
        Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(parentid);
        if (object3dItem.childs != null && (object3dItem.childs.Count > 0))
        {
            fnu.CreateNavagitionList(object3dItem.childs, navigationUI.transform, currentData, frontname, backName);
        }
    }

    protected void DeleteNavigation()
    {
        if (navigationUI != null)
        {
            GameObject.Destroy(navigationUI);
        }
    }


    /// <summary>
    /// 显示当前层，其他层隐藏掉
    /// </summary>
    /// <param name="currentData"></param>
    protected void ShowHideScene(Object3dItem currentData)
    {

        string parentid = currentData.parentid;
        Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(parentid);
        if (object3dItem.childs != null && object3dItem.childs.Count > 0)
        {
            foreach (Object3dItem temp in object3dItem.childs)
            {
                GameObject g = SceneUtility.GetGameByRootName(temp.code, temp.code);
                if (g != null)
                {
                    if (temp.id.Equals(currentData.id))
                    {

                        g.SetActive(true);
                    }
                    else
                    {
                        g.SetActive(false);
                    }
                }


            }
        }
    }

    /// <summary>
    /// 退出前，将下一个对象的目标点拉到屏幕中心
    /// </summary>
    /// <param name="nextid"></param>
    /// <param name="callBack"></param>
    /// <param name="isRoom"></param>
    protected void ExeRotaionAnimation(string nextid,System.Action<Vector3,float> callBack,bool isRoom = false)
    {
        Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(nextid);
        if (object3dItem == null)
        {
            log.Debug("room is null");
            return;
        }
        GameObject box = null;
        float duringTime = 1.0f;
        GameObject root = SceneUtility.GetGameByRootName(currentObject.code, currentObject.code);
        if(isRoom)
        {
            box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);
        }
        else
        {
            GameObject fj = FindObjUtility.GetTransformChildByName(root.transform, object3dItem.code);
            if(fj)
            {
                box = FindObjUtility.GetTransformChildByName(fj.transform, Constant.ColliderName);
            }
            else
            {
                log.Debug("fj is null");
                return;
            }
           
        }
        
        if(box == null)
        {
            log.Debug("room is null");
            return;
        }
        Vector3 center = box.GetComponent<BoxCollider>().bounds.center;

        Vector3 relativePos = center - Camera.main.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Camera.main.transform.DORotate(rotation.eulerAngles, duringTime).OnComplete(() => {
            if(callBack!=null)
            {
                callBack.Invoke(center, duringTime);
            }

        });
    }
    /// <summary>
    /// 创建楼层或者房间下边的地面
    /// </summary>
    /// <param name="isEnter"></param>
    protected void PlaneManger(Transform box, bool isEnter)
    {
        DestryPlane();
        if (isEnter)
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

   
}
