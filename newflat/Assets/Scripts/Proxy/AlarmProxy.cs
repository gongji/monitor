using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AlarmProxy  {

    private static ILog log = LogManagers.GetLogger("AlarmProxy");
    /// <summary>
    /// 得到设备的报警状态
    /// </summary>
    /// <param name="sqlPostData"></param>
    /// <param name="sucesscallBack"></param>
    /// <param name="errorCallBack"></param>
    public static void GetEquipmentAlarmStateList(Dictionary<string, string> sqlPostData, System.Action<string> sucesscallBack, System.Action<string> errorCallBack)
    {
        string url = Config.parse("requestAddress") + "/GetEquipmentAlarmList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, sqlPostData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error GetEquipmentAlarmList:url =" + url);

              log.Error("http reqeust error GetEquipmentAlarmList:" + error.ToString());
              if (errorCallBack != null)
              {
                  errorCallBack.Invoke(error.ToString());
              }

          }));
    }
    /// <summary>
    /// 得到场景的报警状态列表
    /// </summary>
    /// <param name="sqlPostData"></param>
    /// <param name="sucesscallBack"></param>
    /// <param name="errorCallBack"></param>
    public static void GetSceneAlarmStateList(Dictionary<string, string> sqlPostData, System.Action<string> sucesscallBack, System.Action<string> errorCallBack)
    {
        string url = Config.parse("requestAddress") + "/GetSceneAlarmStateList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, sqlPostData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error GetSceneAlarmStateList:url =" + url);

              log.Error("http reqeust error GetSceneAlarmStateList:" + error.ToString());
              if (errorCallBack != null)
              {
                  errorCallBack.Invoke(error.ToString());
              }

          }));

    }

}
