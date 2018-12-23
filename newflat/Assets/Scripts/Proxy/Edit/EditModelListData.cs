using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public sealed class EditModelListData
{
    private static ILog log = LogManagers.GetLogger("EditModelListData");

    public static void GetModelListData(System.Action<List<ModelCategory>> callBack)
    {
        
        string url = Config.parse("requestAddress") + "/getModelTreeNew";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result)=> {
             // Debug.Log(result);
              List<ModelCategory> modelList = CollectionsConvert.ToObject<List<ModelCategory>>(result);

              callBack.Invoke(modelList);

          }, (a) =>
          {

              log.Error("http reqeust error getModelTree:url=" + url);

              log.Error("http reqeust error getModelTree:" + a.ToString());

          }));

    }
}
