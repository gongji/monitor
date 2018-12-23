using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public sealed class TestPointProxy
{

    private static ILog log = LogManagers.GetLogger("TestPointProxy");

   /// <summary>
   /// 获取测点数值列表
   /// </summary>
   /// <param name="id"></param>
   /// <param name="callBack"></param>
    public static void GetTestPointData(string id,System.Action<List<EquipmentTestPoint>> callBack)
    {
        string url = Config.parse("requestAddress") + "/GetTestPointData";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result)=> {

              List<EquipmentTestPoint> equipmentTestPointList = CollectionsConvert.ToObject<List<EquipmentTestPoint>>(result);

              callBack.Invoke(equipmentTestPointList);

          }, (a) =>
          {

              log.Error("http reqeust error GetTestPointData:url=" + url);

              log.Error("http reqeust error GetTestPointData:" + a.ToString());

          }));

    }

   /// <summary>
   /// 获取控制点列表
   /// </summary>
    public static void GetControlPointList(string id,System.Action<string> successCallBack)
    {
        string url = Config.parse("requestAddress") + "/GetControlPointList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result) => {

             

          }, (a) =>
          {

              log.Error("http reqeust error GetControlPointList:url=" + url);

              log.Error("http reqeust error GetControlPointList:" + a.ToString());

          }));
    }
}
