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
        string url = Config.parse("downPath") + "/TestPoint.bat";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result)=> {

              List<EquipmentTestPoint> equipmentTestPointList = CollectionsConvert.ToObject<List<EquipmentTestPoint>>(result);

              callBack.Invoke(equipmentTestPointList);

          }, (a) =>
          {

              log.Error("http reqeust error GetAll3dObjectData:url=" + url);

              log.Error("http reqeust error GetAll3dObjectData:" + a.ToString());

          }));

    }
}
