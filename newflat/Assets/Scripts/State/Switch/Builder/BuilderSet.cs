﻿using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.Linq;

public class BuilderSet: BaseSet
{
    private static ILog log = LogManagers.GetLogger("BuilderSet");

    private List<Object3dItem> currentfloorData;

    private List<Object3dItem> originalData;
    private float yoffest = 0.6f;

    private GameObject navigationUI;

    #region 进入 逻辑
    public override void Enter(List<Object3dItem> currentData,System.Action callBack)
    {
        base.Enter(currentData, callBack);
        
        this.currentfloorData = GetAllFloor(currentData);
        CreateNavigation();
        SaveOrResetFloorPostion(currentData);
        SwitchBG(false);
        InitCamera(()=> { });
       
        SetFloorSplitAnimation(callBack);

       

    }


    private List<Object3dItem> GetAllFloor(List<Object3dItem> currentData)
    {
        List<Object3dItem> result = new List<Object3dItem>();
        Regex flooRegex = new Regex("f\\d");
        foreach (Object3dItem item in currentData)
        {
            if (flooRegex.IsMatch(item.number) && item.number.Split('_').Length == 3)
            {
                
                result.Add(item);
            }
        }

        IEnumerable<Object3dItem> sortresult =
            from object3dItem in result
            orderby object3dItem.number
            select object3dItem;


        //foreach(Object3dItem aa in sortresult)
        //{
        //    Debug.Log(aa.number);
        //}
        //Debug.Log("result="+ result.Count);
        return sortresult.ToList<Object3dItem>();

    }

    private void InitCamera(System.Action callBack)
    {

        Camera camera = Camera.main.GetComponent<Camera>();
       
        camera.transform.localRotation = Quaternion.identity;

        //SwitchCameraMode(true);
        camera.orthographicSize = GetMaxBoxLength() / 2 / (Screen.width * 1.0f / Screen.height * 1.0f) * 2.0f;
        camera.transform.position = CalculateCameraPostion() - (camera.transform.forward * GetMaxBoxWidth());

        camera.transform.localRotation *= Quaternion.AngleAxis(10.0f, Vector3.right);

        //临时增加y的偏移量

        camera.transform.position += Vector3.up;
        CameraInitSet.SetIsEnbaleCamera(null, false);
        //SetIsEnbaleCamera(null, false);
      //  Camera.main.enabled = true;

        //Vector3 tartPostion = CalculateCameraPostion() - (camera.transform.forward * GetMaxBoxWidth()) + Vector3.up;
        //camera.transform.DOMove(tartPostion, 0.5f).OnComplete(()=> {
        //    if(callBack!=null)
        //    {

        //        callBack.Invoke();
        //    }
        //});

    }

 
    /// <summary>
    /// 获取相机碰撞盒的最大长度
    /// </summary>
    public float GetMaxBoxLength()
    {

        float length = 0f;
        foreach (Object3dItem object3dItem in currentfloorData)
        {
            GameObject collider = SceneUtility.GetSceneCollider(object3dItem.number);
           // Debug.Log(collider.GetComponent<MeshRenderer>().bounds.size);

            if (collider != null && collider.GetComponent<BoxCollider>().bounds.size.x > length)
            {
                length = collider.GetComponent<BoxCollider>().bounds.size.x;

                // log.Debug("length="+ length);
            }
        }

        return length;
    }
    /// <summary>
    /// 获取最大的宽度
    /// </summary>
    /// <returns></returns>
    public float GetMaxBoxWidth()
    {

        float width = 0f;
        foreach (Object3dItem object3dItem in currentfloorData)
        {
            GameObject collider = SceneUtility.GetSceneCollider(object3dItem.number);
            //Debug.Log(collider.GetComponent<MeshRenderer>().bounds.size);

            if (collider != null && collider.GetComponent<BoxCollider>().bounds.size.z > width)
            {
                width = collider.GetComponent<BoxCollider>().bounds.size.z;

                // log.Debug("length=" + length);
            }
        }

        return width;
    }

