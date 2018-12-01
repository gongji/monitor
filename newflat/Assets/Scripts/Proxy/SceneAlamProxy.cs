using System.Collections;
using System.Collections.Generic;
using Core.Common.Logging;
using UnityEngine;

/// <summary>
/// 获取场景的报警信息
/// </summary>
public class SceneAlamProxy : MonoBehaviour {

    private static ILog log = LogManagers.GetLogger("SceneAlamProxy");
    public static void GetSceneAlarmList(System.Action<string> sucesscallBack, Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/GetSceneAlarmList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData, sucesscallBack, (a) =>
          {

              log.Error("http reqeust error GetSceneAlarmList:url=" + url);

              log.Error("http reqeust error GetSceneAlarmList:" + a.ToString());

          }));
    }


}
