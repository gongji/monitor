using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Common.Logging;

public class EquipmentAlarmProxy  {

    private static ILog log = LogManagers.GetLogger("EquipmentAlarmProxy");

    /// <summary>
    /// 获取报警设备列表
    /// </summary>
    /// <param name="sucesscallBack"></param>
    /// <param name="postData">设备ids</param>
    public static void GetAlarmEquipmentList(System.Action<string> sucesscallBack, Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/GetAlarmEquipmentList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData, sucesscallBack, (a) =>
          {

              log.Error("http reqeust error GetAlarmEquipmentList:url=" + url);

              log.Error("http reqeust error GetAlarmEquipmentList:" + a.ToString());

          }));

    }


   /// <summary>
   /// 获取报警设备的测点列表
   /// </summary>
   /// <param name="sucesscallBack"></param>
   /// <param name="postData">设备id</param>
    public static void GetAlarmPointTestList(System.Action<string> sucesscallBack, Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/GetAlarmPointTestList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData, sucesscallBack, (a) =>
          {

              log.Error("http reqeust error GetAlarmPointTestList:url=" + url);

              log.Error("http reqeust error GetAlarmPointTestList:" + a.ToString());

          }));

    }

}
