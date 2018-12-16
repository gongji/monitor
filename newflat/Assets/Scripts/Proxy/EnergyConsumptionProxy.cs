using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 能耗数据展示
/// </summary>
public class EnergyConsumptionProxy  {


    private static ILog log = LogManagers.GetLogger("EnergyConsumptionProxy");


    public static void GetEnergyConsumptionData(System.Action<string> callBack,string builderid)
    {
        string url = Config.parse("requestAddress") + "/GetEnergyConsumptionData?id=" + builderid;
       
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, callBack, (a) =>
          {

              log.Error("http reqeust error GetEnergyConsumptionData:url=" + url);

              log.Error("http reqeust error GetEnergyConsumptionData:" + a.ToString());

          }));
    }
}
