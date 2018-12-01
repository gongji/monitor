using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public sealed class RealTimeDataProxy  {

    private static ILog log = LogManagers.GetLogger("RealTimeDataProxy");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlPostData">设备id，用逗号隔开</param>
    /// <param name="sucesscallBack"></param>
    /// <param name="ErrorCallBack"></param>
    public static void GetWenShiduList(List<string> ids, System.Action<string> sucesscallBack, 
        System.Action<string> ErrorCallBack)
    {


        //string _ids = FormatUtil.ConnetString(ids, ",");

        //string url = Config.parse("requestAddress") + "/monitoringPointEditor/getPushEquipmentDataList?ids=" + _ids;

        //HttpRequestSingle.Instance.StartCoroutine(

        //  HttpRequest.GetRequest(url, (result) =>
        //  {

        //      Debug.Log(result);
        //      List<EquipmentTestPoint> equipmentTestPointList = CollectionsConvert.ToObject<List<EquipmentTestPoint>>(result);

        //      sucesscallBack.Invoke(equipmentTestPointList);

        //  }, (a) =>
        //  {
        //      if (failCallBack != null)
        //      {
        //          failCallBack.Invoke();
        //      }
        //      log.Error("http reqeust error getPushEquipmentDataList:url=" + url);

        //      log.Error("http reqeust error getPushEquipmentDataList:" + a.ToString());

        //  }));

    }


}
