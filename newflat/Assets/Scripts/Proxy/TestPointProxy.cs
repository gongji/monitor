﻿using Core.Common.Logging;
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
    public static void GetTestPointData(string id,System.Action<List<EquipmentTestPoint>> callBack, System.Action failCallBack)
    {
        string url = Config.parse("requestAddress") + "/GetTestPointData";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result)=> {

              List<EquipmentTestPoint> equipmentTestPointList = CollectionsConvert.ToObject<List<EquipmentTestPoint>>(result);

              callBack.Invoke(equipmentTestPointList);

          }, (a) =>
          {
              if(failCallBack!=null)
              {
                  failCallBack.Invoke();
              }
              log.Error("http reqeust error GetTestPointData:url=" + url);

              log.Error("http reqeust error GetTestPointData:" + a.ToString());

          }));

    }

   /// <summary>
   /// 获取控制点列表
   /// </summary>
    public static void GetControlPointList(string id,System.Action<string> successCallBack, System.Action failCallBack)
    {
        string url = Config.parse("requestAddress") + "/GetControlPointList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result) => {

          }, (a) =>
          {
              if (failCallBack != null)
              {
                  failCallBack.Invoke();
              }

              log.Error("http reqeust error GetControlPointList:url=" + url);

              log.Error("http reqeust error GetControlPointList:" + a.ToString());

          }));
    }

    /// <summary>
    /// 查询一个设备是否选在控制点
    /// </summary>
    /// <param name="id"></param>
    /// <param name="success"></param>
    public static void IsExistContronTest(string id,System.Action<string> success, System.Action failCallBack)
    {
        string url = Config.parse("requestAddress") + "/IsExistContronTest?id=" +id;

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result) => {

              success.Invoke(result);

          }, (a) =>
          {

              failCallBack.Invoke();
              log.Error("http reqeust error IsExistContronTest:url=" + url);

              log.Error("http reqeust error IsExistContronTest:" + a.ToString());

          }));
    }
}
