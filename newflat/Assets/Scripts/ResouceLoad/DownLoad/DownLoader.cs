using Core.Common.Logging;
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
    /// 下载场景资源
    /// </summary>
    /// <param name="scenelist"></param>
    /// <param name="equipmentPaths"></param>
    /// <param name="callBack"></param>

    public void StartSceneDownLoad(List<Object3dItem> scenelist , System.Action callBack,bool isGuanWang =false)
    {

        LoadLoader();
        if (scenelist == null)
        {
            throw new System.Exception();
            
        }
        taskQueue = new TaskQueue(this);
        //下载场景
        foreach (Object3dItem object3dItem in scenelist)
        {
        
            SceneDownloadTask sceneDownloadTask3 = new SceneDownloadTask(object3dItem.id, object3dItem.path);

            taskQueue.Add(sceneDownloadTask3);
        }
        taskQueue.StartTask();
        //下载完成
        taskQueue.OnFinish = () => {
           
           GameObject.Destroy(loader);
            loader = null;
            taskQueue = null;
            //更新场景状态

            UpdateDownState(scenelist);
          
            if (callBack != null)
            {
                callBack();
            }

        };
    }

    /// <summary>
    /// 下载模型
    /// </summary>
    /// <param name="models"></param>
    /// <param name="callBack"></param>
    public void StartModelDownLoad(string[] modelList, System.Action callBack)
    {

        Dictionary<string, ABModelDownloadTask> abTaskDic = new Dictionary<string, ABModelDownloadTask>();
      // 下载设备模型和资源包
        if (modelList != null && modelList.Length > 0)
        {
            foreach (string  id in modelList)
            {

                string path = Config.parse("requestAddress") + "/upload/equipment/" + id + ".unity3d";
                ABModelDownloadTask abDownloadTask = new ABModelDownloadTask(id, path, id);

                abTaskDic.Add(id, abDownloadTask);

                taskQueue.Add(abDownloadTask);
               
            }
        }
        taskQueue.StartTask();
        //下载完成
        taskQueue.OnFinish = () => {

            GameObject.Destroy(loader);
            loader = null;
            taskQueue = null;
            //更新模型字典

            EquipmentData.UpdateModelDic(abTaskDic);

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
