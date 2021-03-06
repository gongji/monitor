﻿using Core.Common.Logging;
using DataModel;
using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;
using Utils;

public static class Scene3dProxy
{

    private static ILog log = LogManagers.GetLogger("Scene3dProxy");
    /// <summary>
    /// 获取所有的3d对象场景的数据
    /// 
    /// </summary>
    /// <param name="callBack"></param>
    public static void GetAll3dObjectData(System.Action<string> callBack)
    {
        string url = Config.parse("requestAddress") + "/GetSceneFileList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, callBack, (a) =>
          {

              log.Error("http reqeust error GetAll3dObjectData:url=" + url);

              log.Error("http reqeust error GetAll3dObjectData:" + a.ToString());

          }));

    }

    public static void PostUpdateScene(System.Action<string> sucesscallBack, Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/saveSceneData";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData,sucesscallBack, (a) =>
          {

              log.Error("http reqeust error saveSceneData:url=" + url);

              log.Error("http reqeust error saveSceneData:" + a.ToString());

          }));

    }

    public static void IsExistNewScene(System.Action<string> callBack)
    {
        string url = Config.parse("requestAddress") + "/checkNewSceneFile";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, callBack, (a) =>
          {

              log.Error("http reqeust error checkNewSceneFile:url=" + url);

              log.Error("http reqeust error checkNewSceneFile:" + a.ToString());

          }));

    }


    

    /// <summary>
    /// 通过场景id获取房间下设备信息
    /// </summary>
    /// <param name="roomsid"></param>
    /// <param name="callBack"></param>
    public static void GetEquipmentData(string currentid,System.Action<List<EquipmentItem>> callBack)
    {


        callBack.Invoke(null);
        //string url = Config.parse("downPath") + "/equipment.bat";

        //HttpRequestSingle.Instance.StartCoroutine(

        //  HttpRequest.GetRequest(url, (result) =>
        //  {
        //      //对结果进行过滤
        //      List<EquipmentItem> list = CollectionsConvert.ToObject<List<EquipmentItem>>(result);

        //      callBack(list);
        //      log.Debug("list=" + list.Count);


        //  }, (a) =>
        //  {

        //      log.Error("http reqeust error currentid=:" + currentid);

        //      log.Error("http reqeust error GetAll3dObjectData:" + a.ToString());

        //  }));
    }


}
