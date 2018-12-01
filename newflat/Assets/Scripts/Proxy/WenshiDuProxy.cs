using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WenshiDuProxy  {

    private static ILog log = LogManagers.GetLogger("WenshiDuProxy");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlPostData">设备id，用逗号隔开</param>
    /// <param name="sucesscallBack"></param>
    /// <param name="ErrorCallBack"></param>
    public static void GetWenShiduList(Dictionary<string, string> sqlPostData, System.Action<string> sucesscallBack, 
        System.Action<string> ErrorCallBack)
    {
        if(sqlPostData==null)
        {
            return;
        }
        string url = Config.parse("requestAddress") + "/getPushEquipmentDataList";
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, sqlPostData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error getPushEquipmentList:url =" + url);

              log.Error("http reqeust error getPushEquipmentList:" + error.ToString());

          }));

    }


}
