using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;

public static class ModelData  {



   
    /// <summary>
    /// 模型预制物id，和模型的字典
    /// </summary>
    public static Dictionary<string, GameObject> modelPrefebDic = new Dictionary<string, GameObject>();

    public static Dictionary<string, GameObject> GetmodelPrefebDic
    {
        get
        {
            return modelPrefebDic;
        }
    }

    /// <summary>
    /// 下载完成后，更新model字典
    /// </summary>
    /// <param name="abTask"></param>
    public static void UpdateModelDic(Dictionary<string, ABModelDownloadTask> abTask)
    {
        if (abTask.Count == 0)
        {
            return;
        }
        foreach (string key in abTask.Keys)
        {
            if (!modelPrefebDic.ContainsKey(key))
            {
                modelPrefebDic.Add(key, (GameObject)abTask[key].Data);
            }
        }

    }
    /// <summary>
    /// 更新字典
    /// </summary>
    /// <param name="key"></param>
    /// <param name="abTask"></param>
    public static void UpdateModelDic(string key, ABModelDownloadTask abTask)
    {
        if (!modelPrefebDic.ContainsKey(key))
        {
            modelPrefebDic.Add(key, (GameObject)abTask.Data);
        }
    }



    public  static Dictionary<string, ModelItem> dicModelData = new Dictionary<string, ModelItem>();

    public static void  InitModelData()
    {
        Equipment3dProxy.GetAllModelList((result) => {

           // Debug.Log(result);
            List<ModelItem> modelList =  Utils.CollectionsConvert.ToObject<List<ModelItem>>(result);
            if(modelList==null || modelList.Count==0)
            {
                Debug.Log("modelData is  null");
                return;
            }
            foreach(ModelItem modelItem in modelList)
            {
                dicModelData.Add(modelItem.id, modelItem);
            }
        });
    }



}