    /// <summary>
    /// 计算相机的位置
    /// </summary>
    /// <returns></returns>
    public Vector3 CalculateCameraPostion()
    {
        int index = Mathf.CeilToInt(currentfloorData.Count / 2.0f);

        Object3dItem object3dItem = currentfloorData[index - 1];

        GameObject collider = SceneUtility.GetSceneCollider(object3dItem.number);
        if (collider != null)
        {
            return collider.GetComponent<BoxCollider>().bounds.center;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// 切换相机模式，正交和透视的切换
    /// </summary>
    /// <param name="isOrthographic">正交模式</param>
    private void SwitchCameraMode(bool isOrthographic)
    {
        //Camera camera = Camera.main.GetComponent<Camera>();

        //camera.nearClipPlane = 0.01f;
        //if (isOrthographic)
        //{
        //   // camera.orthographic = true;
        //   // camera.gameObject.GetComponent<CameraObjectController>().enabled = false;
        //    //camera.farClipPlane = 100;
        //}
        //else
        //{
        //    //camera.orthographic = false;
        //    //camera.gameObject.GetComponent<CameraObjectController>().enabled = true;
        //    //camera.farClipPlane = 1000;
        //}
    }
    private float moveTime = 0.5f;
    /// <summary>
    /// 设置分开动画
    /// </summary>
    private void SetFloorSplitAnimation(System.Action callBack)
    {
        if(currentfloorData.Count ==1)
        {
            return;
        }
        int index = Mathf.CeilToInt(currentfloorData.Count / 2.0f);

        Object3dItem object3dItem = currentfloorData[index - 1];

        ///奇数,偶数后边考虑
        if (currentfloorData.Count % 2 != 0)
        {
            for (int i = 0; i < currentfloorData.Count; i++)
            {
                GameObject root = SceneUtility.GetGameByRootName(currentfloorData[i].number, currentfloorData[i].number);
                Vector3 tartPostion = root.transform.position - Vector3.up * (index - 1 - i) * yoffest *2;
                root.transform.localRotation = root.transform.localRotation * Quaternion.AngleAxis(-30.0f, Vector3.up);
                root.transform.DOLocalMove(tartPostion, moveTime).SetDelay(1.0f);
            }

        }
        //偶数
        else
        {
            for (int i = 0; i < currentfloorData.Count; i++)
            {
                GameObject root = SceneUtility.GetGameByRootName(currentfloorData[i].number, currentfloorData[i].number);
                Vector3 tartPostion = Vector3.zero;
                if (i==0)
                {
                    tartPostion = root.transform.position - Vector3.up  * yoffest * 2;
                }
                else
                {
                    tartPostion = root.transform.position + Vector3.up * yoffest * 2;
                }
                
                root.transform.localRotation = root.transform.localRotation * Quaternion.AngleAxis(-30.0f, Vector3.up);
                root.transform.DOLocalMove(tartPostion, moveTime).SetDelay(1.0f);
            }
        }
        DOVirtual.DelayedCall(2.0f, () =>
        {
            BuiderNavigationUI.CreateNavigateUI(currentfloorData);
            if(callBack!=null)
            {
                callBack.Invoke();
            }
        });
    }


    protected void CreateNavigation()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        NavigationUI fnui = canvas.GetComponentInChildren<NavigationUI>();
        if (fnui != null)
        {
            Debug.Log(fnui.transform.name);
            return;
        }
        //创建导航
       
        navigationUI = TransformControlUtility.CreateItem("TextNavigation", GameObject.Find("Canvas").transform);
        navigationUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        NavigationUI fnu = navigationUI.GetComponent<NavigationUI>();
       

        int totalCount = SceneData.GetBuilderIndexCount(SceneContext.currentSceneData.id);
   
        fnu.CreateBuiderNavagitionList(SceneContext.currentSceneData, totalCount,SceneContext.FloorGroup, navigationUI.transform);
        
    }

    protected void DeleteNavigation()
    {
       
        if (navigationUI != null)
        {
           
            //GameObject.Destroy(navigationUI);

            Object.DestroyImmediate(navigationUI);
        }
    }

    #endregion

    #region 退出 逻辑

    public override void Exit(string currentid,System.Action callBack)
    {
       
        base.Exit(currentid, callBack);
        List<Object3dItem> topList = new List<Object3dItem>();
        List<Object3dItem> downList = new List<Object3dItem>();
        int index = 0;
        for(int i =0;i< currentfloorData.Count;i++)
        {
            if(currentfloorData[i].id.Equals(currentid))
            {
                index = i;
                break;
            }
        }

        for (int i = 0; i < currentfloorData.Count; i++)
        {
            if(i>index)
            {
                topList.Add(currentfloorData[i]);
            }
            else if(i < index)
            {
                downList.Add(currentfloorData[i]);
            }
        }
        float duringTime = 0.5f;
        //上边向上移动
       foreach(Object3dItem o in topList)
        {
            GameObject g =  SceneUtility.GetGameByRootName(o.number, o.number);
            Vector3 newPostion = g.transform.position + Vector3.up * 10;
            g.transform.DOLocalMove(newPostion, duringTime).OnComplete(()=> {
               // g.SetActive(false);
            });
        }
       //下边向下移动
        foreach (Object3dItem o in downList)
        {
            GameObject g = SceneUtility.GetGameByRootName(o.number, o.number);
            Vector3 newPostion = g.transform.position - Vector3.up * 10;
            g.transform.DOLocalMove(newPostion, duringTime).OnComplete(() => {
                //g.SetActive(false);
            });
        }

        GameObject currentSelect = SceneUtility.GetGameByRootName(currentfloorData[index].number, currentfloorData[index].number);
        currentSelect.transform.DOLocalRotate(Vector3.zero, duringTime);

        DOVirtual.DelayedCall(duringTime * 1.2f, () =>
        {

          //  Debug.Log("回调退出");
            Exit(currentid);

            if (callBack!=null)
            {
                
                callBack.Invoke();
               
            }
           

        });

    }

    public override void Exit(string nextid)
    {
       // Debug.Log("直接退出");
        base.Exit(nextid);
        BuiderNavigationUI.DeleteAllUI();
        DeleteNavigation();

    }
    #endregion
    



    

}
