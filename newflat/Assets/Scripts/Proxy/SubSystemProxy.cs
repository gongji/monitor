using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SubSystemProxy  {

    private static ILog log = LogManagers.GetLogger("SubSystemProxy");

    
    /// <summary>
    /// 通过场景获取子系统列表
    /// </summary>
    /// <param name="callBack"></param>
    /// <param name="sceneid"></param>
    public static void GetSubSystemByScene(System.Action<string> callBack,string sceneId)
    {
        string url = Config.parse("requestAddress") + "/subject/searchSubjectBySceneId?id="+ sceneId;
       // url = "http://192.168.1.116:8080/3dServer/subject/searchSubjectBySceneId?id="+ sceneId;

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, callBack, (a) =>
          {

              log.Error("http reqeust error searchSubjectBySceneId:url=" + url);

              log.Error("http reqeust error searchSubjectBySceneId:" + a.ToString());

          }));

    }
}
