using Core.Common.Logging;
using DataModel;
using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// downLoader
/// </summary>
public class DownLoader:MonoSingleton<DownLoader> {


    private static ILog log = LogManagers.GetLogger("DownLoader");
    private GameObject loader = null;
    private TaskQueue taskQueue = null;
    /// <summary>
    ///downLoad scene resouce
    /// </summary>
    /// <param name="scenelist"></param>
    /// <param name="equipmentPaths"></param>
    /// <param name="callBack"></param>

    public void StartSceneDownLoad(List<Object3dItem> scenelist , System.Action callBack,bool isGuanWang =false)
    {

        CreateLoaderUI();
        if (scenelist == null)
        {
            throw new System.Exception();
            
        }
        taskQueue = new TaskQueue(this);
        
        foreach (Object3dItem object3dItem in scenelist)
        {
        
            SceneDownloadTask sceneDownloadTask3 = new SceneDownloadTask(object3dItem.id.ToString(), object3dItem.path);

            taskQueue.Add(sceneDownloadTask3);
        }
        taskQueue.StartTask();
 
        taskQueue.OnFinish = () => {
           
           GameObject.Destroy(loader);
            loader = null;
            taskQueue = null;
            //update scened state

            UpdateDownState(scenelist);
          
            if (callBack != null)
            {
                callBack();
            }

        };
    }

    /// <summary>
    /// downLoad model
    /// </summary>
    /// <param name="models"></param>
    /// <param name="callBack"></param>
    public void StartModelDownLoad(string[] modelList, System.Action callBack)
    {

        taskQueue = new TaskQueue(this);
        Dictionary<string, ABModelDownloadTask> abTaskDic = new Dictionary<string, ABModelDownloadTask>();
        if (modelList != null && modelList.Length > 0)
        {
            foreach (string  id in modelList)
            {

                string path = Config.parse("requestAddress") + "/upload/modefile/" + id;
                ModelItem mi = ModelData.dicModelData[id];
                ABModelDownloadTask abDownloadTask = new ABModelDownloadTask(id, path, mi.name);


                abTaskDic.Add(id, abDownloadTask);

                taskQueue.Add(abDownloadTask);
               
            }
        }
        taskQueue.StartTask();
        //finish
        taskQueue.OnFinish = () => {

            GameObject.Destroy(loader);
            loader = null;

            //update

            ModelData.UpdateModelDic(abTaskDic);
            taskQueue = null;
            if (callBack != null)
            {
                callBack();
            }
        };

    }

    public void StartModelDownLoad(ModelCategory mc,System.Action callBack)
    {
        taskQueue = new TaskQueue(this);

        ABModelDownloadTask abDownloadTask = new ABModelDownloadTask(mc.id, mc.path, mc.name);
        taskQueue.Add(abDownloadTask);
        taskQueue.StartTask();
        //finish
        taskQueue.OnFinish = () =>
        {
            ModelData.UpdateModelDic(mc.id, abDownloadTask);
            if(callBack!=null)
            {
                callBack.Invoke();
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

  
    private void CreateLoaderUI()
    {
        loader = GameObject.Instantiate(Resources.Load<GameObject>("loadingMask"));
        loader.transform.SetParent(UIUtility.GetRootCanvas());
        loader.transform.localPosition = Vector3.zero;
       
    }

    /// <summary>
    /// set state
    /// </summary>
    private void UpdateDownState(List<Object3dItem> list)
    {
        foreach(Object3dItem o in list)
        {
            o.isDownFinish = true;
        }
    }





}
