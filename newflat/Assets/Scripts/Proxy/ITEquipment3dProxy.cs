using Core.Common.Logging;
using DataModel;
using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;
using Utils;

/// <summary>
/// 设备
/// </summary>
public static class ITEquipment3dProxy
{ 
    private static ILog log = LogManagers.GetLogger("ITEquipment3dProxy");
    /// <summary>
    /// search it equipment
    /// </summary>
    /// <param name="sucesscallBack"></param>
    /// <param name="id"></param>
    /// <param name="errorCallBack"></param>
    public static void SearchITEquipmentData(string jiguiId, System.Action<string> sucesscallBack, System.Action<string> errorCallBack)
    {
        string url = Config.parse("requestAddress") + "/searchITEquipment?id="+ jiguiId;
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, null, sucesscallBack, (error) =>
          {
              if(errorCallBack!=null)
              {
                  errorCallBack.Invoke(error.ToString());
              }
              log.Error("http reqeust error searchEquipment:url=" + url);

              log.Error("http reqeust error searchEquipment:" + error.ToString());

          }));

    }

}
