using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Common.Logging;

public class EquipmentAnimationProxy
{

    private static ILog log = LogManagers.GetLogger("EquipmentAnimationProxy");

    public static void GetAlarmEquipmentList(System.Action<string> sucesscallBack, Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/GetAnimationEquipmentList";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error GetAnimationEquipmentList:url=" + url);

              log.Error("http reqeust error GetAnimationEquipmentList:" + error.ToString());

          }));

    }

}
