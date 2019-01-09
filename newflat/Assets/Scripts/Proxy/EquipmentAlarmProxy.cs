using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Common.Logging;

public class EquipmentAlarmProxy  {

    private static ILog log = LogManagers.GetLogger("EquipmentAlarmProxy");

    /// <summary>
    /// 获取设备的状态
    /// </summary>
    /// <param name="sucesscallBack"></param>
    /// <param name="postData">设备ids</param>
    public static void GetEquipmentAlarmStateList(System.Action<string> successcallBack, string parameter)
    {
        string url = Config.parse("requestAddress") + "/monitoringPointEditor/getEquipmentAlarm?ids=" + parameter;

       // Debug.Log("url="+ url);
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, null, successcallBack, (a) =>
          {

              log.Error("http reqeust error getEquipmentAlarm:url=" + url);

              log.Error("http reqeust error getEquipmentAlarm:" + a.ToString());

          }));

    }


   /// <summary>
   /// 获取报警设备的测点列表，用于查看报警设备的报警测点信息。
   /// </summary>
   /// <param name="sucesscallBack"></param>
   /// <param name="postData">设备id</param>
    public static void GetAlarmPointTestList(System.Action<string> sucesscallBack,string equipmentid)
    {
        string url = Config.parse("requestAddress") + "/monitoringPointEditor/getEquipmentPointAlarm?id="+ equipmentid;

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, null, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error GetAlarmPointTestList:url=" + url);

              log.Error("http reqeust error GetAlarmPointTestList:" + error.ToString());

          }));

    }

}
