using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BimProxy : MonoBehaviour {

    private static ILog log = LogManagers.GetLogger("BimProxy");


    public static void GetBimData(System.Action<string> callBack, string bimId,System.Action<string> error)
    {
        string url = Config.parse("requestAddress") + "/GetBimData?id=" + bimId;

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, callBack, (errorCallBack) =>
          {
              if(error!=null)
              {
                  error.Invoke(errorCallBack.ToString());
              }
              log.Error("http reqeust error GetBimData:url=" + url);

              log.Error("http reqeust error GetBimData:" + errorCallBack.ToString());

          }));
    }
}
