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
        string url = Config.parse("downPath") + "/modelData.bat";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result)=> {

              List<ModelCategory> modelList = CollectionsConvert.ToObject<List<ModelCategory>>(result);

              callBack.Invoke(modelList);

          }, (a) =>
          {

              log.Error("http reqeust error GetModelListData:url=" + url);

              log.Error("http reqeust error GetModelListData:" + a.ToString());

          }));

    }
}
