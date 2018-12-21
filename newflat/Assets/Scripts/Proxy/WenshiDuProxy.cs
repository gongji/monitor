using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WenshiDuProxy  {

    private static ILog log = LogManagers.GetLogger("WenshiDuProxy");

    public static void GetWenShiduList(Dictionary<string, string> sqlPostData, System.Action<string> sucesscallBack, 
        System.Action<string> ErrorCallBack)
    {
        if(sqlPostData==null)
        {
            return;
        }
        string url = Config.parse("requestAddress") + "/getWenshiduList";
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, sqlPostData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error getWenshiduList:url =" + url);

              log.Error("http reqeust error getWenshiduList:" + error.ToString());

          }));

    }


}
