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
    /// <param name="sql">"equipId = 12"</param>
    /// <param name="callBack"></param>
    public static void GetCameraView(Dictionary<string,string> sqlPostData, System.Action<string> sucesscallBack, System.Action<string> ErrorCallBack)
    {
        string url = Config.parse("requestAddress") + "/queryWatch";

        url = "http://192.168.1.116:8080/3dServer/queryWatch";
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, sqlPostData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error queryWatch:url =" + url);

              log.Error("http reqeust error queryWatch:" + error.ToString());

          }));

    }


    /// <summary>
    /// 保存观察点
    /// </summary>
    /// <param name="sucesscallBack"></param>
    /// <param name="postData"></param>
    public static void SaveCameraview(System.Action<string> sucesscallBack, Dictionary<string, string> postData)
    {
        string url = Config.parse("requestAddress") + "/saveWatch";

        url = "http://192.168.1.116:8080/3dServer/saveWatch";
        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.WWWPostRequest(url, postData, sucesscallBack, (error) =>
          {

              log.Error("http reqeust error saveWatch:url=" + url);

              log.Error("http reqeust error saveWatch:" + error.ToString());

          }));

    }
}
