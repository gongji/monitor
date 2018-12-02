using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModelData  {



    public  static Dictionary<string, ModelItem> dicModelData = new Dictionary<string, ModelItem>();

    public static void  InitModelData()
    {
        Equipment3dProxy.GetAllModelList((result) => {

           // Debug.Log(result);
            List<ModelItem> modelList =  Utils.CollectionsConvert.ToObject<List<ModelItem>>(result);
            foreach(ModelItem modelItem in modelList)
            {
                dicModelData.Add(modelItem.id, modelItem);
            }
        });
    }
}
