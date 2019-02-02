using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;

public static class ModelData  {
    public static Dictionary<string, GameObject> modelPrefebDic = new Dictionary<string, GameObject>();

    public static Dictionary<string, GameObject> GetmodelPrefebDic
    {
        get
        {
            return modelPrefebDic;
        }
    }

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
