﻿using Core.Common.Logging;
using DataModel;
using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 下载器
/// </summary>
public class DownLoader:MonoSingleton<DownLoader> {


    private static ILog log = LogManagers.GetLogger("DownLoader");
    private GameObject loader = null;
    private TaskQueue taskQueue = null;
    /// <summary>
    /// 下载资源，
    /// </summary>
    /// <param name="scenelist"></param>
    /// <param name="equipmentPaths"></param>
    /// <param name="callBack"></param>

    public void StartDownLoad(List<Object3dItem> scenelist ,string[] modelids, System.Action callBack)
    {

        //if(equipmentPaths==null || equipmentPaths.Length == 0)
        //{

        //}
       // log.Debug("下载模型列表："+ equipmentPaths.Length);
        LoadLoader();
        if (scenelist == null)
        {
            throw new System.Exception();
            
        }
        string downscenePath = Config.parse("downPath") + "scene/";
        string downequipmentPath = Config.parse("downPath") + "model/";


        taskQueue = new TaskQueue(this);

       
        //下载场景
        foreach (Object3dItem object3dItem in scenelist)
        {
            string path= downscenePath + object3dItem.code + Constant.ExtendName;

            SceneDownloadTask sceneDownloadTask3 = new SceneDownloadTask(path, path);

            taskQueue.Add(sceneDownloadTask3);
        }

        Dictionary<string, ABModelDownloadTask> abTask = new Dictionary<string, ABModelDownloadTask>();
        //下载设备模型和资源包
        if(modelids != null && modelids.Length>0)
        {
            foreach (string modelid in modelids)
            {
                //避免下载重复
                if(!EquipmentData.modelPrefebDic.ContainsKey(modelid))
                {
                    string path = downequipmentPath + modelid;

                    ABModelDownloadTask abDownloadTask = new ABModelDownloadTask(path, path, modelid);

                    abTask.Add(modelid, abDownloadTask);

                    taskQueue.Add(abDownloadTask);
                }
               

            }
        }
        
       
        taskQueue.StartTask();
        //下载完成
        taskQueue.OnFinish = () => {
           
           GameObject.Destroy(loader);
            loader = null;
            taskQueue = null;
            //更新场景状态

            UpdateDownState(scenelist);
            EquipmentData.UpdateModelDic(abTask);
            if (callBack != null)
            {
                callBack();
            }

        };
    }

    private void Update()
    {
        if(loader!=null && taskQueue!=null)
        {
            //Debug.Log(taskQueue.Progress.ToString());
            loader.GetComponentInChildren<Text>().text = "努力加载中:"+Mathf.CeilToInt( (taskQueue.Progress * 100)).ToString() + "%" ;
        }
    }

    /// <summary>
    /// 装载加载界面
    /// </summary>
    private void LoadLoader()
    {
        loader = GameObject.Instantiate(Resources.Load<GameObject>("loadingMask"));
        loader.transform.parent = GameObject.Find("Canvas").transform;
        loader.transform.localPosition = Vector3.zero;
       
    }

    /// <summary>
    /// 下载完成更新长场景数据的状态
    /// </summary>
    private void UpdateDownState(List<Object3dItem> list)
    {
        foreach(Object3dItem o in list)
        {
            o.isDownFinish = true;
        }
    }



}