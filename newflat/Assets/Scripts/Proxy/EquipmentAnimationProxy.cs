using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Common.Logging;

/// <summary>
/// equipment animation
/// </summary>
public class EquipmentAnimationProxy
{
    private static ILog log = LogManagers.GetLogger("EquipmentAnimationProxy");

    public static void GetAlarmEquipmentList(System.Action<string> sucesscallBack, List<string> idsList)
    {
        string ids = FormatUtil.ConnetString(idsList, ",");
        string url = Config.parse("requestAddress") + "monitoringPointEditor/getEquipmentAnimation?ids=" + ids;

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, null, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error GetAnimationEquipmentList:url=" + url);

              log.Error("http reqeust error GetAnimationEquipmentList:" + error.ToString());

          }));

    }

}
