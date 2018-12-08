using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProxy  {

    private static ILog log = LogManagers.GetLogger("UserProxy");

    /// <summary>
    /// 得到维修人的列表
    /// </summary>
    /// <param name="callBack"></param>
    /// <param name="builderid"></param>
    public static void GetRepairUserList(System.Action<string> callBack)
    {
        string url = Config.parse("requestAddress") + "/user/getRepairUser";

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, callBack, (error) =>
          {

              log.Error("http reqeust error getRepairUser:url=" + url);

              log.Error("http reqeust error getRepairUser:" + error.ToString());

          }));
    }


    public static void WebUserLogin(System.Action<string> actionCallBack, System.Action<string>  errorCallBack, string userName,string password)
    {
        string url = Config.parse("requestAddress") + "/user/webuser?userid=" + userName + "&password=" + password;

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, actionCallBack, (error) =>
          {
              if(errorCallBack!=null)
              {
                  errorCallBack.Invoke(error);
              }
              log.Error("http reqeust error UserLogin:url=" + url);

              log.Error("http reqeust error UserLogin:" + error.ToString());

          }));
    }


    public static void PcUserLogin(System.Action<string> actionCallBack, System.Action<string> errorCallBack, string userName, string password)
    {
        string url = Config.parse("requestAddress") + "/user/pcuser?userid=" + userName + "&password=" + password;

        HttpRequestSingle.Instance.StartCoroutine(

          HttpRequest.GetRequest(url, actionCallBack, (error) =>
          {
              if (errorCallBack != null)
              {
                  errorCallBack.Invoke(error);
              }
              log.Error("http reqeust error UserLogin:url=" + url);

              log.Error("http reqeust error UserLogin:" + error.ToString());

          }));
    }



}
