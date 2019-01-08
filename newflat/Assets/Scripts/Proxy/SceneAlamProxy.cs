using System.Collections;
using System.Collections.Generic;
using Core.Common.Logging;
using UnityEngine;
using Utils;
using System.Linq;

/// <summary>
/// 获取场景的报警信息
/// </summary>
public class SceneAlamProxy : MonoBehaviour {

    private static ILog log = LogManagers.GetLogger("SceneAlamProxy");
    public static void GetSceneAlarmStateList(System.Action<string> sucesscallBack, List<SceneAlarmBase> list)
    {
        var postlist = from n in list
                       select new SceneAlarmItem
                       {
                           id = n.sceneId,
                           number = n.number

                       };

        Dictionary<string,string> dic = BaseProxy.GetPostDataByObject(postlist);
        string url = Config.parse("requestAddress") + "/monitoringPointEditor/getSceneAlarm";
        
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, dic, sucesscallBack, (a) =>
          {

              log.Error("http reqeust error GetSceneAlarmStateList:url=" + url);

              log.Error("http reqeust error GetSceneAlarmStateList:" + a.ToString());

          }));
    }


}
