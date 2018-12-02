using Core.Common.Logging;
using DataModel;
using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;
using Utils;

/// <summary>
/// 设备
/// </summary>
public static class Equipment3dProxy
{

    private static ILog log = LogManagers.GetLogger("Equipment3dProxy");

    /// <summary>
    ///获取设备列表
    /// </summary>
    /// <param name="callBack">sql = name like '%aaa%' and parentsId in(106, 107) and type = 'aaaa'";
    /// "parentsId in(107) or parentsId is null";
    /// 
    /// </param>
    public static void SearchEquipmentData(System.Action<string> sucesscallBack,Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/searchEquipment";

       // url = "http://192.168.1.116:8080/3dServer/searchEquipment";
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error searchEquipment:url=" + url);

              log.Error("http reqeust error searchEquipment:" + error.ToString());

          }));

    }

    /// <summary>
    ///  设备的保存
    /// </summary>
    /// <param name="sucesscallBack"></param>
    /// <param name="postData"></param>
    public static void PostEquipmentSaveData(System.Action<string> sucesscallBack, Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/saveEquipment";

        //url = "http://192.168.1.116:8080/3dServer/saveEquipment";
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData,sucesscallBack, (error) =>
          {

              log.Error("http reqeust error saveSceneData:url=" + url);

              log.Error("http reqeust error saveSceneData:" + error.ToString());

          }));

    }

    public static void  GetAllModelList(System.Action<string> callBack)
    {
        string url = Config.parse("requestAddress") + "/getModelList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, callBack, (a) =>
          {

              log.Error("http reqeust error getModelList:url=" + url);

              log.Error("http reqeust error getModelList:" + a.ToString());

          }));
    }


}
