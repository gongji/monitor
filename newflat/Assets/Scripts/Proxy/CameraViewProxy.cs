using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public sealed class CameraViewProxy  {

    private static ILog log = LogManagers.GetLogger("CameraViewProxy");

    /// <summary>
    /// 查询相机的视角
    /// </summary>
    /// <param name="id"></param>
    /// <param name="callBack"></param>
    public static void GetCameraView(string id,System.Action<Vector3,Vector3,bool> callBack,System.Action ErrorCallBack)
    {
        string url = Config.parse("downPath") + "/cameraview.bat";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, (result)=> {

              List<CameraView> cameraViewList = CollectionsConvert.ToObject<List<CameraView>>(result);

              bool isExsits = false;
              Vector3 postion = Vector3.zero;
              Vector3 angle = Vector3.zero;

              foreach (CameraView item in cameraViewList)
              {
                  if(item.id.ToLower().Equals(id.ToLower()))
                  {
                      isExsits = true;
                      postion = item.postion;
                      angle = item.angel;
                      break;
                  }

              }

              callBack.Invoke(postion, angle, isExsits);

          }, (a) =>
          {

              log.Error("http reqeust error cameraview:url=" + url);

              log.Error("http reqeust error cameraview:" + a.ToString());
              ErrorCallBack.Invoke();

          }));

    }
}
