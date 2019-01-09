using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SystemCore.Task;
using System.Collections.Generic;

/// <summary>
/// http request zxw
/// </summary>
public static class HttpRequest  {

	/// <summary>
	/// get请求
	/// </summary>
	/// <returns>The request.</returns>
	/// <param name="url">URL.</param>
	/// <param name="sucessCallBack">Sucess call back.</param>
	/// <param name="errorCallBack">Error call back.</param>
	public static IEnumerator GetRequest(string url,System.Action<string> sucessCallBack,System.Action<string> errorCallBack)
	{
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
          //  Debug.Log("url="+ url);

            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                errorCallBack.Invoke(www.error);
            }
            else
            {
               // Debug.Log(www.responseCode);
                //成功返回
                if (www.responseCode == 200)
                {

                    // Debug.Log(www.downloadHandler);
                   // Debug.Log("www.downloadHandler.text");
                    sucessCallBack.Invoke(www.downloadHandler.text);
                }
            }

        }


    }


    public static IEnumerator GetWWWRequest(string url, System.Action<string> sucessCallBack, System.Action<string> errorCallBack)
    {
        WWW www = new WWW(url);

        yield return www;

        if(!string.IsNullOrEmpty(www.error))
        {
            errorCallBack.Invoke(www.error);
        }
        else
        {
            sucessCallBack.Invoke(www.text);
        }
    }


    /// <summary>
    /// Posts 请求
    /// </summary>
    /// <returns>The request.</returns>
    /// <param name="url">URL.</param>
    /// <param name="postData">Post data.</param>
    /// <param name="sucessCallBack">Sucess call back.</param>
    /// <param name="errorCallBack">Error call back.</param>
    public static IEnumerator PostRequest(string url,string postData,
		System.Action<string> sucessCallBack,System.Action<string> errorCallBack)
	{

		using (UnityWebRequest www = new UnityWebRequest (url,"POST")) 
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes (postData);
			www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer ();
			www.SetRequestHeader ("Content-Type", "application/json");
			yield return www.SendWebRequest();
            if (www.error != null) {
				errorCallBack.Invoke (www.error);
			} else {
				if(www.responseCode == 200)

				{
					sucessCallBack.Invoke (www.downloadHandler.text);
				}
			}
		}
	}

    /// <summary>
    /// post www,UnityWebRequest
    /// </summary>
    /// <param name="url"></param>
    /// <param name="dic"></param>
    /// <param name="sucessCallBack"></param>
    /// <param name="errorCallBack"></param>
    /// <returns></returns>

    public static IEnumerator WWWPostRequest(string url, Dictionary<string,string> dic,
        System.Action<string> sucessCallBack, System.Action<string> errorCallBack)
    {
        WWWForm form = new WWWForm();

        if(dic!=null)
        {
            foreach (string key in dic.Keys)
            {
                form.AddField(key, dic[key]);
            }
        }
       
      
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            if(errorCallBack!=null)
            {
                errorCallBack.Invoke(www.error);
            }
           
        }
        else
        {
            sucessCallBack.Invoke(www.downloadHandler.text);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="dic"></param>
    /// <param name="sucessCallBack"></param>
    /// <param name="errorCallBack"></param>
    /// <returns></returns>

    public static IEnumerator WWWOriginalPostRequest(string url, Dictionary<string, string> dic,
        System.Action<string> sucessCallBack, System.Action<string> errorCallBack)
    {

        WWWForm form = new WWWForm();
        foreach (string key in dic.Keys)
        {
            form.AddField(key, dic[key]);
        }

        WWW www = new WWW(url,form);

        yield return www;

        if (www.error!= null)
        {
            errorCallBack.Invoke(www.error);
        }

        else
        {
            sucessCallBack.Invoke(www.text);

        }
    }
}
