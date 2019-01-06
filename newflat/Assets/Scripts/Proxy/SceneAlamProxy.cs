using System.Collections;
using System.Collections.Generic;
using Core.Common.Logging;
using UnityEngine;
using Utils;

/// <summary>
/// 获取场景的报警信息
/// </summary>
public class SceneAlamProxy : MonoBehaviour {

    private static ILog log = LogManagers.GetLogger("SceneAlamProxy");
    public static void GetSceneAlarmStateList(System.Action<string> sucesscallBack, List<string> sceneids)
    {
        string parameter = FormatUtil.ConnetString(sceneids, ",");
        
        string url = Config.parse("requestAddress") + "/monitoringPointEditor/getSceneAlarm?ids=" + parameter;
        
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, null, sucesscallBack, (a) =>
          {

              log.Error("http reqeust error GetSceneAlarmStateList:url=" + url);

              log.Error("http reqeust error GetSceneAlarmStateList:" + a.ToString());

          }));
    }


}
